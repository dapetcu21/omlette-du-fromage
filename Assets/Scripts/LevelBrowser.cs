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

        int i, k, pageIndex = 0, itemIndex = 0, computedIndex = 0;
        GameObject currentItem;
        
        for(k = 0; k < _levelCount / 3; k++)
        {
            for (i = 0; i < 3; i++)
            {
                pageIndex = itemIndex / 12;
                itemIndex++;
                currentItem = (GameObject)Instantiate(levelBrowserItem);
                currentItem.transform.SetParent(levelSelector.transform);
                currentItem.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                computedIndex = 8 * pageIndex + i * 4 + k;
                currentItem.GetComponent<LevelBrowserItem>().UpdateStars(_levelStars[computedIndex], (_unlockedCount > computedIndex) ? false : true, computedIndex + 1);

            }
        }        
    }
}
