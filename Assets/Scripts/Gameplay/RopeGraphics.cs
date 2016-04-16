using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RopeGraphics : MonoBehaviour {

    public GameObject templateSprite;
    public List<Sprite> ropeSprites;

    private List<Vector2> newVerticies = new List<Vector2>();
    private bool isDirty;
    private List<GameObject> instantiatedSprites;
    private RopeController ropeController;
    
    void Start ()
    {
        instantiatedSprites = new List<GameObject>();
        ropeController = GetComponent<RopeController>();
    }
	
    void Update ()
    {
        newVerticies = ropeController.GetVertices();
        isDirty = ropeController.GetDirty();

        if (isDirty)
        {
            UpdateGraphics();
        }
	}

    private
    void UpdateGraphics()
    {
        //delete
        while(instantiatedSprites.Count > 0)
        {
            Destroy(instantiatedSprites[0]);
            instantiatedSprites.RemoveAt(0);
        }

        foreach(Vector2 vertex in newVerticies)
        {
            foreach (Sprite sprite in ropeSprites)
            {
                Vector2 random = new Vector2(Random.RandomRange(0, 10), Random.RandomRange(0, 10));
                GameObject newSprite = (GameObject)Instantiate(templateSprite, vertex + random, Quaternion.identity);
                newSprite.GetComponent<SpriteRenderer>().sprite = sprite;
                instantiatedSprites.Add(newSprite);
            }
        }

        isDirty = false;
    }
}
