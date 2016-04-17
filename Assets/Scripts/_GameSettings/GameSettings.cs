using UnityEngine;

namespace GameSettings
{
	public class GameSettings : MonoBehaviour
	{
		public string	gameVersion;
		public int 		ropeVertexCount = 200;
		public float	bumpHorizontalScale = 0.5f;
		public string   currentLevel = "GamePlay";
        public bool 	firstTimeEnteringGame = true;
        public Debug	debug;
	}
}
