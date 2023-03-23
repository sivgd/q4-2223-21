using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseEnemy
{
    public string name;

    public enum Type
    {
        DEFENSE,
        SPEED,
        DAMAGE
    }
    
    public enum Rarity
    {
        COMMON,
        UNCOMMON,
        RARE,
    }

    public Type EnemyType;
    public Rarity rarity;

    public float baseHP;
    public float currentHP;
    public float baseMP;
    public float currentMP;
    public float baseATK;
    public float currentATK;
    public float baseDEF;
    public float currentDEF;
}
