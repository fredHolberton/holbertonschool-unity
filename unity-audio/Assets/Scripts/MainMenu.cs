using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour

{
    // Audio source to play clip
    public AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelSelect(int level)
    {
        if (level == 1 || level == 2 || level == 3)
            SceneManager.LoadScene("Level0" + level); // je lance ma scène correspondant au Level
        else
            Debug.Log("Wrong choice");
    }

    public void Options()
    {
        GameplayController.previousSceneName = SceneManager.GetActiveScene().name; 
        SceneManager.LoadScene("Options");
    }

    public void QuitPlatforms()
    {
        Application.Quit();
        Debug.Log("Exited");
    }
}
