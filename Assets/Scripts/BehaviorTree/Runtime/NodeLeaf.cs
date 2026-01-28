using BehaviorTree.Runtime;
using UnityEngine;

public abstract class NodeLeaf : NodeBase
{
    protected NodeLeaf(EnemyAI enemyAI) : base(enemyAI) { }

    public abstract override NodeState Execute();

    public override NodeState ExecuteAndDebug()
    {
        NodeState state = base.ExecuteAndDebug();
        Debug.Log($"{GetType().Name} returned {state}");
        return state;
    }
}