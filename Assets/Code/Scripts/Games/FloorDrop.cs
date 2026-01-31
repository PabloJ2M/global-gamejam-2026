using UnityEngine;
using UnityEngine.Events;

public class FloorDrop : MonoBehaviour
{
    [SerializeField] private UnityEvent _onFailure;

    private const string _tag = "Item";

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag(_tag)) return;
        Destroy(collision.gameObject);
        _onFailure.Invoke();
    }
}
