using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour 
{
	/************* singleton *************/
	static public GameplayManager instance { get; private set; }
	void Awake()
	{
		instance = this;
		DatabaseManager.Initialize(gameSettings);
	}
	/************* singleton *************/


	public GameSettings.GameSettings gameSettings;
	public GameObject player;
    public GameObject winPanel;

	void Start () {
	}

	void Update () {
	}

	public void Win () {
		print ("Hey! You just won! Congratulations! Go do something productive with your life now!");
        winPanel.SetActive(true);
	}

	public void Lose () {
		player.GetComponent<PlayerController>().ResetPosition();
	}

	public void NextLevel () {
		print("Hey! You just advanced to a new level! What about doing something productive?");
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings + 1);
	}

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
