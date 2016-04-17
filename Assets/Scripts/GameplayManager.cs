using UnityEngine;
using System.Collections;
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

    private bool _isMuted;
    private bool _isPaused;
    private float _beforeTimeScale;

    List<RopeController> _ropeControllers = new List<RopeController>();

    void Start()
    {
        _isMuted = false;
        _isPaused = false;
	}

	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
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
        winPanel.GetComponent<WinControl>().SetStars(GetStarCount());
        winPanel.GetComponent<Animator>().SetTrigger("PopUp");
    }

    public void Lose()
    {
		ResetLevel();
    }

    public void ResetLevel()
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
        _isPaused = true;
        _beforeTimeScale = Time.timeScale;
        Time.timeScale = 0.0f;
        pausePanel.GetComponent<PauseControl>().pauseAnim.SetTrigger("PopUp");
    }

    public void Resume()
    {
        _isPaused = false;
        Time.timeScale = _beforeTimeScale;
        pausePanel.GetComponent<PauseControl>().pauseAnim.SetTrigger("PopDown");
    }

    public void Mute()
    {
        _isMuted = true;
        AudioListener.volume = 0.0f;
    }

    public void Unmute()
    {
        _isMuted = false;
        AudioListener.volume = 1.0f;
    }

    public bool GetMuted()
    {
        return _isMuted;
    }

    public int GetStarCount()
    {
        return 3;
    }
    
}
