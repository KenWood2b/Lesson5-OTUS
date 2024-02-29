using System;
using UnityEngine;

namespace Homework
{
    public sealed class CharacterMoveController : MonoBehaviour
    {
        [SerializeField]
        private GameObject character;
        [SerializeField]
        private MoveComponent moveComponent;

        private void Start()
        {
            moveComponent = character.GetComponent<MoveComponent>();
        }

        private void Update()
        {
            //TODO: Реализовать перемещение и поворот в нужную сторону с помощью нажатия WASD / Стрелочек на клавиатуре
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            moveComponent.MoveDirection = moveDirection;
        }
    }
}