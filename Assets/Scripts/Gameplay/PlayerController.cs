using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Vector2 velocity;

    Rigidbody2D _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
		_rigidBody.velocity = velocity;
		_rigidBody.rotation = Mathf.Atan2(velocity.y, velocity.x);
    }

    void FixedUpdate()
    {
        //rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);
    }
}
