using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle invertYToggle;
    // Start is called before the first frame update
    void Start()
    {
        invertYToggle.isOn = GameplayController.isInverted;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {
        SceneManager.LoadScene(GameplayController.previousSceneName);
    }

    public void Apply()
    {
        GameplayController.isInverted = invertYToggle.isOn;
        Back();
    }
}
