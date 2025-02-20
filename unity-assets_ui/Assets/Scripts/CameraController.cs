using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;      // L'objet à suivre
    public float rotationSpeed = 3f;

    public bool isInverted;

    private float currentAngleY = 0f; // L'angle actuel de la caméra autour de l'objet selon l'axe y
    private float currentAngleX = 0f; // L'angle actuel de la caméra autour de l'objet selon l'axe x
    private bool isMousePressed = false; // Pour savoir si la souris est cliquée
    private Vector3 offset;        // Décalage entre la caméra et l'objet

    RaycastHit hit = new RaycastHit();

    void Start()
    {
        // Initialiser le décalage (la caméra se positionne à l'arrière de l'objet)
        offset = player.transform.position - transform.position;
        // Initialiser l'angle que fait la camera avec le player
        currentAngleY = transform.eulerAngles.y;
        currentAngleX = transform.eulerAngles.x;
        isInverted = GameplayController.isInverted;
    }
    private void LateUpdate()
    {
        RotateCameraWithMouse();
        Followplayer();
    }

    void Update()
    {
        if (Input.GetMouseButton(1)) // Si le clic de souris est maintenu
        {
            isMousePressed = true;
            //RotateCameraWithMouse();
        }
        else if (!Input.GetMouseButton(1)) // Si le clic de souris est relâché
        {
            isMousePressed = false;
        }
    }

    // Fonction pour faire tourner la caméra autour de l'objet en fonction de la souris
    void RotateCameraWithMouse()
    {
        
        if (isMousePressed)
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * (isInverted? -1 : 1);
            currentAngleY += mouseX;
            currentAngleX += mouseY;

            // Clamp the pitch to prevent flipping
            currentAngleX = Mathf.Clamp(currentAngleX, -90f, 90f);
            currentAngleY = Mathf.Clamp(currentAngleY,-180f, 180f);
        }
    }

    // Fonction pour suivre l'objet sans rotation de la caméra
    void Followplayer()
    {
        transform.position = player.transform.position - Quaternion.Euler(currentAngleX, currentAngleY, 0f) * offset;
        transform.LookAt(player.transform.position);
    }
}
