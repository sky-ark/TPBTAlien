using System;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{

    public Blackboard Blackboard;
    [SerializeField] private float _visionDistance = 10f;
    [SerializeField] private float _visionAngle = 120f;
    [SerializeField] private LayerMask _obstacleMask;
    
    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (_player == null) return;
        CheckVision();
    }

    private void CheckVision()
    {
        Vector3 dirToPlayer = _player.position - transform.position;
        float distance = dirToPlayer.magnitude;
        
        // Distance check
        if (distance > _visionDistance)
        {
            LosePlayer();
            return; 
        }
        // Angle check
        float angle = Vector3.Angle(transform.forward, dirToPlayer.normalized);
        if (angle > _visionAngle / 2f)
        {
            LosePlayer();
            return; 
        }
        // Obstacle check
        Vector3 origin = transform.position + Vector3.up * 1.5f; // Eye height
        if (Physics.Raycast(origin, dirToPlayer.normalized, distance, _obstacleMask))
        {
            LosePlayer();
            return;
        }

        SeePlayer();

    }

    private void SeePlayer()
    {
        Debug.Log("see player");
        Blackboard.Target = _player;
        Blackboard.IsPlayerVisible = true;
        Blackboard.LastKnownPlayerPosition = _player.position;
    }
    
    private void LosePlayer()
    {
        Blackboard.Target = null;
        Blackboard.IsPlayerVisible = false;
    }
}
