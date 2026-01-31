using UnityEngine;
using UnityEngine.UI;

public class SatisfactoryBar : MonoBehaviour
{
    [SerializeField] private Image _bar;
    [SerializeField] private float _minRandomSpeed, _maxRandomSpeed;
    [SerializeField] private SatisfactoryLevel _level;

    private float _currentSpeed;
    private bool _completeBar;

    private void OnEnable()
    {
        _completeBar = false;
        _bar.fillAmount = 1f;
        _currentSpeed = Random.Range(_minRandomSpeed, _maxRandomSpeed);
    }
    private void Update()
    {
        if (_completeBar) return;

        _bar.fillAmount -= _currentSpeed * Time.deltaTime * 0.05f;
        
        if (_bar.fillAmount <= 0)
        {
            _completeBar = true;
            _level.SetLevel(0f);
        }
    }

    public void CompleteGame()
    {
        _level.SetLevel(_bar.fillAmount);
        _completeBar = true;
    }
}