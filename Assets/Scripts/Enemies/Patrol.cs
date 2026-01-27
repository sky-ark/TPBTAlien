using UnityEngine;
using UnityEngine.AI;

public class Patrol : NodeLeaf
{
    private Transform _self;
    private Transform[] _waypoints;
    private NavMeshAgent _agent;
    private float _speed;
    private float _stopDistance;
    private int _currentIndex;
    private bool _destinationSet;
    
    public Patrol(Transform self, NavMeshAgent agent, Transform[] waypoints, float stopDistance = 0.2f)
    {
        _self = self;
        _agent = agent;
        _waypoints = waypoints;
        _stopDistance = stopDistance;
        _currentIndex = 0;
    }
    
    public override NodeState Execute()
    {
        if (_waypoints == null || _waypoints.Length == 0)
            return NodeState.FAILURE;
        
        _agent.SetDestination(_waypoints[_currentIndex].position);
        
        // Check if reached the waypoint
        if (_agent.remainingDistance <= _stopDistance)
        {
            _currentIndex++;
            if(_currentIndex >= _waypoints.Length)
                _currentIndex = 0;
        }
        return NodeState.RUNNING;
    }
}
