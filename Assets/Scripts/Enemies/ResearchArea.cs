using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class ResearchArea : NodeLeaf 
    {
        private NavMeshAgent _agent;
        private Blackboard _blackboard;
        private float _radius;
        private int _pointsToResearch;
        
        private Queue<Vector3> _waypoints = new Queue<Vector3>();
        private Vector3 _currentTarget;

        public ResearchArea(NavMeshAgent agent, Blackboard blackboard, float radius, int pointsToResearch)
        {
            _agent = agent;
            _blackboard = blackboard;
            _radius = radius;
            _pointsToResearch = pointsToResearch;
        }

        private void GenerateWaypoints(Vector3 center)
        {
            _waypoints.Clear();
            for (int i = 0; i < _pointsToResearch; i++)
            {
                Vector2 circle = Random.insideUnitCircle * _radius;
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
                if (_blackboard.HasHeardNoise)
                {
                    center = _blackboard.LastHeardNoisePosition;
                }
                else if (_blackboard.LastKnownPlayerPosition != Vector3.zero)
                {
                    center = _blackboard.LastKnownPlayerPosition;
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
                _agent.SetDestination(_currentTarget);
            }
            
            if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (_waypoints.Count > 0)
                {
                    _currentTarget = _waypoints.Dequeue();
                    _agent.SetDestination(_currentTarget);
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