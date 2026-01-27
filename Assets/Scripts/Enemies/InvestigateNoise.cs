using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class InvestigateNoise : NodeLeaf
    {
        private NavMeshAgent _agent;
        private Blackboard _blackboard;
        private float _reachDistance;

        public InvestigateNoise(NavMeshAgent agent, Blackboard blackboard, float reachDistance)
        {
            _agent = agent;
            _blackboard = blackboard;
            _reachDistance = reachDistance;
        }
        public override NodeState Execute()
        {
            _agent.SetDestination(_blackboard.LastHeardNoisePosition);
            if(Vector3.Distance(_agent.transform.position , _blackboard.LastHeardNoisePosition) <= _reachDistance)
            {
                _blackboard.HasHeardNoise = false;
                return NodeState.SUCCESS;
            }
            return NodeState.RUNNING;
        }
    }
}