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
    public AudioController audioController;

    private Rigidbody rb;
    private Animator anim;
    private string currentSound;

    private float minAltitude = -6f;
    private Vector3 reinitPosition;
    private float rotationSpeed;
    private bool canMove;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        reinitPosition = transform.position;
        reinitPosition.y = 60f;
        Time.timeScale = 1;
        rotationSpeed = 4f;
        canMove = true;
        currentSound = "Grass";
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleJump();
        handleFalling();
    }
        
    private void HandleJump()
    {
        bool jump = Input.GetButtonDown("Jump");
        if (jump && !anim.GetBool("IsJumping") && canMove)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            anim.SetBool("IsJumping", true);
            Debug.Log("Je saute !");
            //canMove = false;
        }
    }

    private void HandleMovement()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); // Use GetAxisRaw

        if (moveInput.magnitude > 0 && canMove)
        {
            Transform cameraTransform = Camera.main.transform;
            Vector3 moveDirection = cameraTransform.right * moveInput.x + cameraTransform.forward * moveInput.z;
            moveDirection.Normalize();

            rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);

            // ROTATION
            Vector3 desiredDirection = new Vector3(-rb.velocity.x, 0, -rb.velocity.z);

            if (desiredDirection != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(desiredDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.02f * rotationSpeed);
            }

            anim.SetBool("IsMoving", true);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0); // Stop horizontal movement when no input
            anim.SetBool("IsMoving", false);
        }
        PlaySound();
    }

    private void handleFalling()
    {
        if ((transform.position.y < minAltitude) && !anim.GetBool("IsFalling"))
        {
            anim.SetBool("IsFalling", true);
            transform.position = reinitPosition;
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsMoving", false);
            canMove = false;
        }
    }

    private void PlaySound()
    {
        if ((!anim.GetBool("IsFalling")) && (!anim.GetBool("IsJumping")) && anim.GetBool("IsMoving"))
        {
           audioController.PlayRunning(currentSound);
                
        }
        else
        {
            audioController.StopRunning();
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (anim != null)
        {
            if (col.gameObject.tag == "Grass" || col.gameObject.tag == "Rock")
            {
                switch (col.gameObject.tag)
                {
                    case "Grass":
                        currentSound = "Grass";
                        break;

                    case "Rock":
                        currentSound = "Rock";
                        break;
                }
                if (anim.GetBool("IsJumping"))
                {
                    anim.SetBool("IsJumping", false);
                    canMove = true;
                    audioController.PlayJumping(currentSound, 1f);
                }
                if (anim.GetBool("IsFalling"))
                {
                    anim.SetBool("IsFalling", false);
                    audioController.PlayLanding(1f);
                }          
            }
            
        }
        
    }

    public void ReturnToIdle()
    {
        canMove = true;
    }

}
