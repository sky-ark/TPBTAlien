using UnityEngine;
using UnityEngine.AI;

public class FollowFootsteps : NodeLeaf
{
    public FollowFootsteps(EnemyAI enemyAI) : base(enemyAI)
    {
    }

    public override NodeState Execute()
    {
        if (EnemyAI.Blackboard.TargetFootstep == null) return NodeState.FAILURE;

        EnemyAI.Agent.SetDestination(EnemyAI.Blackboard.TargetFootstep.position);

        float dist = Vector3.Distance(EnemyAI.Agent.transform.position, EnemyAI.Blackboard.TargetFootstep.position);
        if (dist <= EnemyAI.ReachDistance)
        {
            EnemyAI.Blackboard.TargetFootstep = null;
            return NodeState.SUCCESS;
        }

        return NodeState.RUNNING;
    }
}
