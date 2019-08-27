using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridUtilities;


public class Clyde : Ghost
{

    public override void Initialize(Square _initialSquare)
    {
        base.Initialize(_initialSquare);
        speed = 1f;
        inHoldingPen = true;
    }

    public override void CalculatePath(Square _start, Square _destination)
    {
        path = PathFinder.Instance.FindPath(_start, _destination);
    }

}
