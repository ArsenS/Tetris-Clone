using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

    public Grid grid;
    public float moveHorizontal;
    public float moveVertical;

    private float timer = 0f;
    private float stepTime = 0.075f;

    private Dropper dropper;

    void Start()
    {
        dropper = GetComponent<Dropper>();
        grid = GameObject.FindWithTag("Grid").GetComponent<Grid>();
    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        if (moveHorizontal != 0 || moveVertical > 0)
        {
            moveVertical = 0;
        }

        timer += Time.deltaTime;
        if (timer >= stepTime && dropper.TetriminoCanDrop())
        {
            Move(moveHorizontal, moveVertical);
            timer = 0f;
        }
    }

    public bool TetriminoWithinBorders(float horizontalMove = 0f)
    {
        foreach (Transform cube in transform)
        {
            if (!CubeWithinBorders(cube, horizontalMove))
            {
                return false;
            }
        }
        return true;
    }

    bool CubeWithinBorders(Transform cube, float horizontalMove = 0f) 
    {
        if (Mathf.Round(cube.position.x + horizontalMove) >= 0 && Mathf.Round(cube.position.x + horizontalMove) <= 9)
        {
            return true;
        }

        else return false;
    }

    //cube not used
    //what the shit?? fix?
    bool TetriminoCanMove(float horizontalMove, float verticalMove)
    {
        foreach (Transform cube in transform)
        {
            if (!grid.IsValidPosition((int)Mathf.Round(cube.position.x + horizontalMove), (int)Mathf.Round(cube.position.y + verticalMove)))
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
            if (gameObject.tag != "Landed" && TetriminoCanMove(moveHorizontal, moveVertical))
            {
                gameObject.transform.position = new Vector3(Mathf.Round(gameObject.transform.position.x + moveHorizontal), Mathf.Round(gameObject.transform.position.y + moveVertical), gameObject.transform.position.z);
            }
        }
    }
}




