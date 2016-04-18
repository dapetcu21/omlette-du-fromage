using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public Vector2 velocity;
    public List<AudioClip> hitSFX;
    public AudioSource winAudio;
    public AudioSource loseAudio;

    Rigidbody2D _rigidBody;
    Animator _animator;
    Vector2 _initialPosition;
    bool _died = false;
    bool _awakened = false;

    AudioSource _audioSource;

    void Awake()
    {
        GameplayManager.instance.SetPlayerController(this);
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _initialPosition = _rigidBody.position;
        ResetPosition();

        _audioSource = GetComponent<AudioSource>();
    }

    public void Die(bool lose = true)
    {
        if (lose) { loseAudio.Play(); }
        _animator.SetTrigger("death");
        _died = true;
        _rigidBody.velocity = Vector2.zero;
    }

    public void Win()
    {
        winAudio.Play();
    }

    public void AwakenPlayer()
    {
        if (!_awakened)
        {
            _awakened = true;
            _rigidBody.velocity = velocity;
            _animator.SetTrigger("awake");
        }
    }

    public void DeathAnimationEnd()
    {
        _animator.SetTrigger("reset");
        GameplayManager.instance.DeathAnimationEnd();
    }

    public void ResetPosition()
    {
        _rigidBody.position = _initialPosition;
        transform.position = _initialPosition;
        _rigidBody.velocity = Vector2.zero;
        _awakened = false;
        _died = false;
        _UpdateOrientation(velocity, true);
    }

    public void HitObstacle()
    {
        if (!_audioSource.isPlaying) {
            _audioSource.PlayOneShot(hitSFX[Random.Range(0, hitSFX.Count - 1)]);
        }

        Animator animator = GetComponentInChildren<Animator>();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
            animator.SetTrigger("hurt");
        }
    }

    void _UpdateOrientation(Vector2 vel, bool immediate)
    {
        Vector3 lastAxis;
        float lastAngle;
        transform.localRotation.ToAngleAxis(out lastAngle, out lastAxis);
        if (lastAxis.z < 0) { lastAngle *= -1; }

        float angle = -Mathf.Atan2(vel.x, vel.y) * (180.0f / Mathf.PI) + 90.0f;
        angle = lastAngle + MathUtil.AngleShortDiff(angle - lastAngle);
        float dampenedAngle = immediate ? angle : MathUtil.LowPassFilter(lastAngle, angle, Time.deltaTime, 3.0f);

        transform.localRotation = Quaternion.AngleAxis(dampenedAngle, Vector3.forward);

        transform.localScale = new Vector3(
            transform.localScale.x,
            Mathf.Abs(transform.localScale.y) * ((vel.x < 0) ? -1.0f : 1.0f),
            transform.localScale.z
        );
    }

    void Update()
    {
        if (_died || !_awakened) { return; }

        _UpdateOrientation(_rigidBody.velocity, false);

        if (_rigidBody.velocity.magnitude < GameplayManager.instance.gameSettings.minPlayerVelocity) {
            GameplayManager.instance.Lose();
        }
    }
}
