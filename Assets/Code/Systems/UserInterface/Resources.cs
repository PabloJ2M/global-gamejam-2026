using TMPro;
using UnityEngine;

public class Resources : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textUI;

    private static int _resources;

    private void OnEnable()
    {
        UpdateResources();
    }
    private void UpdateResources()
    {
        _textUI.SetText(_resources.ToString());
    }

    public void AddResources(int amount)
    {
        _resources += amount;
        UpdateResources();
    }
    public void RemoveResources(int amount)
    {
        _resources -= amount;
        UpdateResources();
    }
}
