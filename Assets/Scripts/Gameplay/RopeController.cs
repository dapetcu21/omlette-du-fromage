using UnityEngine;
using System.Collections.Generic;

public class RopeController : MonoBehaviour
{
    const int MOUSE_INDEX = 28423250;

    private List<Vector2> _vertices;
	private List<float> _animationDiff;
	private bool _dirty;

	public bool userInputEnabled { get; set; }
    public GameSettings.GameSettings gameSettings;
    public bool isTop = false;
    public List<Vector2> initialBumps = new List<Vector2>();

    Camera _gameCamera;
    bool _lastMouseState = false;

    Dictionary<int, Vector2> _grabPoints = new Dictionary<int, Vector2>();

    public List<Vector2> GetVertices() { return _vertices; }
	public bool GetDirty() { return _dirty; }

	// Use this for initialization
	void Start () {
        userInputEnabled = true;
		_gameCamera = Camera.main;
        _vertices = new List<Vector2>();
        _ResetBumps();
        GameplayManager.instance.AddRopeController(this);
        Input.simulateMouseWithTouches = false;
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
        }
    }

    void _OnTouchDown(int index, Vector2 touch)
    {
        if (!userInputEnabled) { return; }
        Vector2 grabStart = transform.InverseTransformPoint(_gameCamera.ScreenToWorldPoint(touch));
		bool grabbing = false;

		int count = _vertices.Count;
		float grabX = grabStart.x;
		float grabY = grabStart.y;

		float cameraLeft = -_gameCamera.orthographicSize * Screen.width / Screen.height;
		float cameraRight = -cameraLeft;

		for (int i = 1; i < count; i++) {
			if (_vertices[i - 1].x <= grabX && grabX < _vertices[i].x) {
				float alpha = (grabX - _vertices[i - 1].x) / (_vertices[i].x - _vertices[i - 1].x);
                float y = _vertices[i - 1].y + alpha * (_vertices[i].y - _vertices[i - 1].y);
                y += (isTop ? -1.0f : 1.0f) * gameSettings.ropeGrabTreshold;
                if (isTop ^ (grabY <= y)) {
					grabbing = true;
				}
				break;
			}
        }

        if (grabbing) {
            _grabPoints[index] = grabStart;
        }
    }

    void _OnTouchMove(int index, Vector2 touch)
    {
        if (!_grabPoints.ContainsKey(index)) { return; }


        Vector2 grabPoint = _grabPoints[index];
        Vector2 dragPoint = transform.InverseTransformPoint(_gameCamera.ScreenToWorldPoint(touch));
		float deltaY = dragPoint.y - grabPoint.y;
        grabPoint.y = dragPoint.y;
        _grabPoints[index] = grabPoint;

        BendRope(grabPoint.x, deltaY);

		_dirty = true;
	}

    void _OnTouchUp(int index)
    {
        _grabPoints.Remove(index);
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


	void FixedUpdate () {
		_dirty = false;

		bool mouseState = Input.GetMouseButton(0);
		if (_lastMouseState != mouseState) {
			_lastMouseState = mouseState;
			if (mouseState) {
                _OnTouchDown(MOUSE_INDEX, Input.mousePosition);
            } else {
				_OnTouchUp(MOUSE_INDEX);
			}
		} else if (mouseState) {
            _OnTouchMove(MOUSE_INDEX, Input.mousePosition);
        }

        for (int i = 0; i < Input.touchCount; i++) {
            Touch touch = Input.GetTouch(i);
            switch (touch.phase) {
                case TouchPhase.Began:
                    _OnTouchDown(touch.fingerId, touch.position);
                    break;
                case TouchPhase.Moved:
                    _OnTouchMove(touch.fingerId, touch.position);
                    break;
                case TouchPhase.Canceled:
                case TouchPhase.Ended:
                    _OnTouchUp(touch.fingerId);
                    break;
            }
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
