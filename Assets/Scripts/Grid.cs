using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    private GameObject[,] gameGrid = new GameObject[10, 20];
    private ArrayList tetriminos = new ArrayList();

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

    public void SetTetriminoToGrid(GameObject tetrimino)
    {
        foreach(Transform block in tetrimino.transform)
        {
            gameGrid[(int)Mathf.Round(block.position.x), (int)Mathf.Round(block.position.y)] = block.gameObject;
        }

        tetriminos.Add(tetrimino);
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
                }
            }
        }
    }

    public void ReinitializeGrid()
    {
        for (int i = 0; i < gameGrid.GetLength(0); i++)
        {
            for (int j = 0; j < gameGrid.GetLength(1); j++)
            {
                Destroy(gameGrid[i, j]);
                gameGrid[i, j] = null;
            }
        }

        foreach (GameObject tetrimino in tetriminos)
        {
            Destroy(tetrimino);
        }

        tetriminos.Clear();
    }
    
}
