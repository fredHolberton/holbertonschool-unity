using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage the game for starting the random genenation of a tetromino ans his movment
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Array of tetrominos
    /// </summary>
    public GameObject[] tetrominos;
    
    /// <summary>
    /// Frequency for the movment of a tetromino
    /// </summary>
    public float movmentFrequency;

    private float passedTime = 0;
    private GameObject currentTetromino;

    // Start is called before the first frame update
    void Start()
    {
        InitGame();
    }

    // Update is called once per frame
    void Update()
    {
        passedTime += Time.deltaTime;
        if (passedTime >= movmentFrequency)
        {
            passedTime -= movmentFrequency;
            MoveTetromino(Vector3.down);
        }
        UserInput();
    }

    /* Random generation and instantiation of a new tetromino */
    void SpawnTetromino()
    {
        int index = UnityEngine.Random.Range(0, tetrominos.Length);
        currentTetromino = Instantiate(tetrominos[index], new Vector3(4, 18, 0), Quaternion.identity);
        if (!IsValidPosition())
        {
            Destroy(currentTetromino);
            GetComponent<GameController>().GameOver();
        }
    }

    /* Managment of the movments of current tetromino */
    void MoveTetromino(Vector3 direction)
    {
        currentTetromino.transform.position += direction;

        if (!IsValidPosition())
        {
            currentTetromino.transform.position -= direction;
            if (direction == Vector3.down)
            {
                GetComponent<GridController>().UpdateGrid(currentTetromino.transform);
                CheckForLines();
                SpawnTetromino();
            }
        }
    }

    /* Listen the player inputs */
    void UserInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveTetromino(Vector3.left);
        } 
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveTetromino(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentTetromino.transform.Rotate(0, 0, 90);
            if (!IsValidPosition())
            {
                currentTetromino.transform.Rotate(0, 0, -90);
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movmentFrequency = 0.1f;
        }
        else
        {
             movmentFrequency = GetComponent<GameController>().GetFrequency();
        }
    }

    /* return true if the new position of the current tetromino is valid */
    bool IsValidPosition()
    {
        return GetComponent<GridController>().IsValidPosition(currentTetromino.transform);
    }
    
    /* Check if there are any full lines */
    void CheckForLines()
    {
        GetComponent<GridController>().CheckForLines();
    }

    /// <summary>
    /// Initialise the game at start for a player
    /// </summary>
    public void InitGame()
    {
        GetComponent<GridController>().InitGame();
        SpawnTetromino();
    }    
}
