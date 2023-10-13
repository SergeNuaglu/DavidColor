using UnityEngine;

public class AspectRatioScaler : MonoBehaviour
{
    private Vector3 _initialScale;
    private float _lastCameraAspect;
    private Camera _camera;
    private const float Epsilon = 0.01f;

    private void Start()
    {
        _camera = Camera.main;
        _initialScale = transform.localScale;
    }

    private void Update()
    {
        float cameraAspect = _camera.aspect;

        if (Mathf.Abs(cameraAspect - _lastCameraAspect) < Epsilon)
        {
            return;
        }

        transform.localScale = new Vector3(_initialScale.x * cameraAspect, _initialScale.y, _initialScale.z);
    }
}
