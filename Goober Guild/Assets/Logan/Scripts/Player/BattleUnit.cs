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

    Image image;
    Vector3 originalPos;

    private void Awake()
    {
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
    }

    public void Setup()
    {
        character = new Character(Base, level);
        if (isPlayerUnit)
            image.sprite = character.Base.BackSprite;
        else
            image.sprite = character.Base.FrontSprite;
    }

    public void PlayEnterAnimation()
    {
        if (isPlayerUnit)
            image.transform.localPosition = new Vector3(-500f, originalPos.y);
        else
            image.transform.localPosition = new Vector3(500f, originalPos.y);
    }
}
