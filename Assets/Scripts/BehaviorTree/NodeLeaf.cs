using Enemies.Components;

namespace BehaviorTree
{
    public abstract class NodeLeaf : NodeBase
    {
        protected NodeLeaf(EnemyAI enemyAI) : base(enemyAI)
        {
        }

        public abstract override NodeState Execute();

        public override NodeState ExecuteAndDebug()
        {
            var state = base.ExecuteAndDebug();
            //Debug.Log($"{GetType().Name} returned {state}");
            return state;
        }
    }
}