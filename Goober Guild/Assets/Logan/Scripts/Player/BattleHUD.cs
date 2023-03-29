using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPbar hpBar;
    
    public void SetData(Character character)
    {
        nameText.text = character.Base.Name;
        levelText.text = "Lvl " + character.Level;
        hpBar.SetHP((float)character.HP / character.MaxHp);
    }
}
