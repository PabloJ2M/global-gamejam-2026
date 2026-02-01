using UnityEngine;

public class StaticFPSCamera : MonoBehaviour {
    
    [Header("Mouse Settings")]
    public float mouseSensitivity = 100f;

    [Header("Vertical Limits")]
    [Tooltip("Mínimo ángulo hacia abajo")] public float minPitch = -80f;
    [Tooltip("Máximo ángulo hacia arriba")]
    public float maxPitch = 80f;

    private float _pitch = 0f;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _pitch -= mouseY;
        _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);

        transform.localRotation = Quaternion.Euler(_pitch, transform.localEulerAngles.y, 0f);

        transform.Rotate(Vector3.up * mouseX, Space.World);
    }
}