using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    private GameObject[,] gameGrid = new GameObject[10, 20];

    public bool IsValidPosition(int x, int y)
    {
        if (x >= 0 && x < 10 && y >= 0 && y < 20 && gameGrid[x, y] == null)
        {
            return true;
        }
        else if (y >= 20)
        {
            return true;
        }
        else return false;
    }

    void PrintBoard()
    {
        for (int i = gameGrid.GetLength(0) - 1; i >= 0; i--)
        {
            Debug.Log("Line " + i);
            for (int j = 0; j < gameGrid.GetLength(1); j++)
            {
                if(gameGrid[i, j] != null)
                {
                    Debug.Log("Yes: "+i+", "+j);
                }
            }
            Debug.Log("---------------");
        }
    }

    public void SetTetriminoToGrid(GameObject tetrimino)
    {
        foreach(Transform cube in tetrimino.transform)
        {
            gameGrid[(int)Mathf.Round(cube.position.x), (int)Mathf.Round(cube.position.y)] = cube.gameObject;
        }
    }

    public void ClearFullLines()
    {
        for (int line = 0; line < gameGrid.GetLength(1); line++)
        {
            if (LineIsFull(line))
            {
                ClearLine(line);
                DropBlocks(line);
                line--;
            }
        }
    }

    bool LineIsFull(int line)
    {
        for (int block = 0; block < gameGrid.GetLength(0); block++)
        {
            if (gameGrid[block, line] == null)
            {
                return false;
            }
        }
        return true;
    }

    void ClearLine(int line)
    {
        for (int block = 0; block < gameGrid.GetLength(0); block++)
        {
            Destroy(gameGrid[block, line]);
            gameGrid[block, line] = null;
        }
    }

    
    void DropBlocks(int startLine)
    {
        for (int line = startLine; line < 19; line++)
        {
            for (int block = 0; block < gameGrid.GetLength(0); block++)
            {
                if (gameGrid[block, line + 1] != null)
                {
                    gameGrid[block, line] = gameGrid[block, line + 1];
                    gameGrid[block, line + 1] = null;
                    gameGrid[block, line].transform.position = new Vector3(gameGrid[block, line].transform.position.x, Mathf.Round(gameGrid[block, line].transform.position.y - 1f), gameGrid[block, line].transform.position.z);
                    //Debug.Log("y: "+gameGrid[block, line].transform.position.y);
                }
            }
        }
    }
    
}
