using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    private int _currentHealth;
    [SerializeField] private Transform _spawnPoint;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        
        if (_currentHealth <= 0)
        { 
            Die();
        }
    }

    private void Die()
    { 
        Debug.Log("Player has died");
        // Notify all enemy sensors to lose the player
        EnemySensor[] sensors = FindObjectsOfType<EnemySensor>();
        foreach (var sensor in sensors)
            sensor.ForceLosePlayer();
        
        Respawn();
    }
    
    private void Respawn()
    {
        _currentHealth = _maxHealth;
        transform.position = _spawnPoint.position; 
    }
}
