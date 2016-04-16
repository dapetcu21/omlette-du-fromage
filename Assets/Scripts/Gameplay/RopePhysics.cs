using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RopePhysics : MonoBehaviour {

    private EdgeCollider2D _edgeCollider;

    private List<Vector2> newVerticies = new List<Vector2>();
    private bool isDirty;
    private RopeController ropeController;

    void Awake ()
    {
        //initialize stuff
        _edgeCollider = GetComponent<EdgeCollider2D>();
        ropeController = GetComponent<RopeController>();
    }
	
	void Update ()
    {
        //get verticies and isDirti from controller.
        newVerticies = ropeController.GetVertices();
        isDirty = ropeController.GetDirty();

        print(newVerticies.Count);

        //update list of points
        if(isDirty)
        {
            _UpdateRopeVerticies();
        }
	}

    private void _UpdateRopeVerticies()
    {
        //replace the points
        _edgeCollider.points = newVerticies.ToArray();
    }
}
