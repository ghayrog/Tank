using UserInput;
using UnityEngine;

namespace Chassis
{
    public class ChassisMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationPower;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _rotationTarget;
        [SerializeField] private IUserInput _userInput;

        private bool _isActive;
        private float _forwardAxis;
        private float _rotationAxis;


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
            _isActive= true;
            _userInput = userInput;
        }

        private void Update()
        {
            if (!_isActive || (_rigidbody == null) || (_userInput == null)) return;
            _forwardAxis = _userInput.ForwardMovementAxis;
            _rotationAxis = _userInput.ChassisRotationAxis;
        }

        private void FixedUpdate()
        {
            if (!_isActive || (_rigidbody == null) || (_userInput == null)) return;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.transform.position = new Vector3(_rigidbody.transform.position.x, 0, _rigidbody.transform.position.z);
            //_rigidbody.transform.rotation = Quaternion.identity;

            //_rigidbody.velocity += _rigidbody.transform.forward * _forwardAxis * _speed;
            _rigidbody.velocity = (Vector3.forward * _forwardAxis + Vector3.right * _rotationAxis).normalized * _speed;
            _rigidbody.rotation= Quaternion.identity;
            if (_rigidbody.velocity.magnitude > 0.1f)
            {
                float angle = Mathf.Abs(_rotationTarget.rotation.eulerAngles.y - Quaternion.LookRotation(_rigidbody.velocity, Vector3.up).eulerAngles.y);
                Quaternion rotation = Quaternion.Slerp(_rotationTarget.rotation, Quaternion.LookRotation(_rigidbody.velocity, Vector3.up), _rotationPower * Time.fixedDeltaTime / angle);
                _rotationTarget.localRotation = Quaternion.AngleAxis(rotation.eulerAngles.y, Vector3.up);
            }

            //float angleAfterRotation = _rigidbody.transform.rotation.eulerAngles.y + _rotationPower * _forwardAxis * Time.fixedDeltaTime * _rotationAxis;
            //_rigidbody.transform.rotation = Quaternion.Euler(0, angleAfterRotation, 0);
        }
    }
}
