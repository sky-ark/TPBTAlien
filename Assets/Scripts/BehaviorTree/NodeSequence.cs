using System.Collections.Generic;
using Enemies.Components;

namespace BehaviorTree
{
    public class NodeSequence : NodeControl
    {
        public List<NodeBase> Children = new();

        public NodeSequence(EnemyAI enemyAI) : base(enemyAI)
        {
        }

        public override NodeState Execute()
        {
            foreach (var child in Children)
            {
                var result = child.ExecuteAndDebug();
                if (result == NodeState.FAILURE) return NodeState.FAILURE;
                if (result == NodeState.RUNNING) return NodeState.RUNNING;
            }

            return NodeState.SUCCESS;
        }
    }
}