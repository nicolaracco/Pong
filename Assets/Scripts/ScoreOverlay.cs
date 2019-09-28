using UnityEngine;
using UnityEngine.EventSystems;

public class ScoreOverlay : MonoBehaviour
{
    public TMPro.TMP_Text leftScore;
    public TMPro.TMP_Text leftWinStatus;
    public TMPro.TMP_Text rightScore;
    public TMPro.TMP_Text rightWinStatus;
    public GameObject LeftPlayerKeysPlaceholder;
    public GameObject RightPlayerKeysPlaceholder;
    public UnityEngine.UI.Button[] endGameActions;

    public string winnerLabel = "WINNER";
    public string loserLabel = "LOSER";

    public void OnMatchStateChanged(MatchStateTransition transition)
    {
        leftScore.SetText(transition.leftScore.ToString());
        rightScore.SetText(transition.rightScore.ToString());
        if (transition.newState == MatchState.WaitingToStart) {
            RightPlayerKeysPlaceholder.SetActive(GameSettings.RightPlayerType == PlayerType.Human);
            LeftPlayerKeysPlaceholder.SetActive(GameSettings.LeftPlayerType == PlayerType.Human);
        } else {
            RightPlayerKeysPlaceholder.SetActive(false);
            LeftPlayerKeysPlaceholder.SetActive(false);
        }
        if (transition.newState == MatchState.Ended) {
            if (transition.leftScore > transition.rightScore) {
                leftWinStatus.SetText(winnerLabel);
                rightWinStatus.SetText(loserLabel);
            } else {
                leftWinStatus.SetText(loserLabel);
                rightWinStatus.SetText(winnerLabel);
            }
            leftWinStatus.gameObject.SetActive(true);
            rightWinStatus.gameObject.SetActive(true);

            for (int i = 0; i < endGameActions.Length; i++) {
                endGameActions[i].gameObject.SetActive(true);
            }
            EventSystem.current.SetSelectedGameObject(endGameActions[0].gameObject);
        } else {
            leftWinStatus.gameObject.SetActive(false);
            rightWinStatus.gameObject.SetActive(false);

            for (int i = 0; i < endGameActions.Length; i++) {
                endGameActions[i].gameObject.SetActive(false);
            }
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
