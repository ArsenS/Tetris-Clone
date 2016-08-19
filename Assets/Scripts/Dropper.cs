using UnityEngine;
using System.Collections;

public class Dropper : MonoBehaviour
{
    public Grid grid;
    public Mover mover;

    private int level = 1;
    private float stepTimer = 0f;
    private float stepTime = 0.5f;
    private bool waitingForLock = false;
    private float lockTimer = 0f;
    private float lockDelay = 0.0025f;
    

    void Start()
    {
        mover = GetComponent<Mover>();
        grid = GameObject.FindWithTag("Grid").GetComponent<Grid>();
    }

    void Update()
    {

        if (waitingForLock)
        {
            if (!TetriminoCanDrop() && lockTimer >= lockDelay)
            {
                gameObject.tag = "Landed";
                grid.SetTetriminoToGrid(this.gameObject);
                waitingForLock = false;
                lockTimer = 0f;
                grid.ClearFullLines();
            }
            else
            {
                //Debug.Log(Time.deltaTime);
                lockTimer += Time.deltaTime;
            }
        }
        else
        {
            stepTimer += Time.deltaTime;

            if (Input.GetButton("Down"))
            {
                stepTime = 0.015f;
            }
            else
            {
                stepTime = 0.5f;
            }

            if (stepTimer * (level * 0.5f) >= stepTime)
            {
                if (TetriminoCanDrop() && gameObject.tag != "Landed")
                {
                    mover.Move(0f, -1f);
                }
                else
                {
                    waitingForLock = true;
                }

                stepTimer = 0f;
            }
        }
    }

    public bool TetriminoHasLanded()
    {
        return gameObject.tag == "Landed";
    }

    public bool TetriminoCanDrop()
    {
        foreach (Transform cube in transform)
        {
            if (!CubeCanDrop(cube))
            {
                return false;
            }
        }
        return true;
    }

    bool CubeCanDrop(Transform cube)
    {
        if ((Mathf.Round(cube.position.y) - 1f) >= 0f && grid.IsValidPosition((int)Mathf.Round(cube.position.x), (int)Mathf.Round(cube.position.y - 1)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
