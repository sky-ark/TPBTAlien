using UnityEngine;

public abstract class NodeDecorator : NodeBase
{
    protected NodeBase Child;

    public NodeDecorator(NodeBase child)
    {
        Child = child;
    }
}
