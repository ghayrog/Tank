using UnityEngine;

namespace InvisibleWall
{
    public class WallMovement : MonoBehaviour
    {
        [SerializeField] private Transform _followTarget;
        [SerializeField] private float _thresholdDistance = 5f;

        public void Initialize(Transform followTarget)
        { 
            _followTarget = followTarget;
        }
        void Update()
        {
            if (_followTarget == null) return;

            if (_followTarget.position.z - transform.position.z > _thresholdDistance)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, _followTarget.position.z - _thresholdDistance);
            }
        }
    }
}
