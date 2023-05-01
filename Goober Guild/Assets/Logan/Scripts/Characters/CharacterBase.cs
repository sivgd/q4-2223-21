using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Character", menuName = "Character/Create new character")]
public class CharacterBase : ScriptableObject
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] public Sprite FrontSprite;
    [SerializeField] public Sprite BackSprite;

    [SerializeField] public CharacterType type1;
    [SerializeField] public CharacterType type2;

    //Base Stats
    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;

    [SerializeField] List<LearnableMove> learnableMoves;

    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public int MaxHp
    {
        get { return maxHp; }
    }

    public int Attack
    {
        get { return attack; }
    }

    public int SpAttack
    {
        get { return spAttack; }
    }

    public int Defense
    {
        get { return defense; }
    }

    public int SpDefense
    {
        get { return spDefense; }
    }

    public int Speed
    {
        get { return speed; }
    }

    public List<LearnableMove> LearnableMoves
    {
        get { return learnableMoves; }
    }
}

[System.Serializable]

public class LearnableMove
{
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;

    public MoveBase Base
    {
        get { return moveBase; }
    }

    public int Level
    {
        get { return level; }
    }
}

public enum CharacterType
{
    None,
    DAMAGE,
    SPEED,
    DEFENSE
}

public class TypeChart
{
    static float[][] chart =
    {
        //                  SPD  DEF  DMG
        /*SPD*/ new float[] {1f, 2f, 0.5f },
        /*DEF*/ new float[] {0.5f, 1f, 2f },
        /*DMG*/ new float[] {2, 0.5f, 1f }
    };

    public static float GetEffectiveness(CharacterType attackType, CharacterType defenseType)
    {
        if (attackType == CharacterType.None || defenseType == CharacterType.None)
            return 1;

        int row = (int)attackType - 1;
        int col = (int)defenseType - 1;

        return chart[row][col];
    }

}