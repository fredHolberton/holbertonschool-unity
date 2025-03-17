using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject player;
    public GameObject timerCanvas;

    // Start is called before the first frame update
    void Start()
    {
        /*mainCamera = GameObject.Find("Main Camera");
        player = GameObject.Find("Player");
        timerCanvas = Canvas.FindAnyObjectByType<Canvas>();*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateGameObjects()
    {
        mainCamera.SetActive(true);
        player.GetComponent<PlayerController>().enabled = true;
        timerCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
}
