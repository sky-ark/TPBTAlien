public class IsPlayerVisible : NodeLeaf
{

    public IsPlayerVisible(EnemyAI enemyAI) : base(enemyAI) {}
   

    public override NodeState Execute()
    {
        return EnemyAI.Blackboard.IsPlayerVisible ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
