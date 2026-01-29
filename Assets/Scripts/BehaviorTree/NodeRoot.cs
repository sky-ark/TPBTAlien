using Enemies.Components;

namespace BehaviorTree
{
    public class NodeRoot : NodeBase
    {
        public NodeBase Child;

        public NodeRoot(EnemyAI enemyAI) : base(enemyAI)
        {
        }

        public override NodeState Execute()
        {
            return Child.Execute();
        }
    }
}