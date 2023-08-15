using UnityEngine;

namespace UserInput
{
    internal class PCInput : IUserInput
    {
        private const string Vertical = "Vertical";
        private const string Horizontal = "Horizontal";
        private const float CameraDepth = 20.0f;
        public float ForwardMovementAxis { get; private set; }
        public float ChassisRotationAxis { get; private set; }
        public Vector3 TurretRotationTarget { get; private set; }
        public bool IsFireUp { get; private set; }
        public bool IsFireHold { get; private set; }
        public bool IsFireDown { get; private set; }
        private Camera _camera;
        internal PCInput(Camera camera)
        {
            _camera = camera;
        }
        void IUserInput.ProcessInput(float deltaTime)
        {
            ForwardMovementAxis = Input.GetAxisRaw(Vertical);
            ChassisRotationAxis = Input.GetAxisRaw(Horizontal);
            IsFireUp = Input.GetMouseButtonUp(0);
            IsFireHold = Input.GetMouseButton(0);
            IsFireDown = Input.GetMouseButtonDown(0);
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = mousePosition + Vector3.forward * (_camera.nearClipPlane + CameraDepth);
            TurretRotationTarget = _camera.ScreenToWorldPoint(mousePosition);
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitData))
            {
                TurretRotationTarget = hitData.point;
            }
        }

    }
}
