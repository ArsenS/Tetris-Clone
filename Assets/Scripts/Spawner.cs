using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject[] tetriminos = new GameObject[7];

    private bool holdTetrimino = false;

    private GameObject currentTetrimino;
    private GameObject nextTetrimino;
    private GameObject heldTetrimino;

    void Start()
    {
        currentTetrimino = SpawnRandomTetrimino();
        ReadyTetrimino(currentTetrimino);
        heldTetrimino = null;
    }

    void Update()
    {
        holdTetrimino = Input.GetKeyDown(KeyCode.LeftControl);

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
}
