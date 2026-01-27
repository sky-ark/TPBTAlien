public class NodeRoot : NodeBase
{
    public NodeBase Child;
    public override NodeState Execute()
    {
        return Child.Execute();
    }
}