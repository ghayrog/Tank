using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Health
{
    internal class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private Image _foregroundHealth;

        private Quaternion initialRotation;
        internal void SetHealthFraction(float healthFraction)
        {
            if (_foregroundHealth) _foregroundHealth.fillAmount = healthFraction;
        }

        private void Update()
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}
