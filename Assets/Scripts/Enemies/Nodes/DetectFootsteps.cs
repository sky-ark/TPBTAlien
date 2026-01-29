using BehaviorTree;
using Enemies.Components;
using Player;
using UnityEngine;

namespace Enemies.Nodes
{
    public class DetectFootsteps : NodeLeaf
    {
        public DetectFootsteps(EnemyAI enemyAI) : base(enemyAI)
        {
        }

        public override NodeState Execute()
        {
            var allSteps = GameObject.FindObjectsByType<Footstep>(FindObjectsSortMode.None);

            Footstep closest = null;
            var minDist = float.MaxValue;

            foreach (var step in allSteps)
            {
                //Check if footstep has been visited
                if (EnemyAI.Blackboard.VisitedFootsteps.Contains(step.transform))
                    continue;

                //Check if footstep is within vision range and angle
                var dir = step.transform.position - EnemyAI.transform.position;
                var dist = dir.magnitude;
                if (dist > EnemyAI.VisionRange)
                    continue;

                var angle = Vector3.Angle(EnemyAI.transform.forward, dir);
                if (angle > EnemyAI.VisionAngle / 2)
                    continue;

                if (dist < minDist)
                {
                    minDist = dist;
                    closest = step;
                }
            }

            EnemyAI.Blackboard.TargetFootstep = closest != null ? closest.transform : null;
            return closest != null ? NodeState.SUCCESS : NodeState.FAILURE;
        }
    }
}