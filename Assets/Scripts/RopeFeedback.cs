using UnityEngine;
using System.Collections.Generic;

public class RopeFeedback : MonoBehaviour
{

    public RopeController ropeController;
    public GameObject feedbackNub;

    List<GameObject> _feedbackIndicators = new List<GameObject>();

    void Update()
    {
        Dictionary<int, Vector2> dragPoints = ropeController.GetGrabPoints();

        int i = 0;
        foreach (KeyValuePair<int, Vector2> pair in dragPoints)
        {
            if (_feedbackIndicators.Count <= i)
            {
                GameObject gameObject = Instantiate(feedbackNub);
                _feedbackIndicators.Add(gameObject);
            }
            _feedbackIndicators[i].transform.localPosition = new Vector3(pair.Value.x, pair.Value.y, 0) + transform.position;
            i++;
        }

        int visibleCount = i;

        if (visibleCount < _feedbackIndicators.Count) {
            for (i = visibleCount; i < _feedbackIndicators.Count; i++) {
                Destroy(_feedbackIndicators[i]);
            }
            _feedbackIndicators.RemoveRange(visibleCount, _feedbackIndicators.Count - visibleCount);
        }
    }
}
