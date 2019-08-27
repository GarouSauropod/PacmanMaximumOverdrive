using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridUtilities;


public class Pinky : Ghost
{

    public override void Initialize(Square _initialSquare)
    {
        base.Initialize(_initialSquare);
        speed = 1.8f;
        inHoldingPen = true;
    }

}
