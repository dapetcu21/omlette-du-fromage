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
        GameplayManager.instance.gameSettings.currentLevel = "GamePlay";
        SceneManager.LoadScene(GameplayManager.instance.gameSettings.currentLevel);
    }

    public
    void OnClick_Quit()
    {
        Application.Quit();
    }
}
