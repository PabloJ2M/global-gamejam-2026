using System.Collections.Generic;
using UnityEngine;

public class GlobalPoints : MonoBehaviour
{
    [SerializeField] private Transform _leavePoint;
    [SerializeField] private List<GameObject> _waypoints;

    public Transform LeavePoint => _leavePoint;
    public List<GameObject> Waypoints => _waypoints;
}
