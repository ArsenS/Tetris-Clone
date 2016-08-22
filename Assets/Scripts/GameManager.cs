using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private GameObject[] startText;

    private bool gameStart = false;
    private bool gameRunning = false;
    private bool gameOver = false;

	void Update ()
    {
        gameStart = Input.GetKeyDown(KeyCode.Space);

        if (!gameRunning && gameStart)
        {
            gameRunning = true;

            startText = GameObject.FindGameObjectsWithTag("StartText");
            foreach(GameObject text in startText)
            {
                text.SetActive(false);
            }
            
            transform.Find("TetriminoSpawner").gameObject.SetActive(true);
        }
	}
}
