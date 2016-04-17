using UnityEngine;
using System.Collections;

public class PauseControl : MonoBehaviour {
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
