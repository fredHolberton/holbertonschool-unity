using UnityEngine;
using UnityEngine.InputSystem;

public class SpereController1 : MonoBehaviour
{
    [SerializeField] private InputActionReference mouseMove;

    // Update is called once per frame
    void Update()
    {
        // Get the current mouse position in screen coordinates
        Vector2 mousePosition = mouseMove.action.ReadValue<Vector2>();
        Debug.Log(string.Format("Position de la souris : {0}, {1}", mousePosition.x, mousePosition.y));

        // Convert the mouse position from screen space to world space
        Vector3 mouseCoord = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, transform.position.z));
        Debug.Log(string.Format("Coordonées de la souris : {0}, {1}", mouseCoord.x, mouseCoord.y));

        // Update the object's position to the mouse position
        transform.position = new Vector3(mouseCoord.x, mouseCoord.y, transform.position.z);
    }
}
