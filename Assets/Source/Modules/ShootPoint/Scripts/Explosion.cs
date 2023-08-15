using UnityEngine;
using Health;

namespace Shooting
{
    internal class Explosion : MonoBehaviour
    {
        [SerializeField] private Transform _explosionSphere;
        [SerializeField] private float _growSpeed;
        [SerializeField] private float _growTime;
        [SerializeField] private float _explosionDamage;
        [SerializeField] private float _damageDecaySpeed;

        private void Start()
        {
            Destroy(gameObject, _growTime);
        }
        private void Update()
        {
            _explosionSphere.localScale = _explosionSphere.localScale + Vector3.one * _growSpeed * Time.deltaTime;
            _explosionDamage -= _damageDecaySpeed* Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            var health = other.GetComponent<HealthBar>();
            if (health != null)
            {
                health.TakeDamage(_explosionDamage);
            }
        }
    }
}
