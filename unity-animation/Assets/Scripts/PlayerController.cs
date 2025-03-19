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

    private Rigidbody rb;
    private Animator anim;

    private float minAltitude = -6f;
    private Vector3 reinitPosition;
    private bool IsGrounded = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        reinitPosition = transform.position;
        reinitPosition.y = 60f;
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
        else if (anim.GetBool("IsJumping") && Physics.Raycast(transform.position, Vector3.down , 0.1f, layerGround))
        {
            anim.SetBool("IsJumping", false);
        }
    }

    private void HandleMovement()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); // Use GetAxisRaw

        if (moveInput.magnitude > 0 &&  !anim.GetBool("IsFalling"))
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
        if ((rb.velocity.y < -6) && !anim.GetBool("IsFalling"))
        {
            anim.SetBool("IsFalling", true);
            transform.position = reinitPosition;
        }
        else if (anim.GetBool("IsFalling") && Physics.Raycast(transform.position, Vector3.down , 0.1f, layerGround))
        {
            //anim.SetBool("IsFalling", false);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StartingTrigger" && anim.GetBool("IsFalling"))
        {
            anim.SetBool("IsFalling", false);
        }
    }
}
