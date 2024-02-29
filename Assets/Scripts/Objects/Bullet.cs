using UnityEngine;

namespace Homework
{
    public sealed class Bullet : MonoBehaviour
    {
        public int Damage
        {
            get => this.damage;
            set => this.damage = value;
        }

        [SerializeField]
        private int damage;
        
        private void OnTriggerEnter(Collider other)
        {
            //TODO: Реализовать нанесение урона цели при попадание пули в Character.
            //После нанесения урона пуля уничтожается
            HealthComponent healthComponent = other.gameObject.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(damage);
            }

            Destroy(gameObject);
        }

    }
}