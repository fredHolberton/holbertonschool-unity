using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour

{ 
    public AudioMixer audioMixer;

    private static bool isLoaded = false;
    
    // Start is called before the first frame update
    void Start()
    {
        LoadPreferences();
    }

    private void LoadPreferences()
    {
        if (!isLoaded)
        {
            if (PlayerPrefs.HasKey("isInverted"))
            {
                GameplayController.isInverted = PlayerPrefs.GetInt("isInverted") == 0? false : true;
            }

            if (PlayerPrefs.HasKey("BGMSlider"))
            {
                GameplayController.BGMSlider = PlayerPrefs.GetFloat("BGMSlider");
            }

            if (PlayerPrefs.HasKey("SFXSlider"))
            {
                GameplayController.SFXSlider = PlayerPrefs.GetFloat("SFXSlider");
            }

            isLoaded = true;

        }

        // apply BGMSlider and SFXSlider values to audioMixer
        float volume = Mathf.Log10(Mathf.Clamp(GameplayController.BGMSlider, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("BGMVolume", volume);

        volume = Mathf.Log10(Mathf.Clamp(GameplayController.SFXSlider, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("RunningVolume", volume);
        audioMixer.SetFloat("LandingVolume", volume);
        audioMixer.SetFloat("AmbianceVolume", volume); 
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
        Debug.Log("Previous scene = " + GameplayController.previousSceneName);
        SceneManager.LoadScene("Options");
    }

    public void QuitPlatforms()
    {
        SavePreferences();
        Application.Quit();
        Debug.Log("Exited");
    }

    private void SavePreferences()
    {
        PlayerPrefs.SetInt("isInverted", GameplayController.isInverted == false? 0 : 1);
        PlayerPrefs.SetFloat("BGMSlider", GameplayController.BGMSlider);
        PlayerPrefs.SetFloat("SFXSlider", GameplayController.SFXSlider);
    }
}
