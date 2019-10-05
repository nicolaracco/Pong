using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchFSM : MonoBehaviour
{
    public int secondsOfPauseAfterGoal = 1;
    public int pointsToWin = 11;
    public MatchStateChangedEvent OnMatchStateChanged;
    public GameObject SecondPadGameObject;

    GameSettings gameSettings;
    readonly int[] score = new int[] { 0, 0 };
    MatchState state = MatchState.WaitingToStart;
    float timeSpentInCurrentState = 0;
    PlayerID? lastGoalPlayerID = null;

    public int LeftScore { get { return score[0]; } }
    public int RightScore { get { return score[1]; } }
    public MatchState State { get { return state; } }

    bool IsMatchOver
    {
        get
        {
            if (pointsToWin == -1) {
                return false;
            }
            return (LeftScore >= pointsToWin && RightScore < LeftScore - 1) ||
                   (RightScore >= pointsToWin && LeftScore < RightScore - 1);
        }
    }

    bool IsMatchPoint
    {
        get
        {
            if (pointsToWin == -1 || IsMatchOver) {
                return false;
            }
            return (LeftScore >= pointsToWin - 1 && RightScore < LeftScore) ||
                    (RightScore >= pointsToWin - 1 && LeftScore < RightScore);
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnGoalMade(PlayerID playerId)
    {
        lastGoalPlayerID = playerId;
        score[(int)playerId]++;
        MoveStateTo(IsMatchOver ? MatchState.Ended : MatchState.GoalMade);
    }

    public void RestartGame()
    {
        score[0] = 0;
        score[1] = 0;
        lastGoalPlayerID = null;
        MoveStateTo(MatchState.WaitingToStart);
    }

    void Awake()
    {
        gameSettings = GameSettings.Current;
    }

    void Start()
    {
        if (gameSettings != null) {
            pointsToWin = gameSettings.pointsToWin;
        }
        OnMatchStateChanged.Invoke(
            new MatchStateTransition(MatchState.WaitingToStart, MatchState.WaitingToStart, 0, 0, IsMatchPoint, null)
        );
    }

    void Update()
    {
        timeSpentInCurrentState += Time.deltaTime;
        if (
            (state == MatchState.GoalMade || state == MatchState.WaitingToStart) && 
            timeSpentInCurrentState > secondsOfPauseAfterGoal
            ) {
            MoveStateTo(MatchState.Running);
        } else if (state != MatchState.Stopped && state != MatchState.Ended && Input.GetButtonDown("Cancel")) {
            BackToMainMenu();
        }
    }

    private void MoveStateTo(MatchState newState)
    {
        MatchState oldState = state;
        state = newState;
        timeSpentInCurrentState = 0;
        OnMatchStateChanged.Invoke(
            new MatchStateTransition(oldState, newState, LeftScore, RightScore, IsMatchPoint, lastGoalPlayerID)
        );
    }
}
