using UnityEngine;

public class SpeedBoostTrigger : MonoBehaviour
{
    [SerializeField] private float boostStrength = 30f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            Debug.Log("Trigger Enter");
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 1) * boostStrength);
        }
    }
}
