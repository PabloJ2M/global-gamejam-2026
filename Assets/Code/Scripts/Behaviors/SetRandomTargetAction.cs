using System;
using Unity.Properties;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unity.Behavior
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "SetRandomTarget", story: "Find Random [Target]", category: "Action/Find", id: "1b43df1eac7c2400f323bb13a38a419d")]
    public partial class SetRandomTargetAction : Action
    {
        [SerializeReference] public BlackboardVariable<Transform> Target;

        protected override Status OnStart()
        {
            var points = GameObject.FindGameObjectsWithTag("GamePoint");
            Target.SetValueWithoutNotify(points[Random.Range(0, points.Length)].transform);
            return Status.Success;
        }
    }
}