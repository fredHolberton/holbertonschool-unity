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
        gameObject.GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0;
        isOnPause = true;
        paused.TransitionTo(0.01f);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        unpaused.TransitionTo(0.01f);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        unpaused.TransitionTo(0.01f);
    }

    public void Options()
    {
        GameplayController.previousSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Options");
        unpaused.TransitionTo(0.01f);
    }
}
