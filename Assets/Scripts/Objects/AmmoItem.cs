using UnityEngine;

namespace Homework
{
    public sealed class AmmoItem : MonoBehaviour
    {
        public int Charges
        {
            get => this.charges;
            set => this.charges = value;
        }

        [SerializeField]
        private int charges;

        private void OnTriggerEnter(Collider other)
        {
            //TODO:
            //Реализовать подбор патронов
            //При подборе кол-во патронов добавляется к оружию
            //После подбора предмет удаляется со сцены
            Weapon weapon = other.GetComponentInChildren<Weapon>();
            if (weapon != null)
               {
                weapon.RestoreCharges(charges);
                Destroy(gameObject);
               }
        }
    }
}