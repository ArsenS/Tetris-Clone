using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    private Grid grid;
    private Dropper dropper;
    private Mover mover;

    private bool rotate = false;

    void Start()
    {
        grid = GameObject.FindWithTag("Grid").GetComponent<Grid>();
        dropper = GetComponent<Dropper>();
        mover = GetComponent<Mover>();
    }
	
	void Update()
    {
        rotate = Input.GetKeyDown(KeyCode.Space);

        if (rotate && dropper.TetriminoCanDrop())
        {
            Rotate();
        }
	}

    void Rotate()
    {
        if (gameObject.tag == "Current")
        {
            gameObject.transform.rotation *= Quaternion.Euler(0, 0, 90);
            if (!TetriminoCanRotate())
            {
                gameObject.transform.rotation *= Quaternion.Euler(0, 0, -90);
            }
            AdjustTetrimino();
        }
    }

    bool TetriminoCanRotate()
    {
        foreach (Transform block in transform)
        {
            if (!grid.IsValidPosition((int)Mathf.Round(block.position.x), (int)Mathf.Round(block.position.y)))
            {
                return false;
            }
        }
        return true;
    }

    void AdjustTetrimino()
    {
        if (!mover.TetriminoWithinBorders())
        {
            if (Mathf.Round(transform.position.x) < 5)
            {
                mover.Move(1f, 0f);
            }
            else
            {
                mover.Move(-1f, 0f);
            }
        }
    }
}
