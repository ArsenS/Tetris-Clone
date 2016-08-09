using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    public Mover mover;

    private GameObject[,] gameBoard = new GameObject[10, 20];

    public bool IsValidPosition(int x, int y)
    {
        if (x >= 0 && x < 10 && y >= 0 && y < 20 && gameBoard[x, y] == null)
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
            gameBoard[(int)Mathf.Round(cube.position.x), (int)Mathf.Round(cube.position.y)] = cube.gameObject;
        }
        //PrintBoard();
    }

    public void ClearBoard()
    {
        for (int line = 0; line < gameBoard.GetLength(1); line++)
        {
            if (LineIsFull(line))
            {
                Debug.Log("FULLLLLLHOUSE");
                ClearLine(line);
                //line--;
            }
        }
    }

    bool LineIsFull(int line)
    {
        for (int block = 0; block < gameBoard.GetLength(0); block++)
        {
            //Debug.Log("x: " + line + ", y: " + i);
            //Debug.Log(gameBoard[square, line]);
            if (gameBoard[block, line] == null)
            {
                return false;
            }
        }
        return true;
    }

    void ClearLine(int line)
    {
        for (int block = 0; block < gameBoard.GetLength(0); block++)
        {
            Destroy(gameBoard[block, line]);
        }
        DropBlocksOnLine(line + 1);
    }

    
    void DropBlocksOnLine(int line)
    {
        for (int block = 0; block < gameBoard.GetLength(0); block++)
        {

            //this aint working
            Debug.Log("Before: " + gameBoard[block, line - 1]);
            gameBoard[block, line - 1] = gameBoard[block, line];
            Debug.Log("After: " + gameBoard[block, line - 1]);
            gameBoard[block, line] = null;
            Debug.Log("After after: " + gameBoard[block, line - 1]);
            mover = gameBoard[block, line - 1].transform.parent.gameObject.GetComponent<Mover>();
            mover.Move(0f, -1f);
        }
    }
    
}
