using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, 0f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
