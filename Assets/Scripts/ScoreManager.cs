using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    private int score = 0;
    private int linesCleared = 0;
    private Text scoreText;
    

    void Start()
    {
        scoreText = GameObject.Find("GameManager/Canvas/ScoreText/Score").GetComponent<Text>();
    }

    public int GetLinesCleared()
    {
        return linesCleared;
    }

    public void UpdateLinesCleared(int lines)
    {
        linesCleared += lines;
    }

    public void ResetLinesCleared()
    {
        linesCleared = 0;
    }

    public void AddPoints(int linesCleared)
    {
        score += (linesCleared * 100);

        if (linesCleared == 4)
        {
            score += 150;
        }

        UpdateScoreText();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}
