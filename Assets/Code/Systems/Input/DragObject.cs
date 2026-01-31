using UnityEngine;
using UnityEngine.InputSystem;

public class DragObject : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _distance = 5f;
    [SerializeField] private InputActionReference _pressScreen;
    [SerializeField] private InputActionReference _pressPosition;
    [SerializeField] private LayerMask _mask;

    private Rigidbody _grabbed;

    public Rigidbody Grabbed => _grabbed;
    public bool IsDragging => _grabbed != null;

    private void OnEnable()
    {
        _pressScreen.action.performed += OnPress;
        _pressScreen.action.Enable();
    }
    private void OnDisable()
    {
        _pressScreen.action.performed -= OnPress;
        _pressScreen.action.Disable();
    }

    private void FixedUpdate()
    {
        if (_grabbed == null) return;

        Vector2 screenPos = _pressPosition.action.ReadValue<Vector2>();
        Vector3 worldPos = _camera.ScreenToWorldPoint(
            new Vector3(screenPos.x, screenPos.y, _distance)
        );

        _grabbed.MovePosition(Vector3.Lerp(
            _grabbed.position,
            worldPos,
            Time.deltaTime * 10f
        ));
    }

    private void OnPress(InputAction.CallbackContext ctx)
    {
        if (!ctx.action.IsPressed()) {
            OnRelease();
            return;
        }

        Ray ray = _camera.ScreenPointToRay(
            _pressPosition.action.ReadValue<Vector2>()
        );

        if (!Physics.Raycast(ray, out RaycastHit hit, 10f, _mask))
            return;

        if (hit.rigidbody != null)
        {
            _grabbed = hit.rigidbody;
            _grabbed.useGravity = false;
        }
    }
    private void OnRelease()
    {
        if (!IsDragging) return;

        _grabbed.useGravity = true;
        _grabbed = null;
    }
}