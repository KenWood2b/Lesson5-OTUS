using System.Collections;
using NUnit.Framework;
using Homework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace HomeworkTests
{
    [TestFixture]
    public sealed class WeaponTests
    {
        private const string SCENE_PATH = "Assets/Tests/Scenes/WeaponTest.unity";
        
        private Weapon weapon;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            AsyncOperation operation = EditorSceneManager
                .LoadSceneAsyncInPlayMode(SCENE_PATH, new LoadSceneParameters(LoadSceneMode.Single));

            while (!operation.isDone)
            {
                yield return null;
            }

            this.weapon = GameObject.FindObjectOfType<Weapon>();
            this.weapon.Charges = 5;
        }
        
        [UnityTest]
        public IEnumerator FireSuccessTest()
        {
            //Arrange:
            this.weapon.Charges = 5;

            //Act:
            this.weapon.Fire();
            
            yield return new WaitForFixedUpdate();
            
            //Assert:
            Assert.True(GameObject.FindObjectOfType<Bullet>() != null);
            Assert.AreEqual(4, this.weapon.Charges);
        }

        [UnityTest]
        public IEnumerator FireFailedTest()
        {
            //Arrange:
            this.weapon.Charges = 0;
            
            //Act:
            this.weapon.Fire();
            
            yield return null;

            //Assert:
            Assert.AreEqual(0, this.weapon.Charges);
            Assert.True(GameObject.FindObjectOfType<Bullet>() == null);
        }
    }
}