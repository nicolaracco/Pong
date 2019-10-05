using UnityEngine;

public class DemoGameLoader : MonoBehaviour
{
    public GameObject GameAreaPrefab;

    void Start()
    {
        GameSettings.Set(-1, PlayerType.AI, PlayerType.AI, true, false);
        Instantiate(GameAreaPrefab, Vector3.zero, Quaternion.identity);
    }
}