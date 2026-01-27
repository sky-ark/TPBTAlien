using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class ResearchArea : NodeLeaf 
    {
        
        private Queue<Vector3> _waypoints = new Queue<Vector3>();
        private Vector3 _currentTarget;


        public ResearchArea(EnemyAI enemyAI) : base(enemyAI)
        {
        }

        private void GenerateWaypoints(Vector3 center)
        {
            _waypoints.Clear();
            for (int i = 0; i < EnemyAI.PointsToResearch; i++)
            {
                Vector2 circle = Random.insideUnitCircle * EnemyAI.ResearchRadius;
                Vector3 point = center + new Vector3(circle.x, 0, circle.y);

                if (NavMesh.SamplePosition(point, out NavMeshHit hit, 1f, NavMesh.AllAreas))
                {
                    _waypoints.Enqueue(hit.position);
                }
            }
            

        }
        public override NodeState Execute()
        {
            if (_waypoints.Count == 0)
            {
                Vector3 center;
                if (EnemyAI.Blackboard.HasHeardNoise)
                {
                    center = EnemyAI.Blackboard.LastHeardNoisePosition;
                }
                else if (EnemyAI.Blackboard.LastKnownPlayerPosition != Vector3.zero)
                {
                    center = EnemyAI.Blackboard.LastKnownPlayerPosition;
                }
                
                else
                {
                    return NodeState.FAILURE;
                }
                GenerateWaypoints(center);
            }

            if (_currentTarget == Vector3.zero)
            {
                _currentTarget = _waypoints.Dequeue();
                EnemyAI.Agent.SetDestination(_currentTarget);
            }
            
            if (!EnemyAI.Agent.pathPending && EnemyAI.Agent.remainingDistance <= EnemyAI.Agent.stoppingDistance)
            {
                if (_waypoints.Count > 0)
                {
                    _currentTarget = _waypoints.Dequeue();
                    EnemyAI.Agent.SetDestination(_currentTarget);
                }
                else
                {
                    _currentTarget = Vector3.zero;
                    return NodeState.SUCCESS;
                }
            }
            return NodeState.RUNNING;
        }
    }
}