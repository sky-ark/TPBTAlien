using System;
using UnityEngine;

public class NodeCondition :NodeDecorator
{
    private Func<bool> _condition;
    public NodeCondition(NodeBase child, Func<bool> condition) : base(child)
    {
        _condition = condition;
    }
    public override NodeState Execute()
    {
        if (!_condition())
        {
            return NodeState.FAILURE;
        }
        return Child.Execute();
    }
}
