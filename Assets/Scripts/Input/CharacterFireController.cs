using System;
using UnityEngine;

namespace Homework
{
    public sealed class CharacterFireController : MonoBehaviour
    {
        [SerializeField]
        private GameObject character;
        [SerializeField]
        private Weapon weapon;

        private void Start()
        {
            if (character != null)
                weapon = character.GetComponentInChildren<Weapon>();
        }
        private void Update()
        {
            //TODO: Реализовать выстрел пулей при нажатии Space на клавиатуре
           if (Input.GetKeyDown(KeyCode.Space))
           {
                    weapon.Fire();
            }
            
        }
    }
}