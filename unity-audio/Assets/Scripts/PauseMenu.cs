using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;
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

    public void Pause()
    {
        paused.TransitionTo(0.01f);
        gameObject.GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0;
        isOnPause = true;
    }

    public void Resume()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
        isOnPause = false;
        unpaused.TransitionTo(0.01f);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        unpaused.TransitionTo(0.01f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        unpaused.TransitionTo(0.01f);
        SceneManager.LoadScene("MainMenu");
    }

    public void Options()
    {
        Time.timeScale = 1;
        unpaused.TransitionTo(0.01f);
        GameplayController.previousSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Options");
    }
}
