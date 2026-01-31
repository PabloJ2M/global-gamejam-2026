using System;
using UnityEngine;
using Unity.Properties;

namespace Unity.Behavior
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "IsGameCompleted", story: "Wait until minigame complete [isComplete]", category: "Events", id: "8328ed1a5a7ab5397192fa9defba27ba")]
    public partial class WaitForGameCompletedAction : Action
    {
        [SerializeReference] public BlackboardVariable<bool> isComplete;

        protected override Status OnStart()
        {
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            return isComplete ? Status.Success : Status.Running;
        }

        protected override void OnEnd()
        {
        }
    }
}