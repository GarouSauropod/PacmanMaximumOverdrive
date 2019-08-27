using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridUtilities;


public class Blinky : Ghost
{

    public override void Initialize(Square _initialSquare)
    {
        base.Initialize(_initialSquare);
        speed = 2.2f;
        inHoldingPen = false;
    }

}
