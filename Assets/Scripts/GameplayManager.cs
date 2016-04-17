using UnityEngine;
using System.Collections.Generic;
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
    public GameObject pausePanel;

    private bool isPaused;
    private float beforeTimeScale;

    List<RopeController> _ropeControllers = new List<RopeController>();

    void Start()
    {
        isPaused = false;
	}

	void Update () {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
	}

    public void AddRopeController(RopeController rc)
    {
        _ropeControllers.Add(rc);
    }

    public void Win () {
		print ("Hey! You just won! Congratulations! Go do something productive with your life now!");
        winPanel.SetActive(true);
	}

    public void Lose()
    {
		player.GetComponent<PlayerController>().Die();
        foreach (RopeController rc in _ropeControllers) {
            rc.userInputEnabled = false;
            rc.AnimateResetBumps();
        }
    }

    public void DeathAnimationEnd()
    {
        foreach (RopeController rc in _ropeControllers) {
            rc.userInputEnabled = true;
        }
    }

    public void NextLevel () {
		print("Hey! You just advanced to a new level! What about doing something productive?");
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings + 1);
	}

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayerHitObstacle()
    {
        player.GetComponent<PlayerController>().HitObstacle();
    }

    public void Pause()
    {
        isPaused = true;
        beforeTimeScale = Time.timeScale;
        Time.timeScale = 0.0f;
        pausePanel.SetActive(true);
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = beforeTimeScale;
        pausePanel.SetActive(false);
    }
}
