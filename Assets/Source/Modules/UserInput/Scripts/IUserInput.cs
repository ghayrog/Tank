using UnityEngine;

namespace UserInput
{
    public interface IUserInput
    {
        public float ForwardMovementAxis { get; }
        public float ChassisRotationAxis { get; }
        public Vector3 TurretRotationTarget { get; }
        public bool IsFireUp { get; }
        public bool IsFireHold { get; }
        public bool IsFireDown { get; }
        internal void ProcessInput(float deltaTime);
    }
}
