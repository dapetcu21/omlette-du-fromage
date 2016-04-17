using UnityEngine;
using System.Collections.Generic;

public class RopeController : MonoBehaviour {
	private List<Vector2> _vertices;
	private List<float> _animationDiff;
	private bool _dirty;

	public bool userInputEnabled { get; set; }
    public GameSettings.GameSettings gameSettings;
    public bool isTop = false;
    public List<Vector2> initialBumps = new List<Vector2>();

    Camera _gameCamera;
	bool _lastMouseState = false;
	Vector2 _grabStart;
    bool _grabbing;


	public List<Vector2> GetVertices() { return _vertices; }
	public bool GetDirty() { return _dirty; }

	// Use this for initialization
	void Start () {
        userInputEnabled = true;
		_gameCamera = Camera.main;
        _vertices = new List<Vector2>();
        _ResetBumps();
		GameplayManager.instance.AddRopeController(this);
    }

    void _ResetBumps()
    {
		float cameraLeft = -_gameCamera.orthographicSize * Screen.width / Screen.height;
		float cameraRight = -cameraLeft;

		int count = gameSettings.ropeVertexCount;
        float slice = (cameraRight - cameraLeft) / (count - 1);

		for (int i = 0; i < count; i++) {
			_vertices.Add(new Vector2(cameraLeft + slice * i, 0));
        }

        foreach (Vector2 desc in initialBumps) {
			BendRope(desc.x, desc.y);
        }
    }

    public void AnimateResetBumps()
    {
        List<Vector2> targetVertices = new List<Vector2>();
        List<Vector2> oldVertices = _vertices;
        _vertices = targetVertices;
        _ResetBumps();
        _vertices = oldVertices;

        _animationDiff = new List<float>();

        int count = gameSettings.ropeVertexCount;
        for (int i = 0; i < count; i++) {
            _animationDiff.Add(targetVertices[i].y - _vertices[i].y);
	        print(_animationDiff[i]);
        }
    }

    void _OnMouseDown()
    {
        if (!userInputEnabled) { return; }
        _grabStart = transform.InverseTransformPoint(_gameCamera.ScreenToWorldPoint(Input.mousePosition));
		_grabbing = false;

		int count = _vertices.Count;
		float grabX = _grabStart.x;
		float grabY = _grabStart.y;

		float cameraLeft = -_gameCamera.orthographicSize * Screen.width / Screen.height;
		float cameraRight = -cameraLeft;

		for (int i = 1; i < count; i++) {
			if (_vertices[i - 1].x <= grabX && grabX < _vertices[i].x) {
				float alpha = (grabX - _vertices[i - 1].x) / (_vertices[i].x - _vertices[i - 1].x);
				float y = _vertices[i - 1].y + alpha * (_vertices[i].y - _vertices[i - 1].y);
				if (isTop ^ (grabY <= y)) {
					_grabbing = true;
				}
				break;
			}
		}
    }

    public void BendRope(float positionX, float deltaY)
    {
		int count = _vertices.Count;
		float bumpHorizontalScale = gameSettings.bumpHorizontalScale;

		for (int i = 0; i < count; i++) {
			Vector2 vertex = _vertices[i];
			float deltaX = (positionX - vertex.x);
			float attenuation = Mathf.Exp(-deltaX * deltaX * bumpHorizontalScale);
			vertex.y += deltaY * attenuation;
			_vertices[i] = vertex;
		}
    }

	void _OnMouseMove () {
		if (!_grabbing) { return; }

		Vector2 dragPoint = transform.InverseTransformPoint(_gameCamera.ScreenToWorldPoint(Input.mousePosition));
		float deltaY = dragPoint.y - _grabStart.y;
		_grabStart.y = dragPoint.y;

		BendRope(dragPoint.x, deltaY);

		_dirty = true;
	}

	void _OnMouseUp () {
		_grabbing = false;
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

        if (_animationDiff == null) { return; }

        float dt = Time.deltaTime;
        int count = gameSettings.ropeVertexCount;
        for (int i = 0; i < count; i++) {
            Vector2 vertex = _vertices[i];
            float oldDiff = _animationDiff[i];
            float newDiff = MathUtil.LowPassFilter(oldDiff, 0, dt, 1.0f);

            _animationDiff[i] = newDiff;
            vertex.y += oldDiff - newDiff;
            _vertices[i] = vertex;
        }

        _dirty = true;
    }
}
