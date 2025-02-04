using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 
    public float speed;
    public float jumpPower;
    public LayerMask layerMask;

    private float vertical;
    private float horizontal;
    private Rigidbody rb;

    private float minAltitude = -30f;
    private Vector3 reinitPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        reinitPosition = transform.position;
        reinitPosition.y = -minAltitude;

    }

    // Update is called once per frame
    void Update()
    {
        // MOVE
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 mouvement = new Vector3(horizontal, 0, vertical).normalized;
        rb.velocity = new Vector3(mouvement.x* speed, rb.velocity.y, mouvement.z * speed);

        //JUMP
        bool jump = Input.GetButtonDown("Jump");

        if (jump)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }

        if (transform.position.y < minAltitude)
        {
            transform.position = reinitPosition;
        }
    }
}
