using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameSettings;

public class RopeController : MonoBehaviour {
	private List<Vector2> _vertices;

	public GameSettings.GameSettings gameSettings;

	Camera camera;

	public List<Vector2> GetVertices() { return _vertices; }

	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera>();
		_vertices = new List<Vector2>();
	
		float cameraLeft = camera.WorldToScreenPoint(new Vector2(camera.rect.xMax, camera.rect.yMax)).x;
		float cameraRight = camera.WorldToScreenPoint(new Vector2(camera.rect.xMax, camera.rect.yMax)).x;

		int count = gameSettings.ropeVertexCount;
		float slice = (cameraRight - cameraLeft) / count;

		for (int i = 0; i < i; i++) {
			_vertices.Add(new Vector2 (cameraLeft + slice * i, 0));
		}
	}

	// Update is called once per frame
	void Update () {
	}
}
