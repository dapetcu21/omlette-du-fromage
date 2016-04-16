using UnityEngine;
using System.Collections;



public class GameplayManager : MonoBehaviour 
{
	/************* singleton *************/
	static public GameplayManager instance { get; private set; }
	void Awake()
	{
		instance = this;
		DatabaseManager.Initialize(gameSettings);
	}
	/************* singleton *************/


	[SerializeField]
	public GameSettings.GameSettings gameSettings;



	void Start()
	{

	}

	void Update()
	{

	}


	private
	void _PrivateFunc()
	{

	}


	public
	void PublicFunc()
	{

	}
}
