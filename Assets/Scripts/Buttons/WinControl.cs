using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WinControl : MonoBehaviour {

    public List<Image> stars;
    public Sprite activeStar;
    public Sprite blankStar;
    public PauseControl pauseControl { get; set; }

    void Start()
    {
        GameplayManager.instance.SetWinControl(this);
    }

    public void ShowPanel()
    {
        GetComponent<Animator>().SetTrigger("PopUp");
        pauseControl.gameObject.SetActive(false);
        _ResetButtons();
    }

    public void HidePanel()
    {
        GetComponent<Animator>().SetTrigger("PopDown");
        pauseControl.gameObject.SetActive(true);
    }

    public void PlayButtonClick()
    {
        MusicManager man = MusicManager.instance;
        if (man) { man.PlayButtonClick(); }
    }

    void _ResetButtons()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void NextLevel()
    {
        HidePanel();
        GameplayManager.instance.NextLevel();
    }

    public void MainMenu()
    {
        HidePanel();
        GameplayManager.instance.MainMenu();
    }

    public void Retry()
    {
        HidePanel();
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
