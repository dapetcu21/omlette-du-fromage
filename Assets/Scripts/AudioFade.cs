using UnityEngine;
using System.Collections;

public class AudioFade : MonoBehaviour
{
    float target = 1.0f;
    bool stop = false;
    AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void FadeTo(float _volume, bool _stop)
    {
        target = _volume;
        stop = _stop;
        if (!_audioSource.isPlaying) {
            _audioSource.Play();
        }
    }

    void Update()
    {
        float fadeVelocity = 1.0f;
        float volume = _audioSource.volume;
        float amount = fadeVelocity * Time.deltaTime;
        float sign = ((target - volume < 0) ? -1.0f : 1.0f);
        float remaining = (target - volume) * sign;
        if (amount >= remaining) {
            amount = remaining;
            if (stop) {
                _audioSource.Stop();
            }
        }
        _audioSource.volume = volume + amount * sign;
    }
}
