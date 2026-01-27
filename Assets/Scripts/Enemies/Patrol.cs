using UnityEngine;
using UnityEngine.AI;

public class Patrol : NodeLeaf
{
    
    private int _currentIndex = 0;
    private bool _destinationSet;


    public Patrol(EnemyAI enemyAI) : base(enemyAI)
    {
    }

    public override NodeState Execute()
    {
        EnemyAI.Agent.isStopped = false;
        if (EnemyAI.PatrolPoints == null || EnemyAI.PatrolPoints.Length == 0)
            return NodeState.FAILURE;
        
        EnemyAI.Agent.SetDestination(EnemyAI.PatrolPoints[_currentIndex].position);
        
        // Check if reached the waypoint
        if (EnemyAI.Agent.remainingDistance <= EnemyAI.ReachDistance)
        {
            _currentIndex++;
            if(_currentIndex >= EnemyAI.PatrolPoints.Length)
                _currentIndex = 0;
        }
        return NodeState.RUNNING;
    }
}
