using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuLogic : MonoBehaviour
{

    void Start()
    {
        MusicManager.instance.PlayMenuMusic();
    }

    public void OnClick_Start()
    {
        if (!PlayerPrefs.HasKey("lastLevel"))
        {
            PlayerPrefs.SetString("lastLevel", "Level01");
        }
        print(PlayerPrefs.GetString("lastLevel"));
        SceneTransition.TransitionToScene(PlayerPrefs.GetString("lastLevel"));
        MusicManager.instance.PlayGameMusic();
    }

    public void OnClick_Quit()
    {
        Application.Quit();
    }

    public void OnClick_Credits()
    {
        SceneTransition.TransitionToScene("Credits");
    }

    public void OnClick_CreditsBack()
    {
        SceneTransition.TransitionToScene("MainMenu");
    }
}
