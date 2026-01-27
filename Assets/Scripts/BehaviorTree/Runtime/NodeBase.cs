using UnityEngine;

namespace BehaviorTree.Runtime
{
    public abstract class NodeBase
    {
        protected EnemyAI EnemyAI;

        protected NodeBase(EnemyAI enemyAI)
        {
            EnemyAI = enemyAI;
        }

        public NodeState ExecuteAndDebug() {
            NodeState state = Execute();
            Debug.Log($"{GetType().Name} returned {state}");
            return state;
        }
    
        public abstract NodeState Execute();
    }
}