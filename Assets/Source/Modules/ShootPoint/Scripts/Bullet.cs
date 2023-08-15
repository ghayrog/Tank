using Health;
using UnityEngine;

namespace Shooting
{
    internal class Bullet : MonoBehaviour
    {
        private const float VELOCITY_THRESHOLD = 1f;

        [SerializeField] private float _lifetime = 10f;
        [SerializeField] private float _baseDamage = 10f;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Explosion _explosionPrefab;

        private float _destroyTimer = 0f;

        private void Start()
        {
            _destroyTimer = 0f;
        }

        private void Update()
        {
            if (_rigidbody != null && _rigidbody.velocity.magnitude >= VELOCITY_THRESHOLD)
            {
                _rigidbody.MoveRotation(Quaternion.LookRotation(_rigidbody.velocity, Vector3.up));
            }

            _destroyTimer += Time.deltaTime;
            if (_destroyTimer >= _lifetime)
                Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var health = collision.gameObject.GetComponent<HealthBar>();
            if (health != null)
            {
                health.TakeDamage(_baseDamage);
            }
            Instantiate(_explosionPrefab,transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
