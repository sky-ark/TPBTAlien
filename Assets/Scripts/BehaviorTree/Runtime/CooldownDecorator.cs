using UnityEngine;

public class CooldownDecorator : NodeDecorator
{
        private float _cooldownTime;
        private float _lastExecutionTime;

        public CooldownDecorator(NodeBase child, float cooldownTime) : base(child)
        {
            _cooldownTime = cooldownTime;
            _lastExecutionTime = -cooldownTime; // Ensure it can run immediately
        }

        public override NodeState Execute()
        {
            if (Time.time - _lastExecutionTime < _cooldownTime)
                return NodeState.RUNNING;
            
            NodeState result = Child.Execute();
            if (result == NodeState.SUCCESS || result == NodeState.RUNNING)
            {
                _lastExecutionTime = Time.time;
            }
            return result;
        }
}