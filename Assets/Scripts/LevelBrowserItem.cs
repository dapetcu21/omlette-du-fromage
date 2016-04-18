using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelBrowserItem : MonoBehaviour {
    public List<Image> starList = new List<Image>();
    public Image buttonImage;
    public Text levelText;

    public Sprite fullStar;
    public Sprite blankStar;
    public Sprite lockedStar;
    public List<Sprite> lockedList = new List<Sprite>();
    public List<Sprite> unlockedList = new List<Sprite>();

    private int _starCount = 0;
    private bool _isLocked = true;
    private int _levelIndex;

    void Start()
    {
    }

    public void UpdateStars(int starCount, bool isLocked, int levelIndex)
    {
        int i;
        _levelIndex = levelIndex;
        _starCount = starCount;
        _isLocked = isLocked;

        levelText.text = "Level " + levelIndex;

        if (isLocked == true)
        {
            for (i = 0; i < 3 && i < starList.Count; i++)
            {
                starList[i].sprite = lockedStar;
            }

            if (_levelIndex >= 1 && _levelIndex <= 12)
            {
                buttonImage.sprite = lockedList[0];
            }
            else if (_levelIndex >= 13 && _levelIndex <= 24)
            {
                buttonImage.sprite = lockedList[1];
            }
            else if (_levelIndex >= 25 && _levelIndex <= 36)
            {
                buttonImage.sprite = lockedList[2];
            }
        }
        else
        {
            for (i = 0; i < _starCount && i < starList.Count; i++)
            {
                starList[i].sprite = fullStar;
            }

            for (; i < starList.Count; i++)
            {
                starList[i].sprite = blankStar;
            }

            if (_levelIndex >= 1 && _levelIndex <= 12)
            {
                buttonImage.sprite = unlockedList[0];
            }
            else if (_levelIndex >= 13 && _levelIndex <= 24)
            {
                buttonImage.sprite = unlockedList[1];
            }
            else if (_levelIndex >= 25 && _levelIndex <= 36)
            {
                buttonImage.sprite = unlockedList[2];
            }
        }
    }

    public void LoadSelf()
    {
        if(_isLocked)
        {
            return;
        }

        string levelIndexStr;
        if (_levelIndex < 10)
        {
            levelIndexStr = "0" + _levelIndex;
        }
        else
        {
            levelIndexStr = _levelIndex.ToString();
        }

        MusicManager.instance.PlayGameMusic();
        SceneTransition.TransitionToScene("Level" + levelIndexStr);
    }
}
