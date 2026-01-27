using System.Collections.Generic;

public class NodeSelector : NodeControl
{
    public List<NodeBase> Children = new List<NodeBase>();
    public override NodeState Execute()
    {
        foreach (NodeBase child in Children)
        {               
            var result = child.Execute();
            if (result == NodeState.SUCCESS) return NodeState.SUCCESS;
            if (result == NodeState.RUNNING) return NodeState.RUNNING;

        }
        return NodeState.FAILURE;
    }
}