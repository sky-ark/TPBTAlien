using UnityEngine;

namespace Player
{
    public class FootstepSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _footstepPrefab;
        [SerializeField] private float _stepDistance = 2f;
        [SerializeField] private float _soundIntensity = 5f;

        private Vector3 _lastStepPosition;

        private void Start()
        {
            _lastStepPosition = transform.position;
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, _lastStepPosition) >= _stepDistance)
            {
                SpawnStep();
                NoiseEvents.OnNoiseEmitted?.Invoke(transform.position, _soundIntensity);
                _lastStepPosition = transform.position;
            }
        }

        private void SpawnStep()
        {
            var step = Instantiate(_footstepPrefab, transform.position, _footstepPrefab.transform.rotation);
            var forwardDir = transform.forward;

            var baseRotation = _footstepPrefab.transform.rotation;

            var groundRotation = Quaternion.LookRotation(forwardDir, Vector3.up);

            step.transform.rotation = groundRotation * baseRotation;

            var fs = step.GetComponent<Footstep>();
            if (fs != null) fs.Direction = forwardDir;
        }
    }
}