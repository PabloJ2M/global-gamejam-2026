using Unity.Behavior;
using UnityEngine;

public class DetectBody : MonoBehaviour
{
    private const string _state = "State";
    private const string _deathBody = "Finish";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_deathBody)) return;
        var agent = GetComponent<BehaviorGraphAgent>();

        agent.SetVariableValue(_state, States.DetectBody);
        agent.Restart();
    }
}