using UnityEngine;
using UserInput;
using Chassis;
using Shooting;
using Enemy;
using Health;
using UIElements;
using System.Collections;

namespace TankGame
{
    public class GameRoot : MonoBehaviour
    {
        private const float _gameStateTimer = 1f;
        [SerializeField] private UserInputSystem _userInputSystem;
        [SerializeField] private ChassisMovement _playerChassisMovement;
        [SerializeField] private TurretMovement _playerTurretMovement;
        [SerializeField] private ShootingPoint _playerShootingPoint;
        [SerializeField] private HealthBar _playerHealthBar;
        [SerializeField] private HealthBar _baseHealthBar;
        [SerializeField] private EnemyRoot[] _enemyRoots;
        [SerializeField] private UIButtons _uIButtons;

        private void Start()
        {
            _userInputSystem.Initialize();
            for (int i = 0; i < _enemyRoots.Length; i++)
            {
                _enemyRoots[i].Initialize();
            }
            _playerChassisMovement.Initialize(_userInputSystem.UserInput);
            _playerTurretMovement.Initialize(_userInputSystem.UserInput);
            _playerShootingPoint.Initialize(_userInputSystem.UserInput);
            _playerHealthBar.Initialize();
            _baseHealthBar.Initialize();
            StartCoroutine(CheckGameState());
        }

        private IEnumerator CheckGameState()
        {
            while (true)
            {
                yield return new WaitForSeconds(_gameStateTimer);
                if (!_playerHealthBar.IsAlive() || !_baseHealthBar.IsAlive())
                {
                    _playerChassisMovement.Deactivate();
                    _playerTurretMovement.Deactivate();
                    _playerShootingPoint.Deactivate();
                    _uIButtons.ShowGameOverPanel();
                }
                bool IsGameCompleted = true;
                for (int i = 0; i < _enemyRoots.Length; i++)
                {
                    if (_enemyRoots[i] != null) IsGameCompleted = false;
                }

                if (IsGameCompleted)
                {
                    _uIButtons.ShowVictoryPanel();
                }
            }
        }

        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            { 
                _uIButtons.ShowPausePanel();
            }
        }
    }
}
