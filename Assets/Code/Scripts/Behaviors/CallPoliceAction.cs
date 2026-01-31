using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CallPolice", story: "Call Police to End Game", category: "Action", id: "2e1810ab0a209e052c9b4f930de6f6af")]
public partial class CallPoliceAction : Action
{
    protected override Status OnStart()
    {
        GameManager.Instance?.CallPolice();
        return Status.Success;
    }
}

