using BehaviorTree;
using Enemies.Components;
using Player;
using UnityEngine;

namespace Enemies.Nodes
{
    public class Attack : NodeLeaf
    {
        public Attack(EnemyAI enemyAI) : base(enemyAI)
        {
        }

        public override NodeState Execute()
        {
            Debug.Log("Attack!");
            var health = EnemyAI.Blackboard.Target.GetComponent<Health>();
            if (health == null) return NodeState.FAILURE;
            health.TakeDamage(EnemyAI.AttackDamage);
            EnemyAI.transform.LookAt(EnemyAI.Blackboard.Target);

            return NodeState.SUCCESS;
        }
    }
}