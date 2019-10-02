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

    public void NewGameHumanVsAI()
    {
        GameSettings.LeftPlayerType = PlayerType.Human;
        GameSettings.RightPlayerType = PlayerType.AI;
        GameSettings.PointsToWin = 11;
        GameSettings.audioEnabled = true;
        SceneManager.LoadScene("Game");
    }

    public void NewGameAIVsHuman()
    {
        GameSettings.LeftPlayerType = PlayerType.AI;
        GameSettings.RightPlayerType = PlayerType.Human;
        GameSettings.PointsToWin = 11;
        GameSettings.audioEnabled = true;
        SceneManager.LoadScene("Game");
    }

    public void NewGameHumanVsHuman()
    {
        GameSettings.LeftPlayerType = PlayerType.Human;
        GameSettings.RightPlayerType = PlayerType.Human;
        GameSettings.PointsToWin = 11;
        GameSettings.audioEnabled = true;
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
