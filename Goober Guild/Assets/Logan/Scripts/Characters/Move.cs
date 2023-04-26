using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public MoveBase Base { get; set; }

    public int ME { get; set; }

    public Move(MoveBase cBase)
    {
        Base = cBase;
        ME = cBase.ME;
    }
}
