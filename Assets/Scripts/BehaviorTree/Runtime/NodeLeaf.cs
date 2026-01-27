using BehaviorTree.Runtime;

public abstract class NodeLeaf : NodeBase
{
    protected NodeLeaf(EnemyAI enemyAI) : base(enemyAI) { }

    public abstract override NodeState Execute();
}