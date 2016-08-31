using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    private int gameLevel;

    private GameObject[] startText;
    private GameObject gameOverText;
    private Text levelText;
    private Text linesText;

    private Spawner spawner;
    private Grid grid;
    private ScoreManager scoreManager;

    private bool gameStart = false;
    private bool gameRunning = false;

    void Start ()
    {
        spawner = transform.Find("TetriminoSpawner").gameObject.GetComponent<Spawner>();
        grid = GameObject.FindWithTag("Grid").GetComponent<Grid>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

        startText = GameObject.FindGameObjectsWithTag("StartText");
        gameOverText = transform.Find("Canvas/GameOverText").gameObject;
        levelText = GameObject.Find("Canvas/LevelText/Level").GetComponent<Text>();
        linesText = GameObject.Find("Canvas/LinesText/Lines").GetComponent<Text>();
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        gameStart = Input.GetKeyDown(KeyCode.Space);
        
        if (!gameRunning && gameStart)
        {
            StartGame();
            scoreManager.ResetScore();
            ResetLevel();
            UpdateLevelText();
            scoreManager.ResetLinesCleared();
        }

        if((scoreManager.GetLinesCleared() / 10 + 1) > gameLevel)
        {
            NextLevel();
            UpdateLevelText();
        }

        UpdateLinesText();

        if (spawner.gameObject.activeSelf && spawner.GetLevelForCurrentTetrimino() != gameLevel)
        {
            spawner.SetLevelForCurrentTetrimino(gameLevel);
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
        ResetLevel();
        gameOverText.SetActive(true);
    }

    void UpdateLevelText()
    {
        levelText.text = gameLevel.ToString();
    }

    void NextLevel()
    {
        if (gameLevel < 10)
        {
            gameLevel++;
        } 
    }

    void ResetLevel()
    {
        gameLevel = 1;
    }

    void UpdateLinesText()
    {
        linesText.text = scoreManager.GetLinesCleared().ToString();
    }
}
