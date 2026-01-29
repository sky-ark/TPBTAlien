using Enemies.Components;
using UnityEngine;

namespace BehaviorTree
{
    public class CooldownDecorator : NodeDecorator
    {
        private float _lastExecutionTime = -Mathf.Infinity;

        public CooldownDecorator(EnemyAI enemyAI) : base(enemyAI)
        {
        }

        public override NodeState Execute()
        {
            if (Time.time - _lastExecutionTime < EnemyAI.AttackCooldownTime) return NodeState.RUNNING;

            var result = Child.ExecuteAndDebug();
            if (result == NodeState.SUCCESS || result == NodeState.RUNNING) _lastExecutionTime = Time.time;
            return result;
        }
    }
}