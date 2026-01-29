using Enemies.Components;

namespace BehaviorTree
{
    public abstract class NodeBase
    {
        protected EnemyAI EnemyAI;

        protected NodeBase(EnemyAI enemyAI)
        {
            EnemyAI = enemyAI;
        }

        public virtual NodeState ExecuteAndDebug()
        {
            return Execute();
        }

        public abstract NodeState Execute();
    }
}