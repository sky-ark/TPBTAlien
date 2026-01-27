using System;

public class NodeCondition : NodeDecorator
{
    private Func<bool> _condition;

    public NodeCondition(EnemyAI enemyAI, Func<bool> condition) : base(enemyAI)
    {
        _condition = condition;
    }

    public override NodeState Execute()
    {
        if (!_condition())
        {
            return NodeState.FAILURE;
        }
        return Child.ExecuteAndDebug();
    }
}
