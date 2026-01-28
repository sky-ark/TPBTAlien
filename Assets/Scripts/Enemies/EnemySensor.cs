using System;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{

    public Blackboard Blackboard;
    [SerializeField] private float _visionDistance = 10f;
    [SerializeField] private float _visionAngle = 120f;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private float _memoryStale = 2f;

    
    private Transform _player;
    private float _timer;

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
            LoseSightTick();
            return;
        }
        // Angle check
        float angle = Vector3.Angle(transform.forward, dirToPlayer.normalized);
        if (angle > _visionAngle / 2f)
        {
            LoseSightTick();
            return;
        }
        // Obstacle check
        Vector3 origin = transform.position + Vector3.up * 1.5f; // Eye height
        if (Physics.Raycast(origin, dirToPlayer.normalized, distance, _obstacleMask))
        {
            LoseSightTick();
            return;
        }
        
        
        SeePlayer();
        _timer = 0;
    }

    private void LoseSightTick()
    {
        _timer += Time.deltaTime;
        if (_timer >= _memoryStale)
            LosePlayer();
    }

    private void SeePlayer()
    {
        Blackboard.Target = _player;
        Blackboard.IsPlayerVisible = true;
        Blackboard.LastKnownPlayerPosition = _player.position;
    }
    
    private void LosePlayer()
    {
        Blackboard.Target = null;
        Blackboard.IsPlayerVisible = false;
    }

    public void ForceLosePlayer()
    {
        _timer = _memoryStale;
        LosePlayer();
    }
}
