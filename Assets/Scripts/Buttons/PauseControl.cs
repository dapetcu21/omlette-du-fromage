using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PauseControl : MonoBehaviour
{
    public Animator animator;

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
        _ResetButtons();
    }

    public void HidePanel()
    {
        animator.SetTrigger("PopDown");
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
