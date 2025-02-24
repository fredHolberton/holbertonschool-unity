using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Class to manage the movments and controls in the grid
/// </summary>
public class GridController : MonoBehaviour
{
    public Transform[,] grid;
    public int width, height;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Transform[width, height];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Upgrade the grid when a tetromino is moved
    /// </summary>
    /// <param name="tetromino"></param>
    public void UpdateGrid(Transform tetromino)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    if (grid[x, y].parent == tetromino)
                    {
                        grid[x, y] = null;
                    }
                }
            }
        }

        foreach (Transform mino in tetromino)
        {
            Vector2 pos = Round(mino.position);
            if (pos.y < height)
            {
                grid[(int)pos.x, (int)pos.y] = mino;
            }
        }
    }

    /// <summary>
    /// Return the transform of a tetromino at his position
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public Transform GetTransformAtGridPosition(Vector2 pos)
    {
        if (pos.y > height - 1)
        {
            return null;
        }
        return grid[(int)pos.x, (int)pos.y];
    }
    
    /// <summary>
    /// Returns true if the tetromino is inside the grid limits, otherwise returns false
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public bool IsInsideBorder(Vector2 pos)
    {
        return (int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0 && (int)pos.y < height;
    }

    /// <summary>
    /// Return true if the position of the tetromino is valid, otherwise return false
    /// </summary>
    /// <param name="tetromino"></param>
    /// <returns></returns>
    public bool IsValidPosition(Transform tetromino)
    {
        foreach (Transform mino in tetromino)
        {
            Vector2 pos = Round(mino.position);
            if (!IsInsideBorder(pos))
            {
                return false;
            }
            if (GetTransformAtGridPosition(pos) != null && GetTransformAtGridPosition(pos).parent != tetromino)
            {
                return false;
            }
        }
        return true;
    }
    
    /// <summary>
    /// Round the position x and y of a mino
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector2 Round(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public void CheckForLines()
    {
        for (int y = 0; y < height; y++)
        {
            if (LineIsFull(y))
            {
                DeleteLine(y);
                DecreaseRowAbove(y + 1);
                y--;
            }
        }
    }

    bool LineIsFull(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }

    void DeleteLine(int y)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    void DecreaseRowAbove(int startRow)
    {
        for (int y = startRow; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;
                    grid[x, y - 1].position += Vector3.down;
                }
            }
        }
    }
}
