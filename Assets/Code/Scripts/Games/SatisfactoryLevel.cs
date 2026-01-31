using UnityEngine;
using UnityEngine.Events;

public class SatisfactoryLevel : MonoBehaviour
{
    [SerializeField] private float _threshold;
    [SerializeField] private UnityEvent _onFailure, _onClose;

    public void SetLevel(float value)
    {
        if (value < _threshold)
            _onFailure.Invoke();

        _onClose.Invoke();
    }
}