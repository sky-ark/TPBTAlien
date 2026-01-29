using BehaviorTree;
using Player;
using UnityEngine;

namespace Enemies.Components
{
    public class EnemyHearing : MonoBehaviour
    {
        public Blackboard Blackboard;

        [SerializeField] private float _hearingRange = 15f;

        private void OnEnable()
        {
            NoiseEvents.OnNoiseEmitted += OnNoiseHeard;
        }

        private void OnDisable()
        {
            NoiseEvents.OnNoiseEmitted -= OnNoiseHeard;
        }

        private void OnNoiseHeard(Vector3 pos, float intensity)
        {
            var dist = Vector3.Distance(transform.position, pos);

            if (dist <= intensity && dist <= _hearingRange)
            {
                Blackboard.HasHeardNoise = true;
                Blackboard.LastHeardNoisePosition = pos;
            }
        }
    }
}