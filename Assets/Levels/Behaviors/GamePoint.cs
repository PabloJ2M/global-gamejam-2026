using UnityEngine;

public class GamePoint : MonoBehaviour
{
    private bool _isOcupped;

    public bool IsOcupped => _isOcupped;

    public void SetNPC()
    {
        _isOcupped = true;
    }
    public void LeaveNPC()
    {
        _isOcupped = false;
    }
}
