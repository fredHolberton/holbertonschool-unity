using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manage the actions when the player click on the buttons in the Pause Canvas
/// </summary>
public class PauseMenu : MonoBehaviour
{
    /* Boolean : true if the game is on pause */
    private bool isOnPause;
    // Start is called before the first frame update
    void Start()
    {
        isOnPause = false;
        gameObject.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            isOnPause = !isOnPause;


            if (isOnPause)
                Pause();
            else
                Resume();
        }
    }

    /// <summary>
    /// set the game in Pause
    /// </summary>
    public void Pause()
    {
        gameObject.GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0;
        isOnPause = true;
    }

    /// <summary>
    /// Resumes the game where it left off
    /// </summary>
    public void Resume()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
        isOnPause = false;
    }

    /// <summary>
    /// Restart the game at the begining
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Load the MainMenu scene
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Load the Options scene
    /// </summary>
    public void Options()
    {
        GameplayController.previousSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Options");
    }

    /// <summary>
    /// Load the LeaderScores scene
    /// </summary>
    public void LeaderScores()
    {
        GameplayController.previousSceneName = SceneManager.GetActiveScene().name; 
        SceneManager.LoadScene("LeaderScores");
    }
}
