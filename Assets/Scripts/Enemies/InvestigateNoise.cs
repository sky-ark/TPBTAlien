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
            Debug.Log("Set noise position");
            EnemyAI.Agent.SetDestination(EnemyAI.Blackboard.LastHeardNoisePosition);
            //if(Vector3.Distance(EnemyAI.Agent.transform.position , EnemyAI.Blackboard.LastHeardNoisePosition) <= EnemyAI.ReachDistance)
            if (EnemyAI.Agent.remainingDistance <= EnemyAI.ReachDistance)
            {
                return NodeState.SUCCESS;
            }
            return NodeState.RUNNING;
        }
    }
}