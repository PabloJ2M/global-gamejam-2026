using UnityEngine;
using UnityEngine.Events;

public class Scanner : MonoBehaviour
{
    [SerializeField] private Transform _point;

    [SerializeField] private float _area;
    [SerializeField] private GameObject[] _prefabs;

    [SerializeField] private UnityEvent _onSuccess;

    private GameObject _objectToDestroy;
    private int _dificulty;

    public void OnEnable()
    {
        _dificulty = Random.Range(1, 5);

        for (int i = 0; i < _dificulty; i++)
        {
            Vector3 direction = Random.insideUnitSphere; direction.y = 0;
            Instantiate(_prefabs[Random.Range(0, _prefabs.Length)], _point.position + direction.normalized * _area, Random.rotation, _point);
        }
    }
    public void OnDisable()
    {
        for (int i = _point.childCount - 1; i >= 0; i--)
            Destroy(_point.GetChild(i).gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsBarcodeFacingScanner(other.transform, transform)) return;
        _objectToDestroy = other.gameObject;
        _onSuccess.Invoke();
        Invoke(nameof(DestroyItem), 0.5f);
        //poner sonido
    }
    private void DestroyItem()
    {
        Destroy(_objectToDestroy);
    }

    public bool IsBarcodeFacingScanner(Transform barcode, Transform scanner)
    {
        float dot = Vector3.Dot(barcode.up, scanner.up);
        return dot > 0.95f;
    }
}