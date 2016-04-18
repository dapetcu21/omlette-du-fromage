using UnityEngine;
using System.Collections;

public class PauseControl : MonoBehaviour {

    public Animator pauseAnim;

    void Start()
    {
        GameplayManager.instance.SetPauseControl(this);
        pauseAnim = GetComponent<Animator>();
    }

    public void Resume(Animator anim)
    {
        anim.SetTrigger("Normal");
        GameplayManager.instance.Resume();
    }

    public void MainMenu()
    {
        GameplayManager.instance.MainMenu();
    }
}
