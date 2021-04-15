using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.TestTools;

namespace Tests
{
    
    public class ButtonTest
    {
        bool clicked;
        float sec = 5;
        private GameObject pauseCanvas = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/PauseMenu.prefab");
        private GameObject pauseCanvasInst;

        public static bool helpBool;

        GameObject[] pauseObjects;
        GameObject[] helpObjects;

        [OneTimeSetUp]
        public void LoadPauseMenu()
        {
            //pauseCanvasInst = Object.Instantiate(pauseCanvas, new Vector3(0, 0, 0), Quaternion.identity);

            SceneManager.LoadScene("VetClinic");

           /* pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
            pauseCanvasInst.PauseGameObject.hidePaused();
            helpObjects = GameObject.FindGameObjectsWithTag("Help");
            pauseCanvasInst.PauseGameObject.hideHelp();*/
        }

        /*// A Test behaves as an ordinary method
        [Test]
        public void ButtonTestSimplePasses()
        {
            // Use the Assert class to test conditions
        }*/

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator ButtonTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            //yield return PlayModeSetup();
            yield return null;

            //Assert PauseMenu closed
            var pauseMenu = GameObject.Find("PauseMenu");
            Assert.Null(pauseMenu);



            /*pauseTopRightButton.onClick.AddListener(Clicked);
            pauseTopRightButton.onClick.Invoke();

            Assert.True(clicked);
            clicked = false;

            var resumeGO = GameObject.Find("Play Button");
            var resumeButton = resumeGO.GetComponent<Button>();

            Assert.NotNull(resumeButton);

            resumeButton.onClick.AddListener(Clicked);
            resumeButton.onClick.Invoke();

            Debug.Log("Success");

            Assert.True(clicked);
            clicked = false;*/

            //Loop TopRight Pause Button
            //for (int i = 0; i < 1000; i++)
            int i = 0;
            while(true)
            {
                i++;
                //Assert TopRight exists
                var pauseTopRightGO = GameObject.Find("PauseTopRight");
                var pauseTopRightButton = pauseTopRightGO.GetComponent<Button>();
                Assert.NotNull(pauseTopRightButton);

                clicked = false;

                pauseTopRightButton.onClick.AddListener(Clicked);
                pauseTopRightButton.onClick.Invoke();

                Assert.True(clicked);
                clicked = false;

                var restartGO = GameObject.Find("Restart");
                var restartButton = restartGO.GetComponent<Button>();

                Assert.NotNull(restartButton);

                restartButton.onClick.AddListener(Clicked);
                restartButton.onClick.Invoke();

                //Debug.Log("Success");

                Assert.True(clicked);
                clicked = false;
                Debug.Log(i);

                //yield return null;
                //yield return new WaitForSeconds(sec);
                //sec = sec / 2;
                //Debug.Log("Sec: " + sec);
            }
            
            

            /*pauseMenu = GameObject.Find("PauseMenu");
            Assert.NotNull(pauseMenu);*/
        }

        private void Clicked()
        {
            clicked = true;
        }
    }
}
