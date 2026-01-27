using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class InvestigateNoise : NodeLeaf
    {
        public InvestigateNoise(EnemyAI enemyAI) : base(enemyAI)
        {
        }

        public override NodeState Execute()
        {
            EnemyAI.Agent.SetDestination(EnemyAI.Blackboard.LastHeardNoisePosition);
            if(Vector3.Distance(EnemyAI.Agent.transform.position , EnemyAI.Blackboard.LastHeardNoisePosition) <= EnemyAI.ReachDistance)
            {
                EnemyAI.Blackboard.HasHeardNoise = false;
                return NodeState.SUCCESS;
            }
            return NodeState.RUNNING;
        }
    }
}