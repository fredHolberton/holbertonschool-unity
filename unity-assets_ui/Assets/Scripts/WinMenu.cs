using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class WinMenu : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Next()
    {
        
        string currentSceneName = SceneManager.GetActiveScene().name;
        int currentLevel = Int32.Parse(currentSceneName.Substring(currentSceneName.Length - 1, 1));
        if (currentLevel == 3)
            SceneManager.LoadScene("MainMenu");
        else
        {
            int nextLevel = currentLevel + 1;
            string nextSceneName = "Level0" + nextLevel;
            SceneManager.LoadScene(nextSceneName);
        }

    }
}
