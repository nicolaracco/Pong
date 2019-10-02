using UnityEngine;

public class DemoGameLoader : MonoBehaviour
{
    public GameObject GameAreaPrefab;

    void Start()
    {
        GameSettings.LeftPlayerType = PlayerType.AI;
        GameSettings.RightPlayerType = PlayerType.AI;
        GameSettings.PointsToWin = null;
        GameSettings.audioEnabled = false;
        Instantiate(GameAreaPrefab, Vector3.zero, Quaternion.identity);
    }
}