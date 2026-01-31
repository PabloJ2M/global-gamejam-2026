using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _maxCapacity;
    [SerializeField] private float _spawnMinTime, _spawnMaxTime;

    [SerializeField] private Transform _point;
    [SerializeField] private GameObject[] _prefabs;

    private WaitWhile _waitMaxCapacity;

    private void Awake()
    {
        _waitMaxCapacity = new(() => _point.childCount >= _maxCapacity);
    }
    private IEnumerator Start()
    {
        yield return _waitMaxCapacity;
        yield return new WaitForSeconds(Random.Range(_spawnMinTime, _spawnMaxTime));

        Instantiate(_prefabs[Random.Range(0, _prefabs.Length)], _point.position, default, _point);
        StartCoroutine(Start());
    }
}