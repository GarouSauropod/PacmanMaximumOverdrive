using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridUtilities;


public class Inky : Ghost
{

    public override void Initialize(Square _initialSquare)
    {
        base.Initialize(_initialSquare);
        speed = 1.4f;
        inHoldingPen = true;
    }

}
