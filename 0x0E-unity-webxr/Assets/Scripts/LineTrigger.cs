using UnityEngine;

public class LineTrigger : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    private bool isFell = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Floor" && !isFell)
        {
            Debug.Log("Line Trigger Exit");
            gameController.IncrementScore();
            isFell = true;
        }
    }
}
