using BehaviorTree.Runtime;
using UnityEngine;

public abstract class NodeDecorator : NodeBase
{
    public NodeBase Child;

    protected NodeDecorator(EnemyAI enemyAI) : base(enemyAI)
    {
    }
}
