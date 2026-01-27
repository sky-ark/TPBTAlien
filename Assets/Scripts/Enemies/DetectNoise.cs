namespace Enemies
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