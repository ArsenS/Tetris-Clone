using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject[] tetriminos = new GameObject[7];

    private bool gameOverState = false;
    private bool holdTetrimino = false;
    
    private GameObject currentTetrimino = null;
    private GameObject nextTetrimino = null;
    private GameObject heldTetrimino = null;

    private Grid grid;

    void Awake()
    {
        grid = GameObject.FindWithTag("Grid").GetComponent<Grid>();
    }

    void OnEnable()
    {
        gameOverState = false;
        currentTetrimino = SpawnRandomTetrimino();
        ReadyTetrimino(currentTetrimino);
    }

    void Update()
    {
        holdTetrimino = Input.GetKeyDown(KeyCode.LeftControl);

        if (!gameOverState)
        {
            if (holdTetrimino)
            {
                holdCurrentTetrimino(currentTetrimino);
            }

            if (currentTetrimino.tag == "Landed")
            {
                currentTetrimino = nextTetrimino;
                ReadyTetrimino(currentTetrimino);
            }
        }
    }

    GameObject SpawnRandomTetrimino()
    {
        int index = Random.Range(0, tetriminos.Length);

        return (GameObject)Instantiate(tetriminos[index], new Vector3(-6f, 15.5f, 0f), tetriminos[index].transform.rotation);
    }

    void ReadyTetrimino(GameObject tetrimino)
    {
        tetrimino.tag = "Current";
        currentTetrimino = tetrimino;
        SendTetriminoToBoard(tetrimino);
        nextTetrimino = SpawnRandomTetrimino();
    }

    void SendTetriminoToBoard(GameObject tetrimino)
    {
        tetrimino.transform.position += new Vector3(10f, 3.5f, 0f);

        foreach (Transform block in tetrimino.transform)
        {
            if (!grid.IsValidPosition((int)Mathf.Round(block.position.x), (int)Mathf.Round(block.position.y)))
            {
                SetGameOver();
            }
        }
    }

    void holdCurrentTetrimino(GameObject tetrimino)
    {
        if (heldTetrimino == null)
        {
            tetrimino.tag = "Held";
            heldTetrimino = tetrimino;
            ReadyTetrimino(nextTetrimino);
            heldTetrimino.transform.position = new Vector3(-6f, 8f, 0f);
        }
        else
        {
            SwapWithHeldTetrimino(tetrimino);
        }
    }

    void SwapWithHeldTetrimino(GameObject tetrimino)
    {
        tetrimino.transform.position = heldTetrimino.transform.position;
        heldTetrimino.transform.position = new Vector3(4f, 19f, 0f);
        
        currentTetrimino = heldTetrimino;
        currentTetrimino.tag = "Current";
        heldTetrimino = tetrimino;
        heldTetrimino.tag = "Held";
    }

    public void DestroyTetriminos()
    {
        Destroy(currentTetrimino);
        Destroy(heldTetrimino);
        Destroy(nextTetrimino);
    }

    void SetGameOver()
    {
        gameOverState = true;
    }

    public bool IsGameOver()
    {
        return gameOverState;
    }
}
