using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] Text statusText;
    [SerializeField] HPbar hpBar;

    [SerializeField] Color psnColor;
    [SerializeField] Color brnColor;
    [SerializeField] Color slpColor;
    [SerializeField] Color parColor;
    [SerializeField] Color frzColor;

    Character _character;
    Dictionary<ConditionID, Color> statusColors;

    public void SetData(Character character)
    {
        _character = character;

        nameText.text = character.Base.Name;
        levelText.text = "Lvl " + character.Level;
        hpBar.SetHP((float)character.HP / character.MaxHp);

        statusColors = new Dictionary<ConditionID, Color>()
        {
            {ConditionID.psn, psnColor },
            {ConditionID.brn, brnColor },
            {ConditionID.slp, slpColor },
            {ConditionID.par, parColor },
            {ConditionID.frz, frzColor },
        };

        SetStatusText();
        _character.OnStatusChanged += SetStatusText;
    }

    void SetStatusText()
    {
        if (_character.Status == null)
        {
            statusText.text = "";
        }
        else
        {
            statusText.text = _character.Status.Id.ToString().ToUpper();
            statusText.color = statusColors[_character.Status.Id];
        }
    }

    public IEnumerator UpdateHP()
    {
        if (_character.HpChanged)
        {
            yield return hpBar.SetHPSmooth((float)_character.HP / _character.MaxHp);
            _character.HpChanged = false;
        }
    }
}
