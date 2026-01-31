using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EmptyChild : MonoBehaviour
{
    [SerializeField] private UnityEvent _onComplete;
    private WaitWhile _childEmpty;

    private void Awake() => _childEmpty = new(() => transform.childCount > 0);
    private void OnEnable() => StartCoroutine(CheckChildCount());
    private void OnDisable() => StopAllCoroutines();

    private IEnumerator CheckChildCount()
    {
        yield return _childEmpty;
        _onComplete.Invoke();
    }
}
