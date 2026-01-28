namespace Enemies
{
    public class HasInvestigatedNoise : NodeLeaf
    {
        public HasInvestigatedNoise(EnemyAI enemyAI) : base(enemyAI)
        {
        }
        public override NodeState Execute()
        {
            return EnemyAI.Blackboard.HasInvestigatedNoise ? NodeState.SUCCESS : NodeState.FAILURE;
        }
    }
}