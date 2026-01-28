using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Blackboard
{
    //Player
    public Transform Target;
    public bool IsPlayerVisible = false;
    public Vector3 LastKnownPlayerPosition;
    
    //Footsteps
    public Transform TargetFootstep;
    //Visited Footsteps
    public List<Transform> VisitedFootsteps = new List<Transform>();
    
    //Research Area
    public Queue<Vector3> ResearchWaypoints = new Queue<Vector3>();
    public Vector3 ResearchCurrentTarget = Vector3.zero;
    public bool HasResearchTarget = false;
    
    //Noise
    public bool HasHeardNoise = false;
    public Vector3 LastHeardNoisePosition;
}
