using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    CharacterBase _base;
    int level;

    public Character(CharacterBase cBase, int cLevel)
    {
        _base = cBase;
        level = cLevel;
    }

    public int Attack
    {
        get { return Mathf.FloorToInt((_base.Attack * level) / 100f) + 5; }
    }


}
