using BehaviorTree;
using Enemies.Components;
using UnityEngine;

namespace Enemies.Nodes
{
    public class Chase : NodeLeaf
    {
        public Chase(EnemyAI enemyAI) : base(enemyAI)
        {
        }

        public override NodeState Execute()
        {
            if (EnemyAI.Blackboard.Target == null)
            {
                Debug.Log("No Target to Chase");
                return NodeState.FAILURE;
            }

            if (EnemyAI.Agent == null)
            {
                Debug.Log("No NavMeshAgent found");
                return NodeState.FAILURE;
            }

            if (!EnemyAI.Agent.isOnNavMesh)
            {
                Debug.Log("Agent is not on NavMesh");
                return NodeState.FAILURE;
            }

            // Set chase speed
            EnemyAI.Agent.isStopped = false;
            EnemyAI.Agent.speed = EnemyAI.ChaseSpeed;
            EnemyAI.Agent.stoppingDistance = EnemyAI.AttackRange;

            EnemyAI.Agent.SetDestination(EnemyAI.Blackboard.Target.position);
            if (EnemyAI.Agent.remainingDistance <= EnemyAI.ReachDistance)
                // EnemyAI.Agent.isStopped = true;
                return NodeState.SUCCESS;

            return NodeState.RUNNING;
        }
    }
}