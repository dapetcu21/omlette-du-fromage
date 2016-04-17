using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            GameplayManager.instance.Win();
            print("You win!");
        }
    }
}
