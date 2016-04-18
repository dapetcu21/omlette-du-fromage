using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CreditsControl : MonoBehaviour {
    public List<string> creditLines;
    public string headText = "";
    public string tailText = "";

    public bool shuffle = true;

    public Vector3 startPosition;
    public Vector3 endPosition;
    public float speed;

    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
        UpdateCredits();
        transform.localPosition = startPosition;
    }

    void Update()
    {
        if(Vector3.Distance(transform.localPosition, endPosition) < 0.1f)
        {
            UpdateCredits();
            transform.localPosition = startPosition;
        }

        float step = speed * Time.deltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, endPosition, step);
    }

    public void UpdateCredits()
    {
        //randomly shuffle
        if(shuffle)
            creditLines.Sort((a, b) => 1 - 2 * Random.Range(0, 2));

        string s = headText.Length == 0 ? "" : (headText + "\n\n");
        foreach (string line in creditLines)
        {
            s += line;
            s += "\n";
        }
        if (tailText.Length != 0) { s += "\n" + tailText; }
        text.text = s;
    }
}
