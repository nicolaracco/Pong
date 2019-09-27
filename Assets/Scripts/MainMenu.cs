using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMPro.TMP_Text versionLabel;
    public GameObject exitMainMenu;

    public void Exit()
    {
        Application.Quit();
    }

    public void NewGameVsAI()
    {
        GameSettings.LeftPlayerType = PlayerType.AI;
        GameSettings.RightPlayerType = PlayerType.Human;
        GameSettings.PointsToWin = 11;
        SceneManager.LoadScene("Game");
    }

    public void NewGameVsHuman()
    {
        GameSettings.LeftPlayerType = PlayerType.Human;
        GameSettings.RightPlayerType = PlayerType.Human;
        GameSettings.PointsToWin = 11;
        SceneManager.LoadScene("Game");
    }

    void Start()
    {
        versionLabel.SetText("v" + Application.version);
        #if UNITY_WEBGL
        exitMainMenu.SetActive(false);
        #endif
    }
}
