using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchFSM : MonoBehaviour
{
    public int secondsOfPauseAfterGoal = 1;
    public MatchStateChangedEvent OnMatchStateChanged;
    public GameObject SecondPadGameObject;

    private readonly int[] score = new int[] { 0, 0 };
    private MatchState state = MatchState.WaitingToStart;
    private float timeSpentInCurrentState = 0;
    private PlayerID? lastGoalPlayerID = null;
    private int? PointsToWin;

    public int LeftScore { get { return score[0]; } }
    public int RightScore { get { return score[1]; } }
    public MatchState State { get { return state; } }

    private bool IsMatchOver
    {
        get
        {
            if (!PointsToWin.HasValue) {
                return false;
            }
            int pointsToWin = PointsToWin.Value;
            return (LeftScore >= pointsToWin && RightScore < LeftScore - 1) ||
                   (RightScore >= pointsToWin && LeftScore < RightScore - 1);
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

    void Start()
    {
        PointsToWin = GameSettings.PointsToWin; // copy the setting because it can be changed while a "demo" is running
        OnMatchStateChanged.Invoke(
            new MatchStateTransition(MatchState.WaitingToStart, MatchState.WaitingToStart, 0, 0, null)
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
            new MatchStateTransition(oldState, newState, LeftScore, RightScore, lastGoalPlayerID)
        );
    }
}
