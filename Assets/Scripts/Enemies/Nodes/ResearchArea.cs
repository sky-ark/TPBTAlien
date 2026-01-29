using BehaviorTree;
using Enemies.Components;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Nodes
{
    public class ResearchArea : NodeLeaf
    {
        public ResearchArea(EnemyAI enemyAI) : base(enemyAI)
        {
        }

        private void GenerateWaypoints(Vector3 center)
        {
            EnemyAI.Blackboard.ResearchWaypoints.Clear(); // clear previous waypoints
            for (var i = 0; i < EnemyAI.PointsToResearch; i++)
            {
                var circle = Random.insideUnitCircle * EnemyAI.ResearchRadius;
                var point = center + new Vector3(circle.x, 0, circle.y);

                if (NavMesh.SamplePosition(point, out var hit, 1f, NavMesh.AllAreas))
                    EnemyAI.Blackboard.ResearchWaypoints.Enqueue(hit.position);
            }
        }

        public override NodeState Execute()
        {
            if (!EnemyAI.Blackboard.IsResearching)
            {
                Vector3 center;
                if (EnemyAI.Blackboard.LastHeardNoisePosition != Vector3.zero)
                    center = EnemyAI.Blackboard.LastHeardNoisePosition;
                else if (EnemyAI.Blackboard.LastKnownPlayerPosition != Vector3.zero)
                    center = EnemyAI.Blackboard.LastKnownPlayerPosition;

                else
                    return NodeState.FAILURE;
                Debug.Log("Generating research waypoints");
                GenerateWaypoints(center);
                EnemyAI.Blackboard.IsResearching = true;
            }

            //If no current target, dequeue the next waypoint
            if (!EnemyAI.Blackboard.HasResearchTarget)
            {
                EnemyAI.Blackboard.ResearchCurrentTarget = EnemyAI.Blackboard.ResearchWaypoints.Dequeue();
                Debug.Log("Set research position");
                EnemyAI.Agent.SetDestination(EnemyAI.Blackboard.ResearchCurrentTarget);
                EnemyAI.Blackboard.HasResearchTarget = true;
            }

            //
            if (Vector3.Distance(EnemyAI.transform.position, EnemyAI.Blackboard.ResearchCurrentTarget) <=
                EnemyAI.ReachDistance)
            {
                if (EnemyAI.Blackboard.ResearchWaypoints.Count > 0)
                {
                    EnemyAI.Blackboard.ResearchCurrentTarget = EnemyAI.Blackboard.ResearchWaypoints.Dequeue();
                    Debug.Log($"Researching next point at {EnemyAI.Blackboard.ResearchCurrentTarget}");
                    EnemyAI.Agent.SetDestination(EnemyAI.Blackboard.ResearchCurrentTarget);
                }
                else
                {
                    EnemyAI.Blackboard.ResearchCurrentTarget = Vector3.zero;
                    EnemyAI.Blackboard.HasResearchTarget = false;
                    EnemyAI.Blackboard.HasHeardNoise = false;
                    EnemyAI.Blackboard.ResearchWaypoints.Clear();
                    EnemyAI.Blackboard.HasInvestigatedNoise = false;
                    EnemyAI.Blackboard.IsResearching = false;
                    return NodeState.SUCCESS;
                }
            }

            return NodeState.RUNNING;
        }
    }
}