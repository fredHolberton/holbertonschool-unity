using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manage the actions when a button is pressed on the main menu
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Start the game
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene("Game"); // load the Game scene
    }

    /// <summary>
    /// Load the Options scene when player click on Options button
    /// </summary>
    public void Options()
    {
        GameplayController.previousSceneName = SceneManager.GetActiveScene().name; 
        SceneManager.LoadScene("Options");
    }

    /// <summary>
    /// Load the LeaderScore scene when player click on LeaderScore button
    /// </summary>
    public void LeaderScores()
    {
        GameplayController.previousSceneName = SceneManager.GetActiveScene().name; 
        SceneManager.LoadScene("LeaderScores");
    }

    /// <summary>
    /// Exit the game when player click on the Exit button
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Exited");
    }
}
