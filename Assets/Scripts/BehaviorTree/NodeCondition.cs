using System;
using Enemies.Components;

namespace BehaviorTree
{
    public class NodeCondition : NodeDecorator
    {
        private readonly Func<bool> _condition;

        public NodeCondition(EnemyAI enemyAI, Func<bool> condition) : base(enemyAI)
        {
            _condition = condition;
        }

        public override NodeState Execute()
        {
            if (!_condition()) return NodeState.FAILURE;
            return Child.ExecuteAndDebug();
        }
    }
}