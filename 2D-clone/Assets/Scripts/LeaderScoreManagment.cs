using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Load and display the best scores from Player Prefs when the current scene is loaded
/// </summary>
public class LeaderScoreManagment : MonoBehaviour
{
    public TextMeshProUGUI[] bestScoreValue;

    // Start is called before the first frame update
    void Start()
    {
        // Loading the best scores
        if (PlayerPrefs.HasKey("BestScorePlayer1"))
        {
            bestScoreValue[0].text = string.Format("{0}", PlayerPrefs.GetInt("BestScorePlayer1"));
        }
        else
        {
            bestScoreValue[0].text = "0";
            PlayerPrefs.SetInt("BestScorePlayer1", 0);
        }

        if (PlayerPrefs.HasKey("BestScorePlayer2"))
        {
            bestScoreValue[1].text = string.Format("{0}", PlayerPrefs.GetInt("BestScorePlayer2"));
        }
        else
        {
            bestScoreValue[1].text = "0";
            PlayerPrefs.SetInt("BestScorePlayer2", 0);
        }
        
    }

    /// <summary>
    /// Load the previous scene if player click on the back button
    /// </summary>
    public void Back()
    {
        SceneManager.LoadScene(GameplayController.previousSceneName);
    }

}
