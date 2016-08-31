using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

    private float moveHorizontal;

    private Grid grid;
    private float timer = 0f;
    private float stepTime = 0.075f;

    void Start()
    {
        grid = GameObject.FindWithTag("Grid").GetComponent<Grid>();
    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");

        timer += Time.deltaTime;
        if (timer >= stepTime && TetriminoCanMove(moveHorizontal, 0f))
        {
            Move(moveHorizontal, 0f);
            timer = 0f;
        }
    }

    public bool TetriminoWithinBorders(float horizontalMove = 0f)
    {
        foreach (Transform block in transform)
        {
            if (!blockWithinBorders(block, horizontalMove))
            {
                return false;
            }
        }
        return true;
    }

    bool blockWithinBorders(Transform block, float horizontalMove = 0f) 
    {
        if (Mathf.Round(block.position.x + horizontalMove) >= 0 && Mathf.Round(block.position.x + horizontalMove) <= 9)
        {
            return true;
        }

        else return false;
    }

    bool TetriminoCanMove(float horizontalMove, float verticalMove)
    {
        foreach (Transform block in transform)
        {
            if (!grid.IsValidPosition((int)Mathf.Round(block.position.x + horizontalMove), (int)Mathf.Round(block.position.y + verticalMove)))
            {
                return false;
            }
        }

        return true;
    }

    public void Move(float moveHorizontal, float moveVertical)
    {
        if (TetriminoWithinBorders(moveHorizontal))
        {
            if (gameObject.tag == "Current" && TetriminoCanMove(moveHorizontal, moveVertical))
            {
                gameObject.transform.position = new Vector3(Mathf.Round(gameObject.transform.position.x + moveHorizontal), Mathf.Round(gameObject.transform.position.y + moveVertical), gameObject.transform.position.z);
            }
        }
    }
}




