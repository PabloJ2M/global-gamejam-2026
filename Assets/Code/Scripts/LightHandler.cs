using UnityEngine;
using UnityEngine.Events;

public class LightHandler : MonoBehaviour {
    
    [Header("Events")]
    public UnityEvent onLightOn;
    public UnityEvent onLightOff;

    [Header("Debug")]
    public KeyCode toggleKey = KeyCode.L;

    private bool _isLightOn = true;

    private void Update() {
        if(Input.GetKeyDown(toggleKey)) {
            ToggleLight();
        }
    }

    public void LightOn() {
        if(_isLightOn) return;

        _isLightOn = true;
        onLightOn?.Invoke();
    }

    public void LightOff() {
        if(!_isLightOn) return;

        _isLightOn = false;
        onLightOff?.Invoke();
    }

    public void ToggleLight() {
        if(_isLightOn)
            LightOff();
        else
            LightOn();
    }
}