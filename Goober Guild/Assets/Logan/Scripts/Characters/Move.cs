using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public MoveBase Base { get; set; }
    public int ME { get; set; }

    public Move(MoveBase cBase, int me)
    {
        Base = cBase;
        ME = me;
    }
}
