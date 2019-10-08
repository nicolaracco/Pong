using UnityEngine;
using Pong.PadBehaviours;

public class PadBehaviourLoader : MonoBehaviour
{
    public PlayerID playerID;
    public PlayerType playerType = PlayerType.AI;

    void Awake()
    {
        LoadGameSettings();
        
        AbstractPad padBehaviour = CreatePadBehaviour();
        padBehaviour.playerId = playerID;

        MatchFSM match = Object.FindObjectOfType<MatchFSM>();
        match.OnMatchStateChanged.AddListener(padBehaviour.OnMatchStateChanged);
    }

    private void LoadGameSettings()
    {
        GameSettings gameSetting = GameSettings.Current;
        if (gameSetting != null) {
            playerType = gameSetting.GetPlayerTypeForPlayerID(playerID);
        }
    }

    private AbstractPad CreatePadBehaviour()
    {
        if (playerType == PlayerType.AI) {
            return gameObject.AddComponent<AIPad>();
        }
        return gameObject.AddComponent<HumanPad>();
    }
}