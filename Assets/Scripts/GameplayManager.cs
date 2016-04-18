﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class GameplayManager : MonoBehaviour
{
	/************* singleton *************/
	static public GameplayManager instance { get; private set; }
	void Awake()
	{
		instance = this;
		DatabaseManager.Initialize(gameSettings);

        //level progress logic
        _levelStars.Clear();
        for (int i = 0; i < GameplayManager.instance.levelCount; i++)
        {
            _levelStars.Add(0);
        }
        _unlockedCount = 1;

        //load the progress at start
        LoadProgress();
    }
	/************* singleton *************/


	public GameSettings.GameSettings gameSettings;
	public GameObject player;

    public float twoStarsTargetTime = 20.0f;
    public float threeStarsPercent = 80;

    public int levelCount = 36;

    private bool _isMuted;
    private bool _isPaused;
    private float _beforeTimeScale;

    private float startTime;

    private WinControl _winControl;
    private PauseControl _pauseControl;

    List<RopeController> _ropeControllers = new List<RopeController>();

    //level logic
    private int _unlockedCount = 1;
    private List<int> _levelStars = new List<int>();

    void Start()
    {
        _isMuted = false;
        _isPaused = false;
        Time.timeScale = 1.0f;
        startTime = Time.time;
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

    public void SetWinControl(WinControl wc)
    {
        _winControl = wc;
    }

    public void SetPauseControl(PauseControl pc)
    {
        _pauseControl = pc;
    }

    public void AddRopeController(RopeController rc)
    {
        _ropeControllers.Add(rc);
    }

    public void Win () {

        //update and save progress
        int starCount = GetStarCount();
        //minus one because arrays starts at 0 and level names are from 1
        int levelIndex = GetLevelIndex();

        //warning log
        if(levelIndex > levelCount)
        {
            Debug.LogWarning("Problem with level index. It's greater than the level count (set in GameplayManager)");
        }

        //update stuff in level control
        UpdateLevelStar(levelIndex-1, starCount);
        UpdateUnlockedCount(levelIndex);
        //and save the progress on disk
        SaveProgress();

        //update and activate win panel
        _winControl.SetStars(GetStarCount());
		_winControl.ShowPanel();
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
        startTime = Time.time;
        foreach (RopeController rc in _ropeControllers) {
            rc.userInputEnabled = true;
        }
    }

    public void NextLevel ()
    {
        int c = SceneManager.GetActiveScene().buildIndex;
        if (c < SceneManager.sceneCountInBuildSettings) {
			SceneTransition.TransitionToScene(c + 1);
        }
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
        _pauseControl.ShowPanel();
    }

    public void Resume()
    {
        _isPaused = false;
        Time.timeScale = _beforeTimeScale;
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
        float playTime = Time.time - startTime;
        if (playTime > twoStarsTargetTime) { return 1; }
        if (playTime <= twoStarsTargetTime * threeStarsPercent / 100.0f) { return 3; }
        return 2;
    }

    public int GetLevelIndex()
    {
        string levelIndexStr = SceneManager.GetActiveScene().name.Substring(5);
        return Int32.Parse(levelIndexStr);
    }

    public void LoadProgress()
    {
        if (!HadProgress())
        {
            return;
        }

        //load unlocked levels count
        _unlockedCount = PlayerPrefs.GetInt("UnlockedCount");

        //load stars for each level
        for (int i = 0; i < GameplayManager.instance.levelCount; i++)
        {
            _levelStars[i] = PlayerPrefs.GetInt("LevelStar" + i);
        }

        print("Unlocked count: " + _unlockedCount);
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt("HasProgress", 1);

        PlayerPrefs.SetInt("UnlockedCount", _unlockedCount);

        for (int i = 0; i < GameplayManager.instance.levelCount; i++)
        {
            PlayerPrefs.SetInt("LevelStar" + i, _levelStars[i]);
        }
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("HasProgress", 1);
        PlayerPrefs.SetInt("UnlockedCount", 1);

        for (int i = 0; i < GameplayManager.instance.levelCount; i++)
        {
            PlayerPrefs.SetInt("LevelStar" + i, 0);
        }
    }

    public bool HadProgress()
    {
        return PlayerPrefs.HasKey("HasProgress");
    }

    public void UpdateUnlockedCount(int lastPassedLevel)
    {
        if (lastPassedLevel+1 > _unlockedCount)
        {
            _unlockedCount = lastPassedLevel+1;
        }
    }

    public void UpdateLevelStar(int levelIndex, int starCount)
    {
        if (starCount < 0 || starCount > 3)
        {
            return;
        }

        if(_levelStars[levelIndex] < starCount)
            _levelStars[levelIndex] = starCount;
    }

    public List<int> GetLevelStars() { return _levelStars; }
    public int GetUnlockedCount() { return _unlockedCount; }
    public int GetLevelCount() { return levelCount; }
}
