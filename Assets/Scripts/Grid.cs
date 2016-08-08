using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    
    private GameObject[,] gameBoard = new GameObject[10, 20];


    public bool IsValidPosition(int x, int y)
    {
        if (x >= 0 && x <= 10 && y >= 0 && y <= 20 && gameBoard[x, y] == null)
        {
            return true;
        }

        else return false;
    }

    void PrintBoard()
    {
        for (int i = gameBoard.GetLength(0) - 1; i >= 0; i--)
        {
            Debug.Log("Line " + i);
            for (int j = 0; j < gameBoard.GetLength(1); j++)
            {
                if(gameBoard[i, j] != null)
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
            gameBoard[(int)cube.position.x, (int)cube.position.y] = cube.gameObject;
        }
        //PrintBoard();
    }

    bool LineIsFull(int index)
    {
        for (int j = 0; j < gameBoard.GetLength(0); j++)
        {
            if (gameBoard[index, j] == null)
            {
                return false;
            }
        }

        return true;
    }

    void ClearLine(int index)
    {
        for (int j = 0; j < gameBoard.GetLength(0); j++)
        {
            Destroy(gameBoard[index, j]);
        }
    }

    
    void DropBlocksByOne()
    {
        for (int i = gameBoard.GetLength(0) - 1; i >= 0; i--)
        {
            for (int j = 0; j < gameBoard.GetLength(1); j++)
            {
                gameBoard[i, j] = gameBoard[i - 1, j];
            }
        }
    }
    
}
