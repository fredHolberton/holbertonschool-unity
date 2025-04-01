using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioRunning;
    public AudioSource audioLanding;
    public AudioSource audioBackground;
     public AudioClip grassSound;
    public AudioClip rockSound;
    public AudioClip grassSplashSound;
    
    public void PlayRunning(string tag)
    {
        AudioClip clip = grassSound;

        if (tag == "Rock") clip = rockSound;
        if (audioRunning.clip != clip)
        {
            StopRunning();
            audioRunning.clip = clip;
        }
        if (!audioRunning.isPlaying)
        {
            audioRunning.Play();
        }
    }

    public void StopRunning()
    {
        if (audioRunning.isPlaying)
            audioRunning.Stop();
    }

    public void PlayJumping(string tag, float volume)
    {
        AudioClip clip = grassSound;
        
        if (tag == "Rock") clip = rockSound;
        audioLanding.PlayOneShot(clip, volume);
    }

    public void PlayLanding(float volume)
    {
        audioLanding.PlayOneShot(grassSplashSound, volume);
    }




}
