using UnityEngine;

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
        GameObject step = Instantiate(_footstepPrefab, transform.position, _footstepPrefab.transform.rotation);
        Vector3 forwardDir = transform.forward;
        step.transform.rotation = Quaternion.LookRotation(forwardDir, Vector3.up);
        
        Footstep fs = step.GetComponent<Footstep>();
        if (fs != null)
        {
            fs.Direction = forwardDir;
        }
    }
}
