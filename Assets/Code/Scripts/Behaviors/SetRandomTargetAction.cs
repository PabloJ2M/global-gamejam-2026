using System;
using System.Linq;
using Unity.Properties;
using UnityEngine;

namespace Unity.Behavior
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "SetRandomTarget", story: "Find Random [Target]", category: "Action/Find", id: "1b43df1eac7c2400f323bb13a38a419d")]
    public partial class SetRandomTargetAction : Action
    {
        [SerializeReference] public BlackboardVariable<Transform> Target;

        protected override Status OnStart()
        {
            var points = GameObject.FindObjectsByType<GamePoint>(FindObjectsSortMode.None).ToList();
            var point = points.Find(x => !x.IsOcupped);

            if (point == null)
                return Status.Failure;

            Target.SetValueWithoutNotify(point.transform);
            point.SetNPC();

            return Status.Success;
        }
    }
}