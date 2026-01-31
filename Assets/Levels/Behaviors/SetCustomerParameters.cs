using Unity.Behavior;
using UnityEngine;

[RequireComponent(typeof(BehaviorGraphAgent))]
public class SetCustomerParameters : MonoBehaviour
{
    private BehaviorGraphAgent _agent;

    private const string _leavePoint = "Leave Point";
    private const string _waypoints = "Waypoints";
    private const string _gameComplete = "GameComplete";

    private void Awake() => _agent = GetComponent<BehaviorGraphAgent>();
    private void Start()
    {
        var global = FindFirstObjectByType<GlobalPoints>();
        _agent.BlackboardReference.SetVariableValue(_leavePoint, global.LeavePoint);
        _agent.BlackboardReference.SetVariableValue(_waypoints, global.Waypoints);
    }

    [ContextMenu("Complete Game")]
    public void CompleteGame()
    {
        _agent.BlackboardReference.SetVariableValue(_gameComplete, true);
    }
}