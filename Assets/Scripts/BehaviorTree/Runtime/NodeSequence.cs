using System.Collections.Generic;

public class NodeSequence : NodeControl
{
    public List<NodeBase> Children = new List<NodeBase>();
    public override NodeState Execute()
    {
        foreach (NodeBase child in Children)
        {
            var result = child.Execute();
            if (result == NodeState.FAILURE) return NodeState.FAILURE;
            if (result == NodeState.RUNNING) return NodeState.RUNNING;
        }
        return NodeState.SUCCESS;
    }
}