using UnityEngine;
using System.Collections;

public class PlayerInner : MonoBehaviour {
    public void DeathAnimationEnd()
    {
        GetComponentInParent<PlayerController>().DeathAnimationEnd();
    }

    public void ResetPosition()
    {
        GetComponentInParent<PlayerController>().ResetPosition();
    }
}
