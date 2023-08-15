using UnityEngine;

namespace Health
{
    internal class Repairable : MonoBehaviour
    {
        private const string REPAIR_TAG = "RepairZone";
        [SerializeField] private float _repairRate = 0.1f;
        [SerializeField] private HealthBar _healthBar;

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(REPAIR_TAG))
            {
                _healthBar.Repair(_repairRate * Time.deltaTime);
            }
        }
    }
}
