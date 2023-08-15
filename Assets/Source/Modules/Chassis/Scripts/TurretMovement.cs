using UserInput;
using UnityEngine;

namespace Chassis
{
    public class TurretMovement : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private IUserInput _userInput;
        [Header("Monitoring")]
        [SerializeField] private Vector3 _targetMonitor;

        private bool _isActive;
        private Vector3 _turretTarget;

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
            _turretTarget = _userInput.TurretRotationTarget;
            _targetMonitor = _turretTarget;
            Vector3 turretDirection = new Vector3(_turretTarget.x - transform.position.x,0,_turretTarget.z-transform.position.z);
            Quaternion targetRotation = Quaternion.LookRotation(turretDirection,transform.up);
            float angle = Quaternion.Angle(transform.rotation,targetRotation);
            if (angle > 0.1f) targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime / angle);
            float angleAfterRotation = targetRotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0,angleAfterRotation,0);

        }

    }
}
