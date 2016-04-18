using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PauseControl : MonoBehaviour
{
    public Animator animator;
    public WinControl winControl { get; set; }

    void Start()
    {
        GameplayManager.instance.SetPauseControl(this);
    }

    void _ResetButtons()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ShowPanel()
    {
        animator.SetTrigger("PopUp");
        winControl.gameObject.SetActive(false);
        _ResetButtons();
    }

    public void PlayButtonClick()
    {
        MusicManager man = MusicManager.instance;
        if (man) { man.PlayButtonClick(); }
    }

    public void HidePanel()
    {
        animator.SetTrigger("PopDown");
        winControl.gameObject.SetActive(true);
    }

    public void TogglePauseGame()
    {
        GameplayManager gm = GameplayManager.instance;
        if (gm.IsPaused()) {
            HidePanel();
            gm.Resume();
        } else {
            gm.Pause();
        }
    }

    public void ResumeGame()
    {
        HidePanel();
        GameplayManager.instance.Resume();
    }

    public void MainMenu()
    {
        HidePanel();
        GameplayManager.instance.Resume();
        GameplayManager.instance.MainMenu();
    }

    public void Retry()
    {
        HidePanel();
        GameplayManager.instance.Resume();
        GameplayManager.instance.ResetLevel();
    }
}
