using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Speed of the player mouvement
    public float speed;
    
    // Score text displayed on the ScoreText GameObject
    public TextMeshProUGUI scoreText;

    // health Text displayed on HealthText GameObject
    public TextMeshProUGUI healthText;

    // Text to display the final result of the game (Lost or Win)
    public TextMeshProUGUI winLoseText;

    // background of the final result
    public Image winLoseBG;

    // health of the player
    public int health;

    private Rigidbody rb;
    private float vertical;
    private float horizontal;
    private int score;
    private bool isTeleporting;
    private Image WinLoseBG;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0; 
        isTeleporting = false;
        winLoseBG.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // DEPLACEMENT
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 mouvement = new Vector3(horizontal, 0, vertical).normalized;
        rb.velocity = new Vector3(mouvement.x* speed, rb.velocity.y, mouvement.z * speed);

        if (health == 0)
        {
            //Debug.Log("Game Over!");
            winLoseText.text = "Game Over!";
            winLoseText.color = Color.white;
            winLoseBG.color = Color.red;
            winLoseBG.gameObject.SetActive(true);
            StartCoroutine(LoadScene(3));
        }

        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("menu");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            score += 1;
            //Debug.Log("Score: " + score);
            SetScoreText();
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "Trap")
        {
            health -= 1;
            //Debug.Log("Health: " + health);
            SetHealthText();
        }
        else if(other.gameObject.tag == "Goal")
        {
            //Debug.Log("You win!");
            winLoseText.text = "You win!";
            winLoseText.color = Color.black;
            winLoseBG.color = Color.green;
            winLoseBG.gameObject.SetActive(true);
            StartCoroutine(LoadScene(3));
        }
        else if (other.gameObject.tag == "Teleporter")
        {
            if (isTeleporting)
            {
                isTeleporting = false;
            }
            else
            {
                isTeleporting = true;
                other.gameObject.SetActive(false);
                GameObject teleporterCible  = GameObject.FindWithTag("Teleporter");
                transform.position = teleporterCible.transform.position;
                other.gameObject.SetActive(true);
            }
            
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void SetHealthText()
    {
        healthText.text = "Health: " + health;
    }

    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
