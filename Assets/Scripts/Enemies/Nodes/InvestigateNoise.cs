using BehaviorTree;
using Enemies.Components;

namespace Enemies.Nodes
{
    public class InvestigateNoise : NodeLeaf
    {
        public InvestigateNoise(EnemyAI enemyAI) : base(enemyAI)
        {
        }

        public override NodeState Execute()
        {
            EnemyAI.Agent.SetDestination(EnemyAI.Blackboard.LastHeardNoisePosition);
            if (EnemyAI.Agent.remainingDistance <= EnemyAI.ReachDistance)
            {
                EnemyAI.Blackboard.HasInvestigatedNoise = true;
                return NodeState.SUCCESS;
            }

            return NodeState.RUNNING;
        }
    }
}