using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    // game object to follow with the main camera
    public GameObject player;

    private float offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position.z - player.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x , transform.position.y, player.transform.position.z + offset);
    }
}
