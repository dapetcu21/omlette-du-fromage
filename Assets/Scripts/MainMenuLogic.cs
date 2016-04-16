using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class MainMenuLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape"))
            Application.Quit();
	}

    void OnApplicationQuit()
    {
        Debug.Log("Game ending after " + Time.time + " seconds");
    }

    void OnGUI()
    {
        int boxw = 200, boxh = 200, btnw = 100, btnh = 30, bdist = 5;
        GUI.Box(new Rect(Screen.width / 2 - (boxw / 2), Screen.height / 2, boxw, boxh), "Game name placeholder");
        if (GUI.Button(new Rect(Screen.width / 2 - (boxw / 2) + (btnw / 2), Screen.height / 2 + (boxh / 2), btnw, btnh), "Start")) {
            Application.LoadLevel("GamePlay");
        }
        if (GUI.Button(new Rect(Screen.width / 2 - (boxw / 2) + (btnw / 2), Screen.height / 2 + (boxh / 2) + btnh + bdist, btnw, btnh), "Exit")) {
            Application.Quit();
        }
    }



	public
	void OnClick_Start()
	{
		SceneManager.LoadScene( "GamePlay" );
	}
}
