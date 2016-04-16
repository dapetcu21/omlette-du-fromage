using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameSettings;

public class RopeController : MonoBehaviour {
	private List<Vector2> _vertices;
	private bool _dirty;

	public GameSettings.GameSettings gameSettings;
	public bool isTop = false;

	Camera _gameCamera;
	bool _lastMouseState = false;
	Vector2 _grabStart;
	bool _grabbing;

	public List<Vector2> GetVertices() { return _vertices; }
	public bool GetDirty() { return _dirty; }

	// Use this for initialization
	void Start () {
		_gameCamera = Camera.main;
		_vertices = new List<Vector2>();

		float cameraLeft = -_gameCamera.orthographicSize * Screen.width / Screen.height;
		float cameraRight = -cameraLeft;

		int count = gameSettings.ropeVertexCount;
		float slice = (cameraRight - cameraLeft) / (count - 1);

		for (int i = 0; i < count; i++) {
			_vertices.Add(new Vector2(cameraLeft + slice * i, 0));
		}
	}

	void _OnMouseDown () {
		_grabStart = transform.InverseTransformPoint(_gameCamera.ScreenToWorldPoint(Input.mousePosition));
		_grabbing = false;

		int count = _vertices.Count;
		float grabX = _grabStart.x;
		float grabY = _grabStart.y;

		float cameraLeft = -_gameCamera.orthographicSize * Screen.width / Screen.height;
		float cameraRight = -cameraLeft;
		float slice = (cameraRight - cameraLeft) / (count - 1);
		int i = (int)Mathf.Ceil((grabX - cameraLeft) / slice);

		if (i >= 1 && i < count && _vertices[i - 1].x <= grabX && grabX < _vertices[i].x) {
			float alpha = (grabX - _vertices[i - 1].x) / (_vertices[i].x - _vertices[i - 1].x);
			float y = _vertices[i - 1].y + alpha * (_vertices[i].y - _vertices[i - 1].y);
			if (isTop ^ (grabY <= y)) {
				_grabbing = true;
			}
		}
	}

	void _OnMouseMove () {
		if (!_grabbing) { return; }

		Vector2 dragPoint = transform.InverseTransformPoint(_gameCamera.ScreenToWorldPoint(Input.mousePosition));
		float deltaY = dragPoint.y - _grabStart.y;
		_grabStart.y = dragPoint.y;

		int count = _vertices.Count;
		float bumpHorizontalScale = gameSettings.bumpHorizontalScale;

		for (int i = 0; i < count; i++) {
			Vector2 vertex = _vertices[i];
			float deltaX = (_grabStart.x - vertex.x);
			float attenuation = Mathf.Exp(-deltaX * deltaX * bumpHorizontalScale);
			vertex.y += deltaY * attenuation;
			_vertices[i] = vertex;
		}

		_dirty = true;
	}

	void _OnMouseUp () {
	}

	void FixedUpdate () {
		_dirty = false;

		bool mouseState = Input.GetMouseButton(0);
		if (_lastMouseState != mouseState) {
			_lastMouseState = mouseState;
			if (mouseState) {
				_OnMouseDown();
			} else {
				_OnMouseUp();
			}
		} else if (mouseState) {
			_OnMouseMove();
		}
	}
}
