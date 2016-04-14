using UnityEngine;
using System.Collections;

public class GameplayManager : MonoBehaviour 
{
	/************* singleton *************/
	static public GameplayManager instance { get; private set; }
	void Awake()
	{
		instance = this;

		Database.Manager.Initialize();
	}
	/************* singleton *************/



	void Start()
	{

	}
}
