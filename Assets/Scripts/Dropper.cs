﻿using UnityEngine;
using System.Collections;

public class Dropper : MonoBehaviour
{
    public Grid grid;
    public Mover mover;

    private int level = 1;
    private float stepTime = 0.5f;
    private float timer = 0f;

    void Start()
    {
        mover = GetComponent<Mover>();
        grid = GameObject.FindWithTag("Grid").GetComponent<Grid>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer * (level * 0.5f) >= stepTime)
        {
            if (gameObject.tag != "Landed" && TetriminoCanDrop())
            {
               mover.Move(0f, -1f);
            }
            else
            {
                gameObject.tag = "Landed";
                grid.SetTetriminoToGrid(this.gameObject);
                grid.ClearBoard();
            }

            timer = 0f;
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