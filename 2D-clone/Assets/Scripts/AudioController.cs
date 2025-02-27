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

    public void Pause()
    {
         audio1.Pause();
         Debug.Log("Pause");
    }

    public void Play()
    {
        audio1.UnPause();
        audio2.Stop();
        Debug.Log("Play");
    }

    public void PlayNewLine()
    {
        audio2.PlayOneShot(soundLine);
    }

    public void PlayGameOver()
    {
         audio1.Pause();
         audio2.PlayOneShot(soundGameOver);
    }

    public void PlayNewLevel()
    {
        audio2.PlayOneShot(soundLevel);
    }

}
