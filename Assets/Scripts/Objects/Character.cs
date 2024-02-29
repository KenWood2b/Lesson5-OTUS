using UnityEngine;

namespace Homework
{
    public sealed class Character : MonoBehaviour
    {
        private RotationComponent rotationComponent;
        private MoveComponent moveComponent;
        private HealthComponent healthComponent;
        bool isAlive = true;


        private void Awake()
        {
            rotationComponent = GetComponent<RotationComponent>();
            moveComponent = GetComponent<MoveComponent>();
            healthComponent = GetComponent<HealthComponent>();
        }
        private void Update()
        {
            //TODO: Реализовать вращение персонажа в том же направлении, куда и двигается

            //TODO:Реализовать условие перемещения и поворота:
            //перемещаться и вращаться можно если здоровье больше нуля, иначе перемещение и вращение не происходят
            UpdateisAlive();
            UpdateRotation();
        }

        private void UpdateRotation()
        {
            rotationComponent.RotationDirection = moveComponent.MoveDirection;
        }
        private void UpdateisAlive()
        {
            isAlive = healthComponent.Health > 0;
            moveComponent.enabled = isAlive;
            rotationComponent.enabled = isAlive;
        }
    }
}