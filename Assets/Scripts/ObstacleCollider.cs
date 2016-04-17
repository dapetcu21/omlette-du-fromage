using UnityEngine;
using System.Collections;

public class ObstacleCollider : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            GameplayManager.instance.PlayerHitObstacle();
        }
    }
}
