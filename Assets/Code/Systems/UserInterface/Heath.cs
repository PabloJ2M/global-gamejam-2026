using UnityEngine;
using UnityEngine.UI;

public class Heath : MonoBehaviour
{
    [SerializeField] private Image _imageBar;
    private static float _amount = 1f;

    private void Awake() => _imageBar.fillAmount = _amount;
    private void Update() => _imageBar.fillAmount = Mathf.Lerp(_imageBar.fillAmount, _amount, Time.deltaTime);

    public void ResetHealth() => _amount = 1f;
    public void AddHealth(float percent) => _amount += percent * 0.01f;
    public void ReduceHealth(float percent) => _amount -= percent * 0.01f;
}