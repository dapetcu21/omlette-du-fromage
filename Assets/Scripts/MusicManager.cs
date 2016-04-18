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
        gameSource.Stop();
        menuSource.Play();
    }

    public void PlayGameMusic()
    {
        menuSource.Stop();
        gameSource.Play();
    }
}
