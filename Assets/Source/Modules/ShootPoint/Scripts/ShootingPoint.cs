using UnityEngine;
using UnityEngine.UI;
using UserInput;

namespace Shooting
{
    public class ShootingPoint : MonoBehaviour
    {
        private const float MIN_POWER_FRACTION_TO_SHOOT = 0.1f;
        enum WeaponState
        { 
            Idle, Charging, Shooting
        }
        [SerializeField] private IUserInput _userInput;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _shootingPoint;
        [SerializeField] private Image _meterImage;
        [SerializeField] private float _minPower;
        [SerializeField] private float _maxPower;
        [SerializeField] private float _powerChangeSpeed;
        [SerializeField] private float _angleDeviation = 0f;

        [Header("Monitoring")]
        [SerializeField] private WeaponState _weaponState = WeaponState.Idle;
        [SerializeField] private float _currentPower;

        private bool _isActive;

        public void Activate()
        {
            _isActive = true;
        }

        public void Deactivate()
        {
            _isActive = false;
        }

        public void Initialize(IUserInput userInput)
        {
            _isActive = true;
            _userInput = userInput;
        }

        private void Update()
        {
            if (!_isActive || (_userInput == null)) return;

            if (_userInput.IsFireDown && _weaponState == WeaponState.Idle)
            { 
                _weaponState = WeaponState.Charging;
                _currentPower = _minPower;
            }
            if (_userInput.IsFireUp && _weaponState == WeaponState.Charging && (_currentPower-_minPower) >= MIN_POWER_FRACTION_TO_SHOOT * (_maxPower-_minPower))
            {
                _weaponState = WeaponState.Shooting;
            }
            if (!_userInput.IsFireHold && _weaponState == WeaponState.Charging)
            {
                _weaponState = WeaponState.Idle;
                if (_meterImage) _meterImage.fillAmount = 0;
            }

            switch (_weaponState) { 
                case WeaponState.Shooting:
                    Shoot();
                    _weaponState= WeaponState.Idle;
                    if (_meterImage) _meterImage.fillAmount = 0;
                    break;
                case WeaponState.Charging:
                    _currentPower = Mathf.Min(_currentPower+_powerChangeSpeed * Time.deltaTime, _maxPower);
                    if (_meterImage) _meterImage.fillAmount = (_currentPower-_minPower) / (_maxPower-_minPower);
                    break;
                default:
                    break;                    
            }
        }

        private void Shoot()
        {
            var newBullet = Instantiate(_bulletPrefab, _shootingPoint.position, _shootingPoint.rotation);
            Rigidbody newBulletRigidbody = newBullet.GetComponent<Rigidbody>();
            if (newBulletRigidbody != null)
            {
                float randomAngleDeviation = Random.Range(-_angleDeviation, _angleDeviation);
                Quaternion deviation = Quaternion.Euler(0,randomAngleDeviation,0);
                newBulletRigidbody.AddForce((deviation * newBullet.transform.forward) * _currentPower, ForceMode.Impulse);
            }
        }

    }
}
