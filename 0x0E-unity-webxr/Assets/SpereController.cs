using UnityEngine;
using UnityEngine.InputSystem;

public class SpereController : MonoBehaviour
{
    [SerializeField] private InputActionReference mouseMove;

    [SerializeField] private GameObject sphere;
    // Update is called once per frame
    void Update()
    {
        // Get the current mouse position in screen coordinates
        Vector2 mousePosition = mouseMove.action.ReadValue<Vector2>();
        Debug.Log(string.Format("Position de la souris : {0}, {1}", mousePosition.x, mousePosition.y));

        // Convert the mouse position from screen space to world space
        Vector3 mousecCoord = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, sphere.transform.position.z));
        Debug.Log(string.Format("Coordonées de la souris : {0}, {1}", mousecCoord.x, mousecCoord.y));

        // Update the object's position to the mouse position
        sphere.transform.position = new Vector3(mousecCoord.x, mousecCoord.y, sphere.transform.position.z);
    }
}
