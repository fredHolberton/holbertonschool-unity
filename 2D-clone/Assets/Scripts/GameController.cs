using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject[] player;

    public TextMeshProUGUI[] title;

    public TextMeshProUGUI[] levelValue;
    public TextMeshProUGUI[] lineValue;
    public TextMeshProUGUI[] scoreValue;

    
    private int[] levelPlayer;
    private int[] linePlayer;
    private int[] scorePlayer;
    private int nbPlayers;
    private int currentPlayer;

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

        if (nbPlayers == 1)
        {
            player[1].SetActive(false);
        }

        currentPlayer = 0;

        InitPlayers();

        // Lancement du jeu
        gameObject.GetComponent<GameManager>().enabled = true;
    }

    private void InitPlayers()
    {
        // Init player's display
        levelPlayer[currentPlayer] = 1;
        linePlayer[currentPlayer] = 0;
        scorePlayer[currentPlayer] = 0;

        // title of current player is colored in red
        title[currentPlayer].color = Color.red;
        if (currentPlayer == 0)
            title[1].color = Color.white;
        else
            title[0].color = Color.white;
    }

    public void ChangeGamer()
    {
        // Desactivate the GameManager script
        gameObject.GetComponent<GameManager>().enabled = false;
        // Save score
        GameplayController.SaveScore(scorePlayer[currentPlayer], currentPlayer);

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

        // Activate the GameManager script
        gameObject.GetComponent<GameManager>().InitGame();
        gameObject.GetComponent<GameManager>().enabled = true;
    }

    public void AddALine()
    {
        linePlayer[currentPlayer] += 1;
        lineValue[currentPlayer].text = string.Format("{0}", linePlayer[currentPlayer]);

        if (linePlayer[currentPlayer] % 10 == 0)
        {
            levelPlayer[currentPlayer] += 1;
            levelValue[currentPlayer].text = string.Format("{0}", levelPlayer[currentPlayer]);
        }
    }
}
