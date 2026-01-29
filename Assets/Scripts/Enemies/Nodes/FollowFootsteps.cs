using BehaviorTree;
using Enemies.Components;
using UnityEngine;

namespace Enemies.Nodes
{
    public class FollowFootsteps : NodeLeaf
    {
        public FollowFootsteps(EnemyAI enemyAI) : base(enemyAI)
        {
        }

        public override NodeState Execute()
        {
            if (EnemyAI.Blackboard.TargetFootstep == null) return NodeState.FAILURE;

            EnemyAI.Agent.SetDestination(EnemyAI.Blackboard.TargetFootstep.position);

            var dist = Vector3.Distance(EnemyAI.transform.position, EnemyAI.Blackboard.TargetFootstep.position);
            Debug.Log($"Distance to footstep: {dist}");
            //if (dist <= EnemyAI.ReachDistance)
            if (EnemyAI.Agent.remainingDistance <= EnemyAI.ReachDistance)
            {
                // Add visited footstep to the list
                if (!EnemyAI.Blackboard.VisitedFootsteps.Contains(EnemyAI.Blackboard.TargetFootstep))
                    EnemyAI.Blackboard.VisitedFootsteps.Add(EnemyAI.Blackboard.TargetFootstep);

                EnemyAI.Blackboard.TargetFootstep = null;
                return NodeState.SUCCESS;
            }

            return NodeState.RUNNING;
        }
    }
}