using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionsDB
{
    public static void Init()
    {
        foreach (var kvp in Conditions)
        {
            var conditionId = kvp.Key;
            var condition = kvp.Value;

            condition.Id = conditionId;
        }
    }

    public static Dictionary<ConditionID, Condition> Conditions { get; set; } = new Dictionary<ConditionID, Condition>()
    {
        {
            ConditionID.psn,
            new Condition()
            {
                Name = "Poison",
                StartMessage = "has been poisoned",
                OnAfterTurn = (Character character) =>
                {
                    character.UpdateHP(character.MaxHp / 8);
                    character.StatusChanges.Enqueue($"{character.Base.Name} hurt itself due to poison");
                }
            }
        },
        {
            ConditionID.brn,
            new Condition()
            {
                Name = "Burn",
                StartMessage = "has been burned",
                OnAfterTurn = (Character character) =>
                {
                    character.UpdateHP(character.MaxHp / 16);
                    character.StatusChanges.Enqueue($"{character.Base.Name} hurt itself due to burn");
                }
            }
        },
        {
            ConditionID.par,
            new Condition()
            {
                Name = "Paralyzed",
                StartMessage = "has been paralyzed",
                OnBeforeMove = (Character character) =>
                {
                    if  (Random.Range(1, 5) == 1)
                    {
                        character.StatusChanges.Enqueue($"{character.Base.Name}'s paralyzed and can't move");
                        return false;
                    }

                    return true;
                }
            }
        },
        {
            ConditionID.frz,
            new Condition()
            {
                Name = "Freeze",
                StartMessage = "has been frozen",
                OnBeforeMove = (Character character) =>
                {
                    if  (Random.Range(1, 5) == 1)
                    {
                        character.CureStatus();
                        character.StatusChanges.Enqueue($"{character.Base.Name}'s is not frozen anymore");
                        return true;
                    }

                    return false;
                }
            }
        },
        {
            ConditionID.slp,
            new Condition()
            {
                Name = "Sleep",
                StartMessage = "has fallen asleep",
                OnStart = (Character character) =>
                {
                    // Sleep for 1-3 turns
                    character.StatusTime = Random.Range(1, 4);
                    Debug.Log($"Will be asleep for {character.StatusTime} moves");
                },
                OnBeforeMove = (Character character) =>
                {
                    if (character.StatusTime <= 0)
                    {
                        character.CureStatus();
                        character.StatusChanges.Enqueue($"{character.Base.Name} woke up!");
                        return true;
                    }

                    character.StatusTime--;
                    character.StatusChanges.Enqueue($"{character.Base.Name} is sleeping");
                    return false;
                }
            }
        },

        // Volatile Status Conditions
        {
            ConditionID.confusion,
            new Condition()
            {
                Name = "Confusion",
                StartMessage = "has been confused",
                OnStart = (Character character) =>
                {
                    // Confused for 1 - 4 turns
                    character.VolatileStatusTime = Random.Range(1, 5);
                    Debug.Log($"Will be confused for {character.VolatileStatusTime} moves");
                },
                OnBeforeMove = (Character character) =>
                {
                    if (character.VolatileStatusTime <= 0)
                    {
                        character.CureVolatileStatus();
                        character.StatusChanges.Enqueue($"{character.Base.Name} kicked out of confusion!");
                        return true;
                    }
                    character.VolatileStatusTime--;

                    // 50% chance to do a move
                    if (Random.Range(1, 3) == 1)
                        return true;

                    // Hurt by confusion
                    character.StatusChanges.Enqueue($"{character.Base.Name} is confused");
                    character.UpdateHP(character.MaxHp / 8);
                    character.StatusChanges.Enqueue($"It hurt itself due to confusion");
                    return false;
                }
            }
        }
    };
}

public enum ConditionID
{
    none, psn, brn, slp, par, frz,
    confusion
}
