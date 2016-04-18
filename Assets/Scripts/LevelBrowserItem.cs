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
    public Sprite lockedLevel;
    public Sprite unlockedLevel;

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
            buttonImage.sprite = lockedLevel;
            for (i = 0; i < 3 && i < starList.Count; i++)
            {
                starList[i].sprite = lockedStar;
            }
        }
        else
        {
            buttonImage.sprite = unlockedLevel;
            for (i = 0; i < _starCount && i < starList.Count; i++)
            {
                starList[i].sprite = fullStar;
            }

            for (; i < starList.Count; i++)
            {
                starList[i].sprite = blankStar;
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
