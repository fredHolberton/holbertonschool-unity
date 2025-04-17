using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJController : MonoBehaviour
{
    
    private GameObject imageTarget;
    // Start is called before the first frame update
    void Start()
    {
         imageTarget = GameObject.Find("ImageTarget").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnStart()
    {
        transform.position = imageTarget.transform.position;
    }

}
