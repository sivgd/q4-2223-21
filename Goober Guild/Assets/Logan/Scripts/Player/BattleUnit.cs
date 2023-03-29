using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] CharacterBase Base;
    [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;

    public Character character { get; set; }

    public void Setup()
    {
        character = new Character(Base, level);
        if (isPlayerUnit)
            GetComponent<Image>().sprite = character.Base.BackSprite;
        else
            GetComponent<Image>().sprite = character.Base.FrontSprite;
    }
}
