using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 
    public float speed;
    public float jumpPower;
    public LayerMask layerGround;

    private float vertical;
    private float horizontal;
    private Rigidbody rb;
    private Animator anim;

    private float minAltitude = -30f;
    private Vector3 reinitPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        reinitPosition = transform.position;
        reinitPosition.y = -minAltitude;
        Time.timeScale = 1;
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        HandleJump();
        HandleMovement();
        Falling();
    }
        
    private void HandleJump()
    {
        bool saut = Input.GetButtonDown("Jump");

        if (saut && Physics.Raycast(transform.position, Vector3.down , 2f, layerGround))
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            anim.SetBool("IsJumping", true);
        }
    }

    private void HandleMovement()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); // Use GetAxisRaw

        if (moveInput.magnitude > 0)
        {
            Transform cameraTransform = Camera.main.transform;
            Vector3 moveDirection = cameraTransform.right * moveInput.x + cameraTransform.forward * moveInput.z;
            moveDirection.Normalize();

            rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);

            anim.SetBool("IsMoving", true);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0); // Stop horizontal movement when no input
            anim.SetBool("IsMoving", false);
        }
    }

    private void Falling()
    {
        if (transform.position.y < minAltitude)
        {
            transform.position = reinitPosition;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            anim.SetBool("IsJumping", false);
            Debug.Log("Fin du saut");
        }
    }



}
