using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class XRZoomController : MonoBehaviour
{
    [SerializeField] private InputActionReference upDownMove;
    [SerializeField] private float zoomSpeed = 1f;
    [SerializeField] private float minDistance = 0.5f;
    [SerializeField] private float maxDistance = 5f;

    private Vector2 direction = Vector2.zero;

    private void Update()
    {
        direction = upDownMove.action.ReadValue<Vector2>();
        if (Mathf.Abs(direction.y) > 0f)
        {
            float zoomValue = transform.position.z + (direction.y * zoomSpeed * Time.deltaTime);

            if (zoomValue < minDistance)
            {
                zoomValue = minDistance;
            }
            else if (zoomValue > maxDistance)
            {
                zoomValue = maxDistance;
            }
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, zoomValue);
            transform.position = newPosition;
        }
    }

    private void OnEnable()
    {
        upDownMove.action.performed += ZoomPerformed;
        upDownMove.action.canceled += ZoomCanceled;
    }

    private void OnDisable()
    {
        upDownMove.action.performed -= ZoomPerformed;
        upDownMove.action.canceled -= ZoomCanceled; ;
    }
    
    private void ZoomPerformed(InputAction.CallbackContext context)
    {
        //direction = context.ReadValue<Vector2>();
    }

    private void ZoomCanceled(InputAction.CallbackContext context)
    {
        direction = Vector2.zero;
    }

}
