using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    static public MusicManager instance { get; private set; }

    public AudioSource menuSource;
    public AudioSource gameSource;

    void Awake()
    {
        print("awake");
        if (MusicManager.instance) { return; }
        MusicManager.instance = this;
		DontDestroyOnLoad(gameObject);
    }

    public void PlayMenuMusic()
    {
        gameSource.GetComponent<AudioFade>().FadeTo(0.0f, true);
        menuSource.GetComponent<AudioFade>().FadeTo(1.0f, false);
    }

    public void PlayGameMusic()
    {
        menuSource.GetComponent<AudioFade>().FadeTo(0.0f, true);
        gameSource.GetComponent<AudioFade>().FadeTo(1.0f, false);
    }
}
