using BehaviorTree.Runtime;

public abstract class NodeControl : NodeBase
{
    protected NodeControl(EnemyAI enemyAI) : base(enemyAI)
    {
    }

    public abstract override NodeState Execute();
}