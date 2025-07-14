using UnityEngine;

public class LookAround : MonoBehaviour
{
    /// <summary>
    /// Speed of the rotation.
    /// </summary>
    private float sensitivity = 3f;
    private float rotationX;
    private float rotationY;

    // Update is called once per frame
    void Update()
    {
        /*if (!isMouseOffScreen())
        {
            rotationX -= Input.GetAxis("Mouse Y") * sensitivity;
            rotationY -= Input.GetAxis("Mouse X") * sensitivity;

            rotationX = Mathf.Clamp(rotationX, -90, 90);

            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        }*/
        
        if (Input.GetMouseButton(1))
        {
            // Camera movement
            transform.RotateAround(transform.position, -Vector3.up, sensitivity * Input.GetAxis("Mouse X"));
            transform.RotateAround(transform.position, transform.right, sensitivity * Input.GetAxis("Mouse Y"));
        }
    }

    private bool isMouseOffScreen()
    {
        if (Input.mousePosition.x <= 2 || Input.mousePosition.y <= 2 || Input.mousePosition.x >= Screen.width - 2 || Input.mousePosition.y >= Screen.height - 2)
            return true;

        return false;
    }
}
