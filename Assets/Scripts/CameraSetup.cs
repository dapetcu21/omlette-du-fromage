using UnityEngine;

public class CameraSetup : MonoBehaviour {
    void Awake()
    {
        Camera camera = Camera.main;

        float designAspect = 16.0f / 9.0f;
        float aspect = (float) Screen.width / Screen.height;

        if (aspect < designAspect) {
            camera.orthographicSize *= designAspect / aspect;
        }
    }
}
