using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Vector2 velocity;
    public Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);
    }
}
