using Enemies.Components;

namespace BehaviorTree
{
    public abstract class NodeControl : NodeBase
    {
        protected NodeControl(EnemyAI enemyAI) : base(enemyAI)
        {
        }

        public abstract override NodeState Execute();
    }
}