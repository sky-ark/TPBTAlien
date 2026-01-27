using UnityEngine;

public class DetectFootsteps : NodeLeaf
{
    private Transform _self;
    private Blackboard _blackboard;
    private float _viewDistance;
    private float _viewAngle;

    public DetectFootsteps(Transform self, Blackboard blackboard, float viewDistance = 10f, float viewAngle = 120f)
    {
        _self = self;
        _blackboard = blackboard;
        _viewDistance = viewDistance;
        _viewAngle = viewAngle;
    }
    public override NodeState Execute()
    {
        Footstep[] allSteps = GameObject.FindObjectsByType<Footstep>(FindObjectsSortMode.None);

        Footstep closest = null;
        float minDist = float.MaxValue;

        foreach (var step in allSteps)
        {
            Vector3 dir = step.transform.position - _self.position;
            float dist = dir.magnitude;
            if (dist > _viewDistance)
                continue;

            float angle = Vector3.Angle(_self.forward, dir);
            if (angle > _viewAngle / 2)
                continue;

            if (dist < minDist)
            {
                minDist = dist;
                closest = step;
            }
        }
        
        _blackboard.TargetFootstep = closest != null ? closest.transform : null;
        return closest != null ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
