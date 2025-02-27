using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manage the actions when player click on a Button of the Options scene
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    /// <summary>
    /// player toggle linked to the PlayerToggle in the Canvas
    /// </summary>
    public Toggle playerToggle;
    
    // Start is called before the first frame update
    void Start()
    {
        playerToggle.isOn = GameplayController.nbPlayers == 2 ? true : false;
    } 

    /// <summary>
    /// Load the previous Scene
    /// </summary>
    public void Back()
    {
        SceneManager.LoadScene(GameplayController.previousSceneName);
    }

    /// <summary>
    /// Save the setted options and load the previous scene
    /// </summary>
    public void Apply()
    {
        GameplayController.nbPlayers = playerToggle.isOn ? 2 : 1;
        Back();
    }
}
