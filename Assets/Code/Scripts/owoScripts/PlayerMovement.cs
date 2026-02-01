using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    [SerializeField] private Transform mask;
    [SerializeField] private float growAmount = 0.2f;
    private Rigidbody rb;
    private Vector2 _input;

    private void Awake() => rb = GetComponent<Rigidbody>();
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(_input.x * speed, rb.linearVelocity.y, _input.y * speed);
    }

    internal void OnMove(InputValue value)
    {
        _input = value.Get<Vector2>();
    }
    public void AddMask()
    {
        Vector3 scale = mask.localScale;
        scale.z += growAmount;
        mask.localScale = scale;

        Vector3 pos = mask.localPosition;
        pos.z += growAmount / 2f;
        mask.localPosition = pos;
    }
}

