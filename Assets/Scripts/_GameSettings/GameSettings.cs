using UnityEngine;

namespace GameSettings
{
    public class GameSettings : MonoBehaviour
    {
        public string   gameVersion;
        public int      ropeVertexCount = 200;
        public float    bumpHorizontalScale = 0.5f;
        public string   currentLevel = "GamePlay";
        public bool     firstTimeEnteringGame = true;
        public float    staticEnemyWiggleAmount = 0.1f;
        public float    staticEnemyWiggleSpeed = 1.0f;
        public float    ropeGrabTreshold = 0.1f;
        public float minPlayerVelocity = 0.5f;
        public float musicVolume = 0.5f;
        public Debug    debug;
    }
}
