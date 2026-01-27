using UnityEngine;

public class CooldownDecorator : NodeDecorator
{
    private float _lastExecutionTime = -Mathf.Infinity;
    public CooldownDecorator(EnemyAI enemyAI) : base(enemyAI)
    {
    }

    public override NodeState Execute()
        {
            if (Time.time - _lastExecutionTime < EnemyAI.AttackCooldownTime) {
                Debug.Log("CooldownDecorator: On cooldown, skipping execution.");
                return NodeState.RUNNING;
            }
            
            NodeState result = Child.ExecuteAndDebug();
            if (result == NodeState.SUCCESS || result == NodeState.RUNNING)
            {
                _lastExecutionTime = Time.time;
                Debug.Log("CooldownDecorator: Action executed, starting cooldown.");
            }
            return result;
        }
}