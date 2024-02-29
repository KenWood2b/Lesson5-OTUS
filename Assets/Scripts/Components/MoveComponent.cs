using System;
using UnityEngine;

namespace Homework
{
    public sealed class MoveComponent : MonoBehaviour
    {
        public float MoveSpeed
        {
            get => this.moveSpeed;
            set => this.moveSpeed = value;
        }

        public Vector3 MoveDirection
        {
            get => this.moveDirection;
            set => this.moveDirection = value;
        }
        /// <summary>
        /// скорость м/с
        /// </summary>
        [SerializeField]
        private float moveSpeed;
        
        /// <summary>
        /// единичный вектор направления перемещения
        /// </summary>
        [SerializeField]
        private Vector3 moveDirection;


        private void Update()
        {
            //TODO: Реализовать покадровое перемещение через transform, используя
            //moveSpeed, moveDirection
            Vector3 delta = moveDirection * moveSpeed * Time.deltaTime;
            transform.position += delta;
        }
    }
}