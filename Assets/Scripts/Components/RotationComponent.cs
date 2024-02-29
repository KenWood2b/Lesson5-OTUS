using System;
using UnityEngine;

namespace Homework
{
    public sealed class RotationComponent : MonoBehaviour
    {
        public float RotationSpeed
        {
            get => this.rotationSpeed;
            set => this.rotationSpeed = value;
        }

        public Vector3 RotationDirection
        {
            get => this.rotationDirection;
            set => this.rotationDirection = value;
        }
        /// <summary>
        /// Угловая скорость градусов в секунду
        /// </summary>
        [SerializeField]
        private float rotationSpeed;

        /// <summary>
        /// Еденичный вектор направления, куда необходимо направить Character
        /// </summary>
        [SerializeField]
        private Vector3 rotationDirection;


        private void Update()
        {
            //TODO: Реализовать покадровый поворот через transform с помощью методов
            //Quaternion.RotateTowards, Quaternion.LookRotation, иcпользуя
            //параметры rotationSpeed, rotationDirection
            //Если направление перемещения ноль, то поворот не происходит
            if (rotationDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(rotationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}