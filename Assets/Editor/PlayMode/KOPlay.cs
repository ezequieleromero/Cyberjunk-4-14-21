using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Collections.Specialized;

namespace Tests
{
    public class KOPlay
    {
        // A Test behaves as an ordinary method
        [OneTimeSetUp]
        public void LoadScene()
        {
            // Use the Assert class to test conditions
            SceneManager.LoadScene("VetClinic");
        }
        [UnityTest]
        public IEnumerator EnemiesSpawn()
        {
            for (int i = 0; i < 600; i++) //This one is long so that the scene gets fully loaded
            {
                yield return null;
            }
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            int numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
            Assert.That(numberOfEnemies > 5);
        }
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator GunsLoad() //This one piggy backs of the other where the scene is alredy loaded
        {

            yield return null;//It gets angry when this is not included, doesn't hurt anything

            GameObject gun = GameObject.FindWithTag("Weapon");
            Assert.True(gun.activeInHierarchy);
        }
        [UnityTest] //Still a work in progress
        public IEnumerator Stress() //This one piggy backs of the other where the scene is alredy loaded
        {
            var deltaTime = 0.0;
            var fps = 0.0;


            deltaTime += Time.deltaTime;
            deltaTime /= 2.0;
            fps = 1.0 / deltaTime;
            int i = 0;

            yield return new WaitForSeconds(3);
            while (fps > 15f)
            {
                UnityEngine.Debug.Log("Weapon count: " + i++);

                GameObject weaponObject = Resources.Load("Gun_AssaultRifle") as GameObject; //This contains the weapon object

                GameObject weapon = GameObject.Instantiate(weaponObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                GunScript weaponScript = weapon.GetComponent<GunScript>();
                // weaponScript.parent = GameObject;
                weaponScript.Hand = Resources.Load("Hand") as GameObject;
                SpriteRenderer weaponRender = weapon.GetComponent<SpriteRenderer>();
                SpriteRenderer render = weaponRender.GetComponent<SpriteRenderer>();

                deltaTime += Time.deltaTime;
                deltaTime /= 2.0;
                fps = 1.0 / deltaTime;
                //shootCheck = true;
                // Debug.log("Count: " + i);
                //i++;
                //yield return new WaitForSeconds(1);

            }
            int numOfGuns = GameObject.FindGameObjectsWithTag("Weapon").Length;
            Assert.That(fps > 10f);

        }

    }
}