using BehaviorTree;
using Enemies.Components;

namespace Enemies.Nodes
{
    public class DetectNoise : NodeLeaf
    {
        public DetectNoise(EnemyAI enemyAI) : base(enemyAI)
        {
        }

        public override NodeState Execute()
        {
            return EnemyAI.Blackboard.HasHeardNoise ? NodeState.SUCCESS : NodeState.FAILURE;
        }
    }
}