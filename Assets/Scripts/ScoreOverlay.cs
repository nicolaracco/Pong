using UnityEngine;
using UnityEngine.EventSystems;

public class ScoreOverlay : MonoBehaviour
{
    public GameObject LeftPlayerKeysPlaceholder;
    public GameObject RightPlayerKeysPlaceholder;
    public UnityEngine.UI.Button[] endGameActions;

    public string winnerLabel = "WINNER";
    public string loserLabel = "LOSER";

    public void OnMatchStateChanged(MatchStateTransition transition)
    {
        if (transition.newState == MatchState.WaitingToStart) {
            RightPlayerKeysPlaceholder.SetActive(GameSettings.RightPlayerType == PlayerType.Human);
            LeftPlayerKeysPlaceholder.SetActive(GameSettings.LeftPlayerType == PlayerType.Human);
        } else {
            RightPlayerKeysPlaceholder.SetActive(false);
            LeftPlayerKeysPlaceholder.SetActive(false);
        }
        if (transition.newState == MatchState.Ended) {
            for (int i = 0; i < endGameActions.Length; i++) {
                endGameActions[i].gameObject.SetActive(true);
            }
            EventSystem.current.SetSelectedGameObject(endGameActions[0].gameObject);
        } else {
            for (int i = 0; i < endGameActions.Length; i++) {
                endGameActions[i].gameObject.SetActive(false);
            }
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
