using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WinControl : MonoBehaviour {

    public List<Image> stars;
    public Sprite activeStar;
    public Sprite blankStar;

    public void NextLevel()
    {
        GameplayManager.instance.NextLevel();
    }

    public void MainMenu()
    {
        GameplayManager.instance.MainMenu();
    }

    public void SetStars(int count)
    {
        int i;

        for(i = 0; i < count && i < stars.Count; i++)
        {
            stars[i].sprite = activeStar;
        }

        for(; i < stars.Count; i++)
        {
            stars[i].sprite = blankStar;
        }
    }
}
