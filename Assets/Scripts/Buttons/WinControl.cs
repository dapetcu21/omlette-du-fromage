using UnityEngine;
using System.Collections;

public class WinControl : MonoBehaviour {

    public void NextLevel()
    {
        GameplayManager.instance.NextLevel();
    }

    public void MainMenu()
    {
        GameplayManager.instance.MainMenu();
    }
}
