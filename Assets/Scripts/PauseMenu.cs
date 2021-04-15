using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool helpBool;

    GameObject[] pauseObjects;
    GameObject[] helpObjects;

    

    // Start is called before the first frame update
    void Start()
    {
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
        helpObjects = GameObject.FindGameObjectsWithTag("Help");
        hideHelp();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseControl();
        }
        if (Input.GetKey("escape"))
        {
            Debug.Log("escaped");
            Application.Quit();
        }
    }

    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    public void hidePaused()
    {
        if (pauseObjects == null)
            Debug.Log("pauseObjects is null");
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    public void pauseControl()
    {
        //Debug.Log("test");
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }

    public void toggleHelp()
    {
        if (helpBool)
        {
            hideHelp();
        }
        else
        {
            showHelp();
        }
    }

    public void showHelp()
    {
        foreach (GameObject g in helpObjects)
        {
            g.SetActive(true);
        }
        helpBool = true;
    }

    public void hideHelp()
    {
        foreach (GameObject g in helpObjects)
        {
            g.SetActive(false);
        }
        helpBool = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
