using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape"))
            Application.Quit();
	}

	public
	void OnClick_Continue()
	{
        SceneManager.LoadScene(GameplayManager.instance.gameSettings.currentLevel);
	}

    public
    void OnClick_Start()
    {
        if(GameplayManager.instance.gameSettings.firstTimeEnteringGame == true) {
            GameplayManager.instance.gameSettings.currentLevel = "Tutorial";
            SceneManager.LoadScene(GameplayManager.instance.gameSettings.currentLevel);
            GameplayManager.instance.gameSettings.firstTimeEnteringGame = false;
        }
        else
        {
            GameplayManager.instance.gameSettings.currentLevel = "Level01";
            SceneManager.LoadScene(GameplayManager.instance.gameSettings.currentLevel);
        }
    }

    public
    void OnClick_Quit()
    {
        Application.Quit();
    }

    public
    void OnClick_Sounds()
    {
        if (AudioListener.volume > 0)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 100;
        }
    }

    public
    void OnClick_TutorialContinue()
    {
        GameplayManager.instance.gameSettings.currentLevel = "Level01";
        SceneManager.LoadScene(GameplayManager.instance.gameSettings.currentLevel);
    }
}
