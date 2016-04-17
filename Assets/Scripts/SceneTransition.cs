using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransition
{
    private AsyncOperation _sceneLoading;

    private SceneTransition(AsyncOperation sceneLoading)
    {
        _sceneLoading = sceneLoading;
        _sceneLoading.allowSceneActivation = false;

        GameObject[] gameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in gameObjects) {
            if (obj.CompareTag("FadeBackground")) {
                obj.GetComponent<FadeBackground>().StartFade(this);
            }
        }
    }

    public IEnumerator OnEndFade()
    {
        _sceneLoading.allowSceneActivation = true;
        yield return _sceneLoading;
    }

    static public void TransitionToScene(int index)
    {
        new SceneTransition(SceneManager.LoadSceneAsync(index));
    }

    static public void TransitionToScene(string name)
    {
        new SceneTransition(SceneManager.LoadSceneAsync(name));
    }
}
