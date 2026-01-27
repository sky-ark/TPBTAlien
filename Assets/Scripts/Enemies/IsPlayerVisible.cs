public class IsPlayerVisible : NodeLeaf
{
    private Blackboard _blackboard;
    
    public IsPlayerVisible(Blackboard blackboard)
    {
        _blackboard = blackboard;
    }

    public override NodeState Execute()
    {
        return _blackboard.IsPlayerVisible ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
