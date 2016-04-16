using UnityEngine;
using System.Collections;

public class WinControl : MonoBehaviour {

    public void NextLevel()
    {
        print("You PRESSEEEED ME!");
        GameplayManager.instance.NextLevel();
    }

    public void MainMenu()
    {
        GameplayManager.instance.MainMenu();
    }
}
