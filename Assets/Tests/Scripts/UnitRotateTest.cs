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
    public sealed class UnitRotateTest
    {
        private GameObject character;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            AsyncOperation operation = EditorSceneManager.LoadSceneAsyncInPlayMode(
                "Assets/Tests/Scenes/UnitRotateTest.unity",
                new LoadSceneParameters(LoadSceneMode.Single)
            );
            
            while (!operation.isDone)
            {
                yield return null;
            }

            this.character = GameObject.FindGameObjectWithTag("Player");
        }

        [UnityTest]
        public IEnumerator RotationTest()
        {
            //Arange:
            Transform transform = this.character.transform;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            const float rotationSpeed = 100;
            var rotationComponent = this.character.GetComponent<RotationComponent>();
            
            //Act#1:
            rotationComponent.RotationSpeed = rotationSpeed;
            rotationComponent.RotationDirection = Vector3.back;
            yield return new WaitForSeconds(2.0f);
            
            //Assert:
            Assert.AreEqual(transform.eulerAngles.y, 180, 1e-2);

            //Act:
            rotationComponent.RotationDirection = Vector3.forward;
            yield return new WaitForSeconds(2.0f);

            //Assert:
            Assert.AreEqual(transform.eulerAngles.y, 0, 1e-2);

            //Act:
            rotationComponent.RotationDirection = new Vector3(-1, 0, -1);
            yield return new WaitForSeconds(2.0f);

            //Assert:
            Assert.AreEqual(transform.eulerAngles.y, 225, 1e-2);

            //Act:
            rotationComponent.RotationDirection = new Vector3(1, 0, -1);
            yield return new WaitForSeconds(2.0f);

            //Assert:
            Assert.AreEqual(transform.eulerAngles.y, 135, 1e-2);

            //Act:
            rotationComponent.RotationDirection = Vector3.zero;
            yield return new WaitForSeconds(1.0f);

            //Assert:
            Assert.AreEqual(transform.eulerAngles.y, 135, 1e-2);
        }
    }
}