using UnityEngine;

public class AlleyTrigger : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            Debug.Log("Alley Trigger Enter");
            gameController.SpawnObstacles();
        }
    }
}
