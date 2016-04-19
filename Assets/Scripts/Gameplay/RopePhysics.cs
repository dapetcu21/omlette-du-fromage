using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RopePhysics : MonoBehaviour {

    public bool isDown;

    private PolygonCollider2D _collider;

    private RopeController ropeController;

    void Start ()
    {
        //initialize stuff
        _collider = GetComponent<PolygonCollider2D>();
        ropeController = GetComponent<RopeController>();

        _UpdateRopeVerticies();
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
        List<Vector2> currentVerticies = ropeController.GetVertices();

        List<Vector2> newVerticies = new List<Vector2>();

        float cameraUp = Camera.main.orthographicSize;
        float cameraLeft = cameraUp * Screen.width / Screen.height;
        float cameraRight = -cameraLeft;

        newVerticies.AddRange(currentVerticies);

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
