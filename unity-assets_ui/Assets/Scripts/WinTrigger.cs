using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private Timer timer;

    private void Awake()
    {
        timer = GameObject.Find("Player").GetComponent<Timer>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        { 
            //other.gameObject.GetComponent<Timer>().timerText.fontSize = 60;
            //other.gameObject.GetComponent<Timer>().timerText.color = Color.green;
            other.gameObject.GetComponent<Timer>().enabled = false;
            GameObject.Find("WinCanvas").GetComponent<Canvas>().enabled = true;
            timer.Win();

        }
    }
}
