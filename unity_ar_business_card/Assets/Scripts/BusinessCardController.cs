using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusinessCardController : MonoBehaviour
{
    // AudioController script for play sounds
    private AudioController audioController;

    // EMail address 
    private string urlMail = "furbani@hotmail.com";
    
    // Twitter url
    private string urlTwitter = "https://x.com/fredHolberton64";
    
    // LinkedIn url
    private string urlLinkedIn = "https://www.linkedin.com/in/frédéric-urbani-16211759";
    
    // GitHub url
    private string urlGitHub = "https://github.com/fredHolberton";
    
    // Start is called before the first frame update
    void Start()
    {
        audioController = GameObject.Find("AudioManager").GetComponent<AudioController>();
    }

    // function called every frame per second
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            OnMouseEnter();
            OnMouseDown();
            OnMouseExit();
        }*/
    }

    /// <summary>
    /// Occurs when the user clicks on an icon
    /// </summary>
    public void OnMouseDown()
    {
        switch (this.gameObject.name)
        {
            case "Logo_Twitter":
                Application.OpenURL(urlTwitter);
                break;

            case "Logo_LikedIn":
                Application.OpenURL(urlLinkedIn);
                break;

            case "Logo_GitHub":
                Application.OpenURL(urlGitHub);
                break;

            case "Logo_Email":
                Application.OpenURL("mailto:" + Uri.EscapeDataString(urlMail));
                break;
        }

        audioController.PlayClick();
        
    }

    /// <summary>
    /// Occurs when the mouse pointer enters an icon
    /// </summary>
    public void OnMouseEnter()
    {
        Debug.Log(this.gameObject.name + " La souris entre");
        this.gameObject.GetComponent<Animator>().SetTrigger("MouseEnter");
    }

    
    /// <summary>
    /// occurs when the mouse pointer exits from an icon
    /// </summary>
    public void OnMouseExit()
    {
        Debug.Log(this.gameObject.name + " La souris sort");
        this.gameObject.GetComponent<Animator>().SetTrigger("MouseExit");
    }

}
