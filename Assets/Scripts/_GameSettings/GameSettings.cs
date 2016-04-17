﻿using UnityEngine;

namespace GameSettings
{
	public class GameSettings : MonoBehaviour
	{
		public string	gameVersion;
		public int 		ropeVertexCount;
		public float	bumpHorizontalScale;
        public string   currentLevel = "GamePlay";
        public bool     firstTimeEnteringGame = true;
		public Debug	debug;
	}
}
