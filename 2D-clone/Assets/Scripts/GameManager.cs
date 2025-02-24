using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage the game for starting the random genenation of a tetromino ans his movment
/// </summary>
public class GameManager : MonoBehaviour
{
    public GameObject[] tetrominos;
    public float movmentFrequency = 0.8f;
    private float passedTime = 0;
    private GameObject currentTetromino;

    // Start is called before the first frame update
    void Start()
    {
        SpawnTetromino();
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

    // Random generation and instantiation of a new tetromino
    void SpawnTetromino()
    {
        int index = UnityEngine.Random.Range(0, tetrominos.Length);
        Debug.Log(tetrominos[index].name);
        currentTetromino = Instantiate(tetrominos[index], new Vector3(5, 18, 0), Quaternion.identity);
    }

    // Managment of the movments of current tetromino
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

    void UserInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("Current position: " + currentTetromino.transform.position.x + ", " + currentTetromino.transform.position.y);
            MoveTetromino(Vector3.left);
            Debug.Log("Current position: " + currentTetromino.transform.position.x + ", " + currentTetromino.transform.position.y);
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
            movmentFrequency = 0.2f;
        }
        else
        {
             movmentFrequency = 0.8f;
        }
    }

    bool IsValidPosition()
    {
        return GetComponent<GridController>().IsValidPosition(currentTetromino.transform);
    }
    
    void CheckForLines()
    {
        GetComponent<GridController>().CheckForLines();
    }

    
}
