using UnityEngine;

namespace Health
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _currentHealth;
        [SerializeField] private bool _isDestroyAllowed = true;
        [SerializeField] private HealthBarUI _healthBar;
        [SerializeField] private GameObject _objectToDestroy;

        private bool _isActive;

        public void Activate()
        {
            _isActive = true;
        }

        public void Deactivate()
        {
            _isActive = false;
        }

        public void Initialize()
        {
            _currentHealth = _maxHealth;
            if (_objectToDestroy == null)
                _objectToDestroy = gameObject;
            Activate();
        }

        public void TakeDamage(float damage)
        {
            if (!_isActive) return;
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            { 
                _currentHealth= 0;
                if (_isDestroyAllowed)
                {
                    Destroy(_objectToDestroy);
                    return;
                }
            }
            if (_healthBar) _healthBar.SetHealthFraction(_currentHealth/_maxHealth);
        }

        public void Repair(float repairAmount)
        {
            if (!_isActive) return;
            _currentHealth += repairAmount / _maxHealth;
            if (_currentHealth > _maxHealth) _currentHealth= _maxHealth;
            if (_healthBar) _healthBar.SetHealthFraction(_currentHealth / _maxHealth);
        }

        public bool IsAlive()
        {
            if (_isActive && _currentHealth > 0) return true;
            else return false;
        }

    }
}
