using UnityEngine;
using System.Collections;
using GameSettings;

public class MusicManager : MonoBehaviour
{
    static public MusicManager instance { get; private set; }

    public AudioSource menuSource;
    public AudioSource gameSource;
    public AudioSource buttonSource;
    public GameSettings.GameSettings gameSettings;

    void Awake()
    {
        if (MusicManager.instance) { return; }
        MusicManager.instance = this;
		DontDestroyOnLoad(gameObject);
    }

    public void PlayMenuMusic()
    {
        float volume = gameSettings.musicVolume;
        gameSource.GetComponent<AudioFade>().FadeTo(0.0f, true);
        menuSource.GetComponent<AudioFade>().FadeTo(volume, false);
    }

    public void PlayGameMusic()
    {
        float volume = gameSettings.musicVolume;
        menuSource.GetComponent<AudioFade>().FadeTo(0.0f, true);
        gameSource.GetComponent<AudioFade>().FadeTo(volume, false);
    }

    public void PlayButtonClick()
    {
        buttonSource.Play();
    }
}
