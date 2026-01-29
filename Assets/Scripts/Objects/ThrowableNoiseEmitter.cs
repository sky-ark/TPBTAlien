using Player;
using UnityEngine;

namespace Objects
{
    public class ThrowableNoiseEmitter : MonoBehaviour
    {
        [SerializeField] private float _soundIntensity = 20f;


        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("ThrowableNoiseEmitter: Emitting noise on collision");
            NoiseEvents.OnNoiseEmitted?.Invoke(transform.position, _soundIntensity);
        }
    }
}