using System.Collections.Generic;
using BehaviorTree.Runtime;

public class NodeSelector : NodeControl
{
    public List<NodeBase> Children = new List<NodeBase>();

    public NodeSelector(EnemyAI enemyAI) : base(enemyAI)
    {
    }

    public override NodeState Execute()
    {
        foreach (NodeBase child in Children)
        {               
            var result = child.ExecuteAndDebug();
            if (result == NodeState.SUCCESS) return NodeState.SUCCESS;
            if (result == NodeState.RUNNING) return NodeState.RUNNING;

        }
        return NodeState.FAILURE;
    }
}