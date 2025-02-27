using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Controle the game, switch player and save best scores
/// </summary>
public class GameController : MonoBehaviour
{
    public GameObject[] player;
    public TextMeshProUGUI[] title;
    public TextMeshProUGUI[] levelValue;
    public TextMeshProUGUI[] lineValue;
    public TextMeshProUGUI[] scoreValue;
    public AudioController audioController;

    private int[] levelPlayer;
    private int[] linePlayer;
    private int[] scorePlayer;
    private int nbPlayers;
    private int currentPlayer;
    private bool isWaitingForNextGamer;

    // Start is called before the first frame update
    void Start()
    {
        nbPlayers = GameplayController.nbPlayers;
        levelPlayer = new int[nbPlayers];
        linePlayer = new int[nbPlayers];
        scorePlayer = new int[nbPlayers];
        for (int i = 0; i < nbPlayers; i++)
        {
            levelPlayer[i] = 1;
            linePlayer[i] = 0;
            scorePlayer[i] = 0;
        }

        InitBestScores();

        if (nbPlayers == 1)
        {
            player[1].SetActive(false);
        }

        currentPlayer = 0;

        InitPlayers();

        // Lancement du jeu
        gameObject.GetComponent<GameManager>().enabled = true;
        isWaitingForNextGamer = false;
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetButtonDown("Submit") && isWaitingForNextGamer)
        {
            isWaitingForNextGamer = false;
            GameObject.Find("GameOverCanvas").GetComponent<Canvas>().enabled = false;
            ChangeGamer();
            
        }
    }

    /* Initialize the informations of the players and reset displays */
    void InitPlayers()
    {
        // Init player's display
        levelPlayer[currentPlayer] = 1;
        linePlayer[currentPlayer] = 0;
        scorePlayer[currentPlayer] = 0;
        levelValue[currentPlayer].text = "1";
        lineValue[currentPlayer].text = "0";
        scoreValue[currentPlayer].text = "0";

        // title of current player is colored in red
        title[currentPlayer].color = new Color(0.76f, 0.31f, 0.14f, 1f);
        if (currentPlayer == 0)
            title[1].color = Color.white;
        else
            title[0].color = Color.white;

        //GetComponent<AudioSource>().Stop();
        audioController.Play();
    }

    /* Save score and switch gamer if there are two gamers */
    void ChangeGamer()
    {
        // Pause the Game
        Time.timeScale = 0;
        // Save score
        SaveScore();

        // If two players, switch player
        if (nbPlayers == 2)
        {
            if (currentPlayer == 0)
                currentPlayer = 1;
            else
                currentPlayer = 0;
        }
    
        // Init players
        InitPlayers();

        // Init and Activate the Game
        gameObject.GetComponent<GameManager>().InitGame();
        Time.timeScale = 1;
    }

    /* Get best scores from Player Prefs */
    void InitBestScores()
    {
        if (PlayerPrefs.HasKey("BestScorePlayer1"))
        {
           GameplayController.hightScorePlayer1 =  PlayerPrefs.GetInt("BestScorePlayer1");
        }
        else
        {
             GameplayController.hightScorePlayer1 = 0;
        }

        if (PlayerPrefs.HasKey("BestScorePlayer2"))
        {
           GameplayController.hightScorePlayer2 =  PlayerPrefs.GetInt("BestScorePlayer2");
        }
        else
        {
             GameplayController.hightScorePlayer2 = 0;
        }
    }

    /* Save the current player score if it is is best one */
    void SaveScore()
    {
        if (currentPlayer == 0)
        {
            if (scorePlayer[currentPlayer] > GameplayController.hightScorePlayer1)
            {
                GameplayController.hightScorePlayer1 = scorePlayer[currentPlayer];
                PlayerPrefs.SetInt("BestScorePlayer1", GameplayController.hightScorePlayer1);
            }
        }
        else
        {
            if (scorePlayer[currentPlayer] > GameplayController.hightScorePlayer2)
            {
                GameplayController.hightScorePlayer2 = scorePlayer[currentPlayer];
                PlayerPrefs.SetInt("BestScorePlayer2", GameplayController.hightScorePlayer2);
            }
        }
    }
    
    /// <summary>
    /// Display Game over text and switch boolean to wait the player to press Enter key
    /// </summary>
    public void GameOver()
    {
        Time.timeScale = 0;
        GameObject.Find("GameOverCanvas").GetComponent<Canvas>().enabled = true; 
        isWaitingForNextGamer = true;
        audioController.PlayGameOver();
        SaveScore();

    }

    /// <summary>
    /// Add a line to the current player and set the level up if it is necessary
    /// </summary>
    public void AddALine()
    {
        // Play the sound for one line more
        audioController.PlayNewLine();
        // add and display the number of lines
        linePlayer[currentPlayer] += 1;
        lineValue[currentPlayer].text = string.Format("{0}", linePlayer[currentPlayer]);

        if (linePlayer[currentPlayer] % 10 == 0)
        {
            // 10 lines more: next level ! Play sound for next level
            audioController.PlayNewLevel();
            levelPlayer[currentPlayer] += 1;
            levelValue[currentPlayer].text = string.Format("{0}", levelPlayer[currentPlayer]);
        }
        // Compute the new score
        scorePlayer[currentPlayer] += 10 * levelPlayer[currentPlayer];
        scoreValue[currentPlayer].text = string.Format("{0}", scorePlayer[currentPlayer]);

    }

    /// <summary>
    /// Compute the frequency corresponding to the current player level
    /// </summary>
    /// <returns></returns>
    public float GetFrequency()
    {
        return (float)(1.1 - (levelPlayer[currentPlayer] * 0.1));
    }
}
