using Chassis;
using Health;
using Shooting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UserInput;

namespace Enemy
{
    public class DestructableBrick : MonoBehaviour
    {
        [SerializeField] private HealthBar _enemyHealthBar;

        private void Start()
        {
            _enemyHealthBar.Initialize();
        }
    }
}
