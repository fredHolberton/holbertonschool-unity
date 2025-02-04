using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;      // L'objet à suivre
    public float rotationSpeed = 3f;

    private float currentAngle = 0f; // L'angle actuel de la caméra autour de l'objet
    private bool isMousePressed = false; // Pour savoir si la souris est cliquée
    private Vector3 offset;        // Décalage entre la caméra et l'objet

    void Start()
    {
        // Initialiser le décalage (la caméra se positionne à l'arrière de l'objet)
        offset = player.transform.position - transform.position;
        // Initialiser l'angle que fait la camera avec le player
        currentAngle = transform.eulerAngles.y;
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
            currentAngle += mouseX;
        }       
    }

    // Fonction pour suivre l'objet sans rotation de la caméra
    void Followplayer()
    {
        transform.position = player.transform.position - Quaternion.Euler(0f, currentAngle, 0f) * offset;
        transform.LookAt(player.transform.position);
    }
}
