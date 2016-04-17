using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MuteUnmute : MonoBehaviour {
    public Sprite mutedSprite;
    public Sprite unmutedSprite;

    private Image img;

    void Start()
    {
        img = GetComponent<Image>();
        if(GameplayManager.instance.GetMuted())
        {
            img.sprite = mutedSprite;
        }
        else
        {
            img.sprite = unmutedSprite;
        }
    }

    public void DoMute()
    {
        if (GameplayManager.instance.GetMuted())
        {
            img.sprite = unmutedSprite;
            GameplayManager.instance.Unmute();
        }
        else
        {
            img.sprite = mutedSprite;
            GameplayManager.instance.Mute();
        }
    }
}
