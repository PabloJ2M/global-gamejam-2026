using UnityEngine;

public class FaceCameraDirection : MonoBehaviour
{
    private Transform _transform, _camera;

    private void Awake() => _camera = Camera.main.transform;
    private void Start() => _transform = transform;
    private void Update() => _transform.forward = _camera.forward;
}