using UnityEngine;

public class LookAround : MonoBehaviour
{
    /// <summary>
    /// Speed of the rotation.
    /// </summary>
    public float speed = 3;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // Camera movement
            transform.RotateAround(transform.position, -Vector3.up, speed * Input.GetAxis("Mouse X"));
            transform.RotateAround(transform.position, transform.right, speed * Input.GetAxis("Mouse Y"));
        }
    }
}
