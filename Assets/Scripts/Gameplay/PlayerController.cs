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
        Update();
	}

    void Update()
    {
        Vector2 vel = _rigidBody.velocity;
        float angle = -Mathf.Atan2(vel.x, vel.y) * (180.0f / Mathf.PI) + 90.0f;
        transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.localScale = new Vector3(
                transform.localScale.x,
                Mathf.Abs(transform.localScale.y) * ((vel.x < 0) ? -1.0f : 1.0f),
                transform.localScale.z
            );
    }
}
