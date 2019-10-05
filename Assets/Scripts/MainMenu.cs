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
        GameSettings.Set(11, PlayerType.Human, PlayerType.AI, false, true);
        SceneManager.LoadScene("Game");
    }

    public void NewGameAIVsHuman()
    {
        GameSettings.Set(11, PlayerType.AI, PlayerType.Human, false, true);
        SceneManager.LoadScene("Game");
    }

    public void NewGameHumanVsHuman()
    {
        GameSettings.Set(11, PlayerType.Human, PlayerType.Human, false, true);
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
