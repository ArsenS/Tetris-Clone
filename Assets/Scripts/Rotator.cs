using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    private Dropper dropper;
    private Mover mover;
    private bool rotate = false;

    void Start()
    {
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
        if (tag == "Current")
        {
            gameObject.transform.rotation *= Quaternion.Euler(0, 0, 90);
            AdjustTetrimino();
        }
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
