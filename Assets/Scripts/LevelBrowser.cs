using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBrowser : MonoBehaviour {
    public GameObject levelSelector;
    public GameObject levelBrowserItem;
    
    private List<int> _levelStars;
    private int _unlockedCount;
    private int _levelCount;

    void Start ()
    {
        _levelStars = GameplayManager.instance.GetLevelStars();
        _unlockedCount = GameplayManager.instance.GetUnlockedCount();
        _levelCount = GameplayManager.instance.GetLevelCount();

        int i;
        GameObject currentItem;

        for(i = 0; i < _levelCount; i++)
        {
            currentItem = (GameObject)Instantiate(levelBrowserItem);
            currentItem.transform.SetParent(levelSelector.transform);
            currentItem.GetComponent<LevelBrowserItem>().UpdateStars(_levelStars[i], (_unlockedCount > i) ? false : true, i+1);
        }
    }
}
