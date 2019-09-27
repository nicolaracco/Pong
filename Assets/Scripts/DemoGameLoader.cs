using UnityEngine;

public class DemoGameLoader : MonoBehaviour
{
    public GameObject GameAreaPrefab;

    void Start()
    {
        GameSettings.LeftPlayerType = PlayerType.AI;
        GameSettings.RightPlayerType = PlayerType.AI;
        GameSettings.PointsToWin = null;
        Instantiate(GameAreaPrefab, Vector3.down, Quaternion.identity);
    }
}