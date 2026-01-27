using UnityEngine;
using UnityEngine.AI;

public class Chase : NodeLeaf
{
    private Transform _self;
    private NavMeshAgent _agent;
    private Blackboard _blackboard;
    private float _speed;
    private float _attackRange;
    
    public Chase(Transform self, NavMeshAgent agent, Blackboard blackboard, float attackRange = 1f)
    {
        _self = self;
        _agent = agent;
        _blackboard = blackboard;
        _attackRange = attackRange;
    }
    
    public override NodeState Execute()
    {
        // Move towards the player
        Vector3 direction = (_blackboard.Target.position - _self.position);
        float distance = direction.magnitude;
        // Check if within stop distance
        if (distance <= _attackRange) {
            Debug.Log("Reached Attack Range");
            return NodeState.SUCCESS;
        }
        _agent.SetDestination(_blackboard.Target.position);
        
        return NodeState.RUNNING;
    }
}