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
    public AudioClip grassSound;
    public AudioClip rockSound;

    private Rigidbody rb;
    private Animator anim;
    private AudioClip currentSound;

    private float minAltitude = -6f;
    private Vector3 reinitPosition;
    private float rotationSpeed;
    private AudioSource audioSource;

    private bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        reinitPosition = transform.position;
        reinitPosition.y = 60f;
        Time.timeScale = 1;
        anim = GetComponent<Animator>();
        rotationSpeed = 4f;
        audioSource = GetComponent<AudioSource>();
        canMove = true;

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

        if (saut && Physics.Raycast(transform.position, Vector3.down , 0.8f, layerGround) && canMove)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            anim.SetBool("IsJumping", true);
            canMove = false;
        }
        else if (anim.GetBool("IsJumping") && Physics.Raycast(transform.position, Vector3.down , 0.08f, layerGround))
        {
            anim.SetBool("IsJumping", false);
            canMove = true;
        }
    }

    private void HandleMovement()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); // Use GetAxisRaw

        if (moveInput.magnitude > 0 &&  canMove)
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

    private void Falling()
    {
        if ((transform.position.y < minAltitude) && !anim.GetBool("IsFalling"))
        {
            anim.SetBool("IsFalling", true);
            transform.position = reinitPosition;
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsMoving", false);
            canMove = false;
        }
        else if (anim.GetBool("IsFalling") && Physics.Raycast(transform.position, Vector3.down , 0.1f, layerGround))
        {
            anim.SetBool("IsFalling", false);
        }

    }

    private void PlaySound()
    {
        if ((!anim.GetBool("IsFalling")) && (!anim.GetBool("IsJumping")) && anim.GetBool("IsMoving"))
        {
            if (audioSource.clip != currentSound)
            {
                audioSource.Stop();
                audioSource.clip = currentSound;
            }
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
                
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    private void OnCollisionStay(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Grass":
                currentSound = grassSound;
                break;

            case "Rock":
                currentSound = rockSound;
                break;
        }
    }

    public void EndFalling()
    {
        canMove = true;
    }
}
