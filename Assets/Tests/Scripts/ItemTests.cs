using System.Collections;
using Homework;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace HomeworkTests
{
    [TestFixture]
    public sealed class ItemTests
    {
        private const string SCENE_PATH = "Assets/Tests/Scenes/ItemTest.unity";

        private Character character;
        private AmmoItem ammoItem;
        private HealthItem healthItem;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            AsyncOperation operation = EditorSceneManager
                .LoadSceneAsyncInPlayMode(SCENE_PATH, new LoadSceneParameters(LoadSceneMode.Single));

            while (!operation.isDone)
            {
                yield return null;
            }

            this.character = GameObject.FindObjectOfType<Character>();
            this.ammoItem = GameObject.FindObjectOfType<AmmoItem>();
            this.healthItem = GameObject.FindObjectOfType<HealthItem>();
        }

        [UnityTest]
        public IEnumerator PickUpAmmoTest()
        {
            //Arrange:
            this.ammoItem.Charges = 5;

            var weapon = this.character.GetComponentInChildren<Weapon>();
            weapon.Charges = 3;
            weapon.MaxCharges = 5;
            
            var moveComponent = this.character.GetComponent<MoveComponent>();
            moveComponent.MoveDirection = Vector3.left;
            moveComponent.MoveSpeed = 3.0f;

            //Act:
            yield return new WaitForSeconds(3.0f);

            //Assert:
            Assert.AreEqual(5, weapon.Charges);
            Assert.IsTrue(this.ammoItem == null);
        }

        [UnityTest]
        public IEnumerator PickUpHealthTest()
        {
            //Arrange:
            this.healthItem.HealingPoints = 5;

            var healthComponent = this.character.GetComponent<HealthComponent>();
            healthComponent.MaxHealth = 3;
            healthComponent.Health = 1;
            
            var moveComponent = this.character.GetComponent<MoveComponent>();
            moveComponent.MoveDirection = Vector3.right;
            moveComponent.MoveSpeed = 3.0f;

            //Act:
            yield return new WaitForSeconds(3.0f);

            //Assert:
            Assert.AreEqual(3, healthComponent.Health);
            Assert.IsTrue(this.healthItem == null);
        }
    }
}