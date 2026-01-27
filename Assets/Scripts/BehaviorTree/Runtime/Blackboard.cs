using UnityEngine;

public class Blackboard
{
    //Player
    public Transform Target;
    public bool IsPlayerVisible = false;
    public Vector3 LastKnownPlayerPosition;
    
    //Footsteps
    public Transform TargetFootstep;
    
    //Noise
    public bool HasHeardNoise = false;
    public Vector3 LastHeardNoisePosition;
}
