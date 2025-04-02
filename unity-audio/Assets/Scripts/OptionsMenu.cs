using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public Toggle invertYToggle;
    public Slider BGMSlider;
    public Slider SFXSlider;
    public AudioMixer audioMixer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        invertYToggle.isOn = GameplayController.isInverted;
        BGMSlider.value = GameplayController.BGMSlider;
        SFXSlider.value = GameplayController.SFXSlider;

    }

    public void Back()
    {
        SceneManager.LoadScene(GameplayController.previousSceneName);
    }

    public void Apply()
    {
        GameplayController.isInverted = invertYToggle.isOn;
        GameplayController.BGMSlider = BGMSlider.value;
        GameplayController.SFXSlider = SFXSlider.value;
        Back();
    }

    public void HandleBGMSliderValueChanged()
    {
        Debug.Log("value = " + BGMSlider.value);
        float volume = Mathf.Log10(Mathf.Clamp(BGMSlider.value, 0.0001f, 1f)) * 20f;
        Debug.Log("volume = " + volume + " Db");
        audioMixer.SetFloat("BGMVolume", volume);
    }
    
    public void HandleSFXSliderValueChanged()
    {
        float volume = Mathf.Log10(Mathf.Clamp(SFXSlider.value, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("RunningVolume", volume);
        audioMixer.SetFloat("LandingVolume", volume);
        audioMixer.SetFloat("AmbianceVolume", volume);
    }
}
