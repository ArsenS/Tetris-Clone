using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
   
    public GameObject[] tetriminos = new GameObject[7];

    private GameObject tetrimino;
    private Dropper dropper;

    void Start()
    {
        
        tetrimino = SpawnRandomTetrimino();
        dropper = tetrimino.GetComponent<Dropper>();
    }

    void Update()
    {
        if (dropper.TetriminoHasLanded() || !dropper.TetriminoCanDrop())
        {
            tetrimino = SpawnRandomTetrimino();
            dropper = tetrimino.GetComponent<Dropper>();
        }
    }

    GameObject SpawnRandomTetrimino()
    {
        int index = Random.Range(0, tetriminos.Length);

        return (GameObject)Instantiate(tetriminos[index], new Vector3(5f, 19f, 0f), Quaternion.identity);
    }

    public GameObject GetCurrentTetrimino()
    {
        return tetrimino;
    }
}
