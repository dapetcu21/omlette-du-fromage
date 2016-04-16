using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Vector2 velocity;

    Rigidbody2D _rigidBody;
	Vector2 initialPosition;

    void Start()
    {
		GameplayManager.instance.player = gameObject;
        _rigidBody = GetComponent<Rigidbody2D>();
		initialPosition = _rigidBody.position;
		ResetPosition();
    }

	public void ResetPosition() {
		_rigidBody.position = initialPosition;
		_rigidBody.velocity = velocity;
		_rigidBody.rotation = Mathf.Atan2(velocity.y, velocity.x);
	}
}
