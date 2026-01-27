using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class Attack : NodeLeaf
    {
        
        public Attack(EnemyAI enemyAI) : base(enemyAI) { }

        public override NodeState Execute()
        {
            Debug.Log("Attack!");
            Health health = EnemyAI.Blackboard.Target.GetComponent<Health>();
            if (health == null) return NodeState.FAILURE;
            health.TakeDamage(EnemyAI.AttackDamage);
            EnemyAI.transform.LookAt(EnemyAI.Blackboard.Target);
            
            return NodeState.SUCCESS;
        }
    }
}
