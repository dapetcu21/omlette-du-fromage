using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RopePhysics : MonoBehaviour {

    public bool isDown;

    private PolygonCollider2D _collider;

    private List<Vector2> newVerticies = new List<Vector2>();
    private bool isDirty;
    private RopeController ropeController;

    void Awake ()
    {
        //initialize stuff
        _collider = GetComponent<PolygonCollider2D>();
        ropeController = GetComponent<RopeController>();
    }
	
	void Update ()
    {
        //update list of points
        if(ropeController.GetDirty())
        {
            _UpdateRopeVerticies();
        }
	}

    private void _UpdateRopeVerticies()
    {
        //replace the points
        List<Vector2> newVerticies = ropeController.GetVertices();

        float cameraUp = Camera.main.orthographicSize;
        float cameraLeft = cameraUp * Screen.width / Screen.height;
        float cameraRight = -cameraLeft;

        if (isDown) {
            newVerticies.Add(new Vector2(cameraLeft, -cameraUp));
            newVerticies.Add(new Vector2(cameraRight, -cameraUp));
        }
        else {
            newVerticies.Add(new Vector2(cameraLeft, cameraUp));
            newVerticies.Add(new Vector2(cameraRight, cameraUp));
        }

        _collider.SetPath(0, newVerticies.ToArray());
    }
}
