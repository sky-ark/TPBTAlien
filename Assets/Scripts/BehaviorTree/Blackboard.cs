using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    [Serializable]
    public class Blackboard
    {
        //Player
        public Transform Target;
        public bool IsPlayerVisible;
        public Vector3 LastKnownPlayerPosition;

        //Footsteps
        public Transform TargetFootstep;

        //Visited Footsteps
        public List<Transform> VisitedFootsteps = new();
        public Vector3 ResearchCurrentTarget = Vector3.zero;
        public bool HasResearchTarget;
        public bool IsResearching;

        //Noise
        public bool HasHeardNoise;
        public Vector3 LastHeardNoisePosition;
        public bool HasInvestigatedNoise;

        //Research Area
        public Queue<Vector3> ResearchWaypoints = new();
    }
}