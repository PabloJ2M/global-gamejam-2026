using UnityEngine;
using UnityEngine.InputSystem;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private DragObject _dragObject;
    [SerializeField] private InputActionReference _rotateButton;

    private static readonly Vector3[] _rotationPresets = new Vector3[] {
        new(0f, 0f, 0f),
        new(0f, 180f, 0f),
        new(90f, 0f, 0f),
        new(-90f, 0f, 0f)
    };

    private int _currentIndex;

    private void OnEnable()
    {
        _rotateButton.action.performed += OnRotate;
        _rotateButton.action.Enable();
    }
    private void OnDisable()
    {
        _rotateButton.action.performed -= OnRotate;
        _rotateButton.action.Disable();
    }

    private void OnRotate(InputAction.CallbackContext ctx)
    {
        if (!_dragObject.IsDragging) return;

        _currentIndex = (_currentIndex + 1) % _rotationPresets.Length;

        _dragObject.Grabbed.freezeRotation = true;
        _dragObject.Grabbed.rotation = Quaternion.Euler(_rotationPresets[_currentIndex]);
        _dragObject.Grabbed.freezeRotation = false;
    }
}
