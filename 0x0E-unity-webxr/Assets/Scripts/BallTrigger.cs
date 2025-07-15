using UnityEngine;

public class BallTrigger : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    private bool isRolling = false;

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
        if (collision.gameObject.tag == "Floor" && !isRolling)
        {
            Debug.Log("Floor Trigger Enter");
            gameController.SpawnObstacles();
            isRolling = true;
        }
    }

    public void Reset()
    {
        isRolling = false;
    }
}
