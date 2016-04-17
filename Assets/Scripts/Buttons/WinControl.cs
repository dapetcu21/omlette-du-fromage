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
        GetComponent<Animator>().SetTrigger("PopDown");
        GameplayManager.instance.NextLevel();
    }

    public void MainMenu()
    {
        GetComponent<Animator>().SetTrigger("PopDown");
        GameplayManager.instance.MainMenu();
    }

    public void Retry()
    {
        GetComponent<Animator>().SetTrigger("PopDown");
        GameplayManager.instance.ResetLevel();
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
