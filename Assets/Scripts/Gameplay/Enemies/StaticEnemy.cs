using UnityEngine;
using System.Collections;

public class StaticEnemy : MonoBehaviour
{
    Vector3 _position;
    float _wiggleAmount;
    float _wiggleSpeed;
    float _phase;

    void Start()
    {
        _position = transform.position;
        _wiggleAmount = GameplayManager.instance.gameSettings.staticEnemyWiggleAmount;
        _wiggleSpeed = GameplayManager.instance.gameSettings.staticEnemyWiggleSpeed;
        _phase = Random.Range(0, 2.0f * Mathf.PI);
    }

    void Update()
    {
        float wiggle = _wiggleAmount * Mathf.Sin(Time.realtimeSinceStartup * _wiggleSpeed + _phase);
        transform.position = _position + new Vector3(0, wiggle, 0);
    }
}
