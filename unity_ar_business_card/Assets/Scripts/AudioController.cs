using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    /// <summary>
    /// AudioClip Music
    /// </summary>
    public AudioClip clickSound;
    
    /// <summary>
    /// Clip when displaying icons and NameJob text
    /// </summary>
    public AudioClip musicSound;

    // Audio source for sounds
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClick()
    {
        audioSource.PlayOneShot(clickSound);
    }

    public void PlayMusic()
    {
        audioSource.PlayOneShot(musicSound);
    }

}
