using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private GameObject[] startText;
    private GameObject gameOverText;

    private Spawner spawner;
    private Grid grid;

    private bool gameStart = false;
    private bool gameRunning = false;

    void Start ()
    {
        spawner = transform.Find("TetriminoSpawner").gameObject.GetComponent<Spawner>();
        grid = GameObject.FindWithTag("Grid").GetComponent<Grid>();

        startText = GameObject.FindGameObjectsWithTag("StartText");
        gameOverText = transform.Find("Canvas/GameOverText").gameObject;
    }

	void Update ()
    {
        gameStart = Input.GetKeyDown(KeyCode.Space);

        if (!gameRunning && gameStart)
        {
            StartGame();
        }

        if (spawner.gameObject.activeSelf && spawner.IsGameOver())
        {
            GameOver();
        }
	}

    void StartGame()
    {
        gameRunning = true;

        if (gameOverText != null)
        {
            gameOverText.SetActive(false);
        }

        foreach (GameObject text in startText)
        {
            text.SetActive(false);
        }

        spawner.gameObject.SetActive(true);
    }

    void GameOver()
    {
        gameRunning = false;
        spawner.DestroyTetriminos();
        spawner.gameObject.SetActive(false);
        grid.ReinitializeGrid();
        gameOverText.SetActive(true);
    }
}
