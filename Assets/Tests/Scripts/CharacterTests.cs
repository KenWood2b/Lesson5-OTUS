using System;
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
    public sealed class CharacterTests
    {
        private const string SCENE_PATH = "Assets/Tests/Scenes/CharacterTest.unity";

        private GameObject character;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            AsyncOperation operation = EditorSceneManager
                .LoadSceneAsyncInPlayMode(SCENE_PATH, new LoadSceneParameters(LoadSceneMode.Single));

            while (!operation.isDone)
            {
                yield return null;
            }

            this.character = GameObject.FindGameObjectWithTag("Player");
        }

        [UnityTest]
        public IEnumerator DamageTest()
        {
            //Arange:
            var healthComponent = this.character.GetComponent<HealthComponent>();
            if (healthComponent == null)
            {
                throw new Exception("Expected HealthComponent on Character");
            }
            
            healthComponent.Health = 10;

            //Act:
            healthComponent.TakeDamage(5);

            //Assert:
            Assert.AreEqual(healthComponent.Health, 5);

            yield return new WaitForFixedUpdate();
            Assert.IsTrue(this.character.activeSelf);
        }

        [UnityTest]
        public IEnumerator RestoreHealthTest()
        {
            //Arrange
            var healthComponent = this.character.GetComponent<HealthComponent>();
            if (healthComponent == null)
            {
                throw new Exception("Expected HealthComponent on Character");
            }
            
            healthComponent.MaxHealth = 10;
            healthComponent.Health = 0;
            this.character.SetActive(false);

            //Act:
            healthComponent.RestoreHitPoints(5);

            //Assert:
            Assert.AreEqual(healthComponent.Health, 5);

            yield return new WaitForSeconds(1);

            //Act:
            healthComponent.RestoreHitPoints(20);

            //Assert:
            Assert.AreEqual(healthComponent.Health, 10);
        }

        [UnityTest]
        public IEnumerator MoveForwardTest()
        {
            //Arange:
            var transform = this.character.transform;
            Vector3 startPosition = Vector3.zero;
            transform.position = startPosition;
            Vector3 direction = Vector3.forward;
            
            //Act:
            const float moveSpeed = 5;
            const float moveFrames = 100;

            var moveComponent = this.character.GetComponent<MoveComponent>();
            if (moveComponent == null)
            {
                throw new Exception("Expected MoveComponent on Character");
            }
            
            moveComponent.MoveSpeed = moveSpeed;
            moveComponent.MoveDirection = direction;

            float timeStart = Time.time;
            for (int i = 0; i < moveFrames; i++)
            {
                yield return null;
            }
            float deltaTime = Time.time - timeStart;
            
            //Assert:
            Vector3 diff = transform.position - (startPosition + direction * moveSpeed * deltaTime);
            Assert.AreEqual(diff.magnitude, 0, 1e-2);
        }


        [UnityTest]
        public IEnumerator MoveDiagonalTest()
        {
            //Arange:
            var transform = this.character.transform;
            Vector3 startPosition = Vector3.zero;
            transform.position = startPosition;
            Vector3 direction = new Vector3(-1, 0, -1);
            
            //Act:
            const float moveSpeed = 5;
            const float moveFrames = 100;

            var moveComponent = this.character.GetComponent<MoveComponent>();
            if (moveComponent == null)
            {
                throw new Exception("Expected MoveComponent on Character");
            }
            
            moveComponent.MoveSpeed = moveSpeed;
            moveComponent.MoveDirection = direction;

            float timeStart = Time.time;
            for (int i = 0; i < moveFrames; i++)
            {
                yield return null;
            }
            float deltaTime = Time.time - timeStart;

            //Assert:
            Vector3 diff = transform.position - (startPosition + direction * moveSpeed * deltaTime);
            Assert.AreEqual(diff.magnitude, 0, 1e-2);
        }

        [UnityTest]
        public IEnumerator IntegrationTest()
        {
            //Arange:
            var transform = this.character.transform;
            var healthComponent = this.character.GetComponent<HealthComponent>();
            var moveComponent = this.character.GetComponent<MoveComponent>();
            var rotationComponent = this.character.GetComponent<RotationComponent>();
            
            const float moveFrames = 50;

            Vector3 direction = new Vector3(-1, 0, -1);
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            
            moveComponent.MoveSpeed = 5;
            moveComponent.MoveDirection = direction;
            rotationComponent.RotationSpeed = 5;
            
            healthComponent.MaxHealth = 1;
            healthComponent.Health = 1;
            
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            
            for (int i = 0; i < moveFrames; i++)
            {
                yield return null;
            }
            Vector3 positionFirst = transform.position;
            Quaternion rotationFirst = transform.rotation;

            healthComponent.Health = 0;
            yield return null;
            
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            
            for (int i = 0; i < moveFrames; i++)
            {
                yield return null;
            }
            Vector3 positionSecond = transform.position;
            Quaternion rotationSecond = transform.rotation;
            
            //Assert:
            Assert.True(positionSecond == Vector3.zero && positionFirst != positionSecond);
            Assert.True(rotationSecond == Quaternion.identity && rotationFirst != rotationSecond);
        }
    }
}