using Enemies.Components;

namespace BehaviorTree
{
    public abstract class NodeDecorator : NodeBase
    {
        public NodeBase Child;

        protected NodeDecorator(EnemyAI enemyAI) : base(enemyAI)
        {
        }
    }
}