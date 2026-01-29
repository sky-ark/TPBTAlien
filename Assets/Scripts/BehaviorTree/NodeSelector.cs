using System.Collections.Generic;
using Enemies.Components;

namespace BehaviorTree
{
    public class NodeSelector : NodeControl
    {
        public List<NodeBase> Children = new();

        public NodeSelector(EnemyAI enemyAI) : base(enemyAI)
        {
        }

        public override NodeState Execute()
        {
            foreach (var child in Children)
            {
                var result = child.ExecuteAndDebug();
                if (result == NodeState.SUCCESS) return NodeState.SUCCESS;
                if (result == NodeState.RUNNING) return NodeState.RUNNING;
            }

            return NodeState.FAILURE;
        }
    }
}