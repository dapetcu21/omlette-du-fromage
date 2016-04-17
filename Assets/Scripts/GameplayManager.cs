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
    public GameObject pausePanel;

    private bool _isMuted;
    private bool _isPaused;
    private float _beforeTimeScale;

	void Start () {
        _isMuted = false;
        _isPaused = false;
	}

	void Update () {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
                Resume();
            else
                Pause();
        }
	}

	public void Win () {
		print ("Hey! You just won! Congratulations! Go do something productive with your life now!");
        winPanel.SetActive(true);
	}

	public void Lose () {
		player.GetComponent<PlayerController>().Die();
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
        pausePanel.SetActive(true);
    }

    public void Resume()
    {
        _isPaused = false;
        Time.timeScale = _beforeTimeScale;
        pausePanel.SetActive(false);
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
}
