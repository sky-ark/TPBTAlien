using UnityEngine;
using UnityEngine.AI;

public class FollowFootsteps : NodeLeaf
{
    private NavMeshAgent _agent;
    private Blackboard _blackboard;
    private float _reachDistance;
    
    public FollowFootsteps(NavMeshAgent agent, Blackboard blackboard, float reachDistance)
    {
        _agent = agent;
        _blackboard = blackboard;
        _reachDistance = reachDistance;
    }
    public override NodeState Execute()
    {
        if (_blackboard.TargetFootstep == null) return NodeState.FAILURE;

        _agent.SetDestination(_blackboard.TargetFootstep.position);

        float dist = Vector3.Distance(_agent.transform.position, _blackboard.TargetFootstep.position);
        if (dist <= _reachDistance)
        {
            _blackboard.TargetFootstep = null;
            return NodeState.SUCCESS;
        }

        return NodeState.RUNNING;
    }
}
