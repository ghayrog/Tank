using UnityEngine;
using Chassis;
using Shooting;
using UserInput;
using Health;

namespace Enemy
{
    public class EnemyRoot : MonoBehaviour
    {
        [SerializeField] private UserInputSystem _enemyInputSystem;
        [SerializeField] private ChassisMovement _enemyChassisMovement;
        [SerializeField] private TurretMovement _enemyTurretMovement;
        [SerializeField] private ShootingPoint _enemyShootingPoint;
        [SerializeField] private HealthBar _enemyHealthBar;

        public void Initialize()
        {
            _enemyInputSystem.Initialize();
            _enemyChassisMovement?.Initialize(_enemyInputSystem.UserInput);
            _enemyTurretMovement?.Initialize(_enemyInputSystem.UserInput);
            _enemyShootingPoint?.Initialize(_enemyInputSystem.UserInput);
            _enemyHealthBar.Initialize();
        }
    }
}
