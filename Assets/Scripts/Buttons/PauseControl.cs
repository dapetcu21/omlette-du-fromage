using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PauseControl : MonoBehaviour
{
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
        GetComponent<Animator>().SetTrigger("PopUp");
        _ResetButtons();
    }

    public void HidePanel()
    {
        GetComponent<Animator>().SetTrigger("PopDown");
    }

    public void Resume()
    {
        HidePanel();
        GameplayManager.instance.Resume();
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
}
