using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    private bool isOnPause;
    // Start is called before the first frame update
    void Start()
    {
        isOnPause = false;
        pauseCanvas.SetActive(false);
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
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
        isOnPause = true;
    }

    public void Resume()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
        isOnPause = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level01");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }
}
