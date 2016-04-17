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
        SceneManager.LoadScene(PlayerPrefs.GetString("currentLevel"));
	}

    public
    void OnClick_Start()
    {
        //PlayerPrefs.DeleteKey("currentLevel");
        //PlayerPrefs.DeleteKey("firstTimeEnteringGame");

        if (!PlayerPrefs.HasKey("currentLevel"))
        {
            PlayerPrefs.SetString("currentLevel", "Tutorial");
        }
        else {
            PlayerPrefs.SetString("currentLevel", "Level01");
        }

        if (!PlayerPrefs.HasKey("firstTimeEnteringGame"))
        {
            PlayerPrefs.SetInt("firstTimeEnteringGame", 1);
        }
 
        if(PlayerPrefs.GetInt("firstTimeEnteringGame") == 1) {
            SceneManager.LoadScene(PlayerPrefs.GetString("currentLevel"));
            PlayerPrefs.SetInt("firstTimeEnteringGame", 0);
        }
        else
        {
            PlayerPrefs.SetString("currentLevel", "Level01");
            SceneManager.LoadScene(PlayerPrefs.GetString("currentLevel"));
        }
    }

    public
    void OnClick_Quit()
    {
        Application.Quit();
    }

    public
    void OnClick_Credits()
    {
        PlayerPrefs.SetString("currentLevel", "Credits");
        SceneManager.LoadScene(PlayerPrefs.GetString("currentLevel"));
    }

    public
    void OnClick_CreditsBack()
    {
        PlayerPrefs.SetString("currentLevel", "MainMenu");
        SceneManager.LoadScene(PlayerPrefs.GetString("currentLevel"));
    }

    public
    void OnClick_TutorialContinue()
    {
        PlayerPrefs.SetString("currentLevel", "Level01");
        SceneManager.LoadScene(PlayerPrefs.GetString("currentLevel"));
    }
}
