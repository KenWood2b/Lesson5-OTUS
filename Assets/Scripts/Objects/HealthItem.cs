using UnityEngine;

namespace Homework
{
    public sealed class HealthItem : MonoBehaviour
    {
        public int HealingPoints
        {
            get => this.healingPoints;
            set => this.healingPoints = value;
        }
        
        [SerializeField]
        private int healingPoints;
        
        private void OnTriggerEnter(Collider other)
        {
            //TODO: Реализовать подбор аптечки. 
            //При подборе кол-во здоровья добавляется персонажу
            //После подбора аптечка удаляется со сцены
            if (other.TryGetComponent(out HealthComponent healthComponent))
            {
                healthComponent.RestoreHitPoints(healingPoints);
                Destroy(gameObject);

            }
        }
    }
}