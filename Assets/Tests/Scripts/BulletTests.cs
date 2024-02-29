using System;
using System.Collections;
using Homework;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace HomeworkTests
{
    [TestFixture]
    public sealed class BulletTests 
    {
        private const string SCENE_PATH = "Assets/Tests/Scenes/BulletTest.unity";

        private Bullet bullet;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            AsyncOperation operation = EditorSceneManager
                .LoadSceneAsyncInPlayMode(SCENE_PATH, new LoadSceneParameters(LoadSceneMode.Single));

            while (!operation.isDone)
            {
                yield return null;
            }

            this.bullet = Object.FindObjectOfType<Bullet>(includeInactive: true);
            this.bullet.gameObject.SetActive(true);
        }

        [UnityTest]
        public IEnumerator MovementTest()
        {
            //Arange:
            var transform = this.bullet.transform;
            Vector3 startPosition = new Vector3(0, 0.5f, 0);
            transform.position = startPosition;

            //Act:
            const float moveSpeed = 5;
            const float moveFrames = 25;

            var moveComponent = this.bullet.GetComponent<MoveComponent>();
            if (moveComponent == null)
            {
                throw new Exception("Expected MoveComponent on Bullet");
            }
            
            moveComponent.MoveSpeed = moveSpeed;
            moveComponent.MoveDirection = Vector3.forward;
            float timeStart = Time.time;
            for (int i = 0; i < moveFrames; i++)
            {
                yield return null;
            }
            float deltaTime = Time.time - timeStart;
            
            //Assert:
            Vector3 diff = transform.position - (startPosition + Vector3.forward * moveSpeed * deltaTime);
            Assert.AreEqual(0, diff.magnitude, 1e-2);
        }

        [UnityTest]
        public IEnumerator LifetimeTest()
        {
            //Arange:
            var moveComponent = this.bullet.GetComponent<MoveComponent>();
            if (moveComponent == null)
            {
                throw new Exception("Expected MoveComponent on Bullet");
            }
            
            moveComponent.MoveSpeed = 0;
            moveComponent.MoveDirection = Vector3.zero;
            
            var lifetimeComponent = this.bullet.GetComponent<LifetimeComponent>();
            if (lifetimeComponent == null)
            {
                throw new Exception("Expected LifetimeComponent on Bullet");
            }

            lifetimeComponent.Lifetime = 1.5f;

            //Act:
            yield return new WaitForSeconds(2);

            //Assert:
            Assert.True(this.bullet == null);
        }

        [UnityTest]
        public IEnumerator HitTest()
        {
            var character = Object.FindObjectOfType<Character>();
            var healthComponent = character.GetComponent<HealthComponent>();
            if (healthComponent == null)
            {
                throw new Exception("Expected HealthComponent on Сharacter");
            }

            healthComponent.MaxHealth = 5;
            healthComponent.Health = 5;
            
            this.bullet.Damage = 1;
            
            var moveComponent = this.bullet.GetComponent<MoveComponent>();
            if (moveComponent == null)
            {
                throw new Exception("Expected MoveComponent on Bullet");
            }
            
            moveComponent.MoveSpeed = 5;
            moveComponent.MoveDirection = Vector3.forward;
            
            //Act:
            yield return new WaitForSeconds(3);

            //Assert:
            Assert.AreEqual(4, healthComponent.Health);
            Assert.True(this.bullet == null);
        }
    }
}