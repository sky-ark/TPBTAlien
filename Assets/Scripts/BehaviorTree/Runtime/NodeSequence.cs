using System.Collections.Generic;
using BehaviorTree.Runtime;

public class NodeSequence : NodeControl
{
    public List<NodeBase> Children = new List<NodeBase>();

    public NodeSequence(EnemyAI enemyAI) : base(enemyAI)
    {
    }

    public override NodeState Execute()
    {
        foreach (NodeBase child in Children)
        {
            var result = child.ExecuteAndDebug();
            if (result == NodeState.FAILURE) return NodeState.FAILURE;
            if (result == NodeState.RUNNING) return NodeState.RUNNING;
        }
        return NodeState.SUCCESS;
    }
}