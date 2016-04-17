using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameplayManager.instance.Lose();
        }
    }
}
