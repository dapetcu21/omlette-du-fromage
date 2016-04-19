using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RopeGraphics : MonoBehaviour {

    public GameObject templateSprite;
    public List<Sprite> ropeSprites;

    private List<GameObject> instantiatedSprites;
    private RopeController ropeController;
    private LineRenderer lineRenderer;

    void Start ()
    {
        instantiatedSprites = new List<GameObject>();
        ropeController = GetComponent<RopeController>();
        lineRenderer = GetComponent<LineRenderer>();

        //UpdateGraphics();
        UpdateLineGraphics();
    }
    
    void Update ()
    {
        if (ropeController.GetDirty())
        {
            //UpdateGraphics();
            UpdateLineGraphics();
        }
    }

    private void UpdateGraphics()
    {
        //delete
        while(instantiatedSprites.Count > 0)
        {
            Destroy(instantiatedSprites[0]);
            instantiatedSprites.RemoveAt(0);
        }

        List<Vector2> newVerticies = ropeController.GetVertices();
        foreach(Vector2 vertex in newVerticies)
        {
            foreach (Sprite sprite in ropeSprites)
            {
                Vector2 random = new Vector2(Random.Range(0.0f, 0.1f), Random.Range(0.0f, 0.1f));
                GameObject newSprite = (GameObject)Instantiate(templateSprite, vertex + random, Quaternion.identity);
                newSprite.GetComponent<SpriteRenderer>().sprite = sprite;
                instantiatedSprites.Add(newSprite);
            }
        }
    }

    private void UpdateLineGraphics()
    {
        List<Vector2> newVerticies = ropeController.GetVertices();
        lineRenderer.SetVertexCount(newVerticies.Count);
        for (int i = 0; i < newVerticies.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(newVerticies[i].x, newVerticies[i].y, 0));
        }   
    }
}
