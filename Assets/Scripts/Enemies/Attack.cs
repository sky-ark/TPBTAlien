using UnityEngine;

namespace Enemies
{
    public class Attack : NodeLeaf
    {
        private Transform _self;
        private Blackboard _blackboard;
        private int _damage;
    
        public Attack(Transform self, Blackboard blackboard, int damage)
        {
            _self = self;
            _blackboard = blackboard;
            _damage = damage;
        }
        public override NodeState Execute()
        {
            Debug.Log("Attack!");
            Health health = _blackboard.Target.GetComponent<Health>();
            if (health == null) return NodeState.FAILURE;
            health.TakeDamage(_damage);
            return NodeState.SUCCESS;
        }
    }
}
