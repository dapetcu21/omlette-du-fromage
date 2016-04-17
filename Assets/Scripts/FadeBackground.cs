using UnityEngine;
using System.Collections;

public class FadeBackground : MonoBehaviour
{
    SceneTransition _transition;

    public void StartFade(SceneTransition transition)
    {
        _transition = transition;
        GetComponent<Animator>().SetTrigger("quit");
    }

    public void OnEndFade()
    {
        print("on end fade");
        StartCoroutine(_transition.OnEndFade());
    }
}
