using UnityEngine;

/// <summary>
/// Manage the game music when players play
/// </summary>
public class AudioController : MonoBehaviour
{
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioClip soundLine;
    public AudioClip soundLevel;
    public AudioClip soundGameOver;

    // Start is called before the first frame update
    void Start()
    {
        audio1.Play();
    }

    /// <summary>
    /// Set game music on pause
    /// </summary>
    public void Pause()
    {
         audio1.Pause();
         Debug.Log("Pause");
    }

    /// <summary>
    /// Unpause the game music
    /// </summary>
    public void Play()
    {
        audio1.UnPause();
        audio2.Stop();
        Debug.Log("Play");
    }

    /// <summary>
    /// Play the sound when a new line is full
    /// </summary>
    public void PlayNewLine()
    {
        audio2.PlayOneShot(soundLine);
    }

    /// <summary>
    /// Play the Game over sound
    /// </summary>
    public void PlayGameOver()
    {
         audio1.Pause();
         audio2.PlayOneShot(soundGameOver);
    }

    /// <summary>
    /// Play the sound when player reaches a new level 
    /// </summary>
    public void PlayNewLevel()
    {
        audio2.PlayOneShot(soundLevel);
    }

}
