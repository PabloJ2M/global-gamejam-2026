using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textUI;
    [SerializeField] private float _time;

    [SerializeField] private UnityEvent _onCompleteTimer;
    private bool _isComplete;

    private void Update()
    {
        if (_isComplete) return;

        _time -= Time.deltaTime;

        string seconds = _time.ToString("00.00");
        _textUI?.SetText($"00:{seconds}");

        if (_time > 0) return;
        _onCompleteTimer.Invoke();
        _isComplete = true;
    }
}