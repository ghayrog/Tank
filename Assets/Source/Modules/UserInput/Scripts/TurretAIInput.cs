using UnityEngine;

namespace UserInput
{

    internal class TurretAIInput : IUserInput
    {
        private enum FireState        
        { 
            Idle, Hold, Release
        }

        private const float START_SEARCHING_DELAY = 0.2f;
        private const float DISTANCE_CORRECTION = 4f;
        public float ForwardMovementAxis { get; private set; }
        public float ChassisRotationAxis { get; private set; }
        public Vector3 TurretRotationTarget { get; private set; }
        public bool IsFireUp { get; private set; }
        public bool IsFireHold { get; private set; }
        public bool IsFireDown { get; private set; }

        private string _targetTag;
        private Transform _self;
        private Transform _currentTarget;
        private float _fireMinHold;
        private float _fireMaxHold;
        private float _shootingDistance;
        private float _holdingTimer;
        private float _holdingTimerMax;
        private float _searchingTimer = 0;
        private FireState _currentFireState = FireState.Idle;

        internal TurretAIInput(Transform self, string targetTag, float fireMinHold, float fireMaxHold, float shootingDistance)
        { 
            _self = self;
            _targetTag = targetTag;
            _fireMaxHold= fireMaxHold;
            _fireMinHold= fireMinHold;
            _shootingDistance= shootingDistance;
        }

        private Transform FindClosestTarget()
        {
            GameObject[] gameObjectsWithTag = GameObject.FindGameObjectsWithTag(_targetTag);
            if (gameObjectsWithTag.Length == 0)
            {
                //Debug.Log($"AI couldn't find any target with tag {_targetTag}");
                return null;
            }
            Transform returnTarget = gameObjectsWithTag[0].transform;
            for (int i = 1; i < gameObjectsWithTag.Length; i++)
            {
                if ((returnTarget.position - _self.position).magnitude > (gameObjectsWithTag[i].transform.position - _self.position).magnitude)
                { 
                    returnTarget = gameObjectsWithTag[i].transform;
                }
            }
            //Debug.Log($"AI successfully found a target with tag {_targetTag}: {returnTarget.position}");
            return returnTarget;
        }

        void IUserInput.ProcessInput(float deltaTime)
        {
            if (_currentTarget == null || ((_currentTarget.position-_self.position).magnitude>_shootingDistance))
            {
                IsFireDown= false;
                IsFireUp= false;
                IsFireHold= false;
                _searchingTimer += deltaTime;
                if (_searchingTimer >= START_SEARCHING_DELAY)
                {
                    _searchingTimer = 0;
                    _currentTarget = FindClosestTarget();                    
                }
                return;
            }

            TurretRotationTarget = _currentTarget.position;
            float distanceFraction = (_currentTarget.position - _self.position).magnitude / _shootingDistance;
            if (_currentFireState == FireState.Idle && distanceFraction <= 1)
            {
                _currentFireState = FireState.Hold;
                //Debug.Log("AI switched to HOLD state");
                _holdingTimer = 0;
                distanceFraction = ((_currentTarget.position - _self.position).magnitude - DISTANCE_CORRECTION) / _shootingDistance;
                _holdingTimerMax = distanceFraction  * _fireMaxHold + (1-distanceFraction) * _fireMinHold;
                IsFireDown = true;
                IsFireHold = true;
                IsFireUp= false;
                return;
            }
            if (_currentFireState == FireState.Hold)
            {
                _holdingTimer += deltaTime;
                IsFireDown = false;
                IsFireHold = true;
                if (_holdingTimer >= _holdingTimerMax)
                {
                    //Debug.Log("AI switched to RELEASE state");
                    _currentFireState = FireState.Release;
                    IsFireHold = false;
                    IsFireUp = true;
                }
                return;
            }
            if (_currentFireState == FireState.Release)
            { 
                IsFireUp= false;
                //Debug.Log("AI switched to IDLE state");
                _currentFireState = FireState.Idle;
            }
        }
    }
}
