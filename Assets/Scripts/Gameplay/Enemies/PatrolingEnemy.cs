using UnityEngine;
using System.Collections;

public class PatrolingEnemy : MonoBehaviour
{

    public Vector2 patrolPointA;
    public Vector2 patrolPointB;
    public float patrolSpeed = 2;

    bool isPointA = true;
    Vector2 _initialPosition;

    // Use this for initialization
    void Start()
    {
        Vector3 initialPosition = transform.position;
        _initialPosition = new Vector2(initialPosition.x, initialPosition.y);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        Vector2 _position = new Vector2(position.x, position.y);

        Vector2 _target = (isPointA ? patrolPointA : patrolPointB) + _initialPosition;
        Vector3 target = new Vector3(_target.x, _target.y, position.z);

        Vector3 diff = target - position;
        Vector3 dir = diff.normalized;

        float distanceA = (patrolPointA + _initialPosition - _position).magnitude;
        float distanceB = (patrolPointB + _initialPosition - _position).magnitude;
        float distance = Mathf.Min(distanceA, distanceB);

        float velocity = (distance < 0.5f) ? distance * 2.0f * patrolSpeed : patrolSpeed;
        transform.position = position + dir * velocity * Time.deltaTime;

        Vector3 scale = transform.localScale;
        scale.x = dir.x <= 0 ? -1.0f : 1.0f;
        transform.localScale = scale;

        if ((isPointA ? distanceA : distanceB) < 0.1f) { isPointA = !isPointA; }
    }
}
