using UnityEngine;

public class DetectFootsteps : NodeLeaf
{
    public DetectFootsteps(EnemyAI enemyAI) : base(enemyAI)
    {
    }

    public override NodeState Execute()
    {
        Footstep[] allSteps = GameObject.FindObjectsByType<Footstep>(FindObjectsSortMode.None);

        Footstep closest = null;
        float minDist = float.MaxValue;

        foreach (var step in allSteps)
        {
            //Check if footstep has been visited
            if (EnemyAI.Blackboard.VisitedFootsteps.Contains(step.transform))
                continue;
            
            //Check if footstep is within vision range and angle
            Vector3 dir = step.transform.position - EnemyAI.transform.position;
            float dist = dir.magnitude;
            if (dist > EnemyAI.VisionRange)
                continue;

            float angle = Vector3.Angle(EnemyAI.transform.forward, dir);
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
