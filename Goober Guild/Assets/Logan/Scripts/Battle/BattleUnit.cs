using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] bool isPlayerUnit;
    [SerializeField] BattleHUD hud;

    public bool IsPlayerUnit
    {
        get { return isPlayerUnit; }
    }

    public BattleHUD hud
    {
        get { return hud; }
    }

    public Character Character { get; set; }

    Image image;
    Vector3 orginalPos;
    Color originalColor;
    private void Awake()
    {
        image = GetComponent<Image>();
        orginalPos = image.transform.localPosition;
        originalColor = image.color;
    }

    public void Setup(Character character)
    {
        Character = character;
        if (isPlayerUnit)
            image.sprite = Character.Base.BackSprite;
        else
            image.sprite = Character.Base.FrontSprite;

        hud.SetData(character);

        image.color = originalColor;
        PlayEnterAnimation();
    }

    public void PlayEnterAnimation()
    {
        if (isPlayerUnit)
            image.transform.localPosition = new Vector3(-700f, orginalPos.y);
        else
            image.transform.localPosition = new Vector3(700f, orginalPos.y);

        image.transform.DOLocalMoveX(orginalPos.x, 1f);
    }

    public void PlayAttackAnimation()
    {
        var sequence = DOTween.Sequence();
        if (isPlayerUnit)
            sequence.Append(image.transform.DOLocalMoveX(orginalPos.x + 50f, 0.25f));
        else
            sequence.Append(image.transform.DOLocalMoveX(orginalPos.x - 50f, 0.25f));

        sequence.Append(image.transform.DOLocalMoveX(orginalPos.x, 0.25f));
    }

    public void PlayHitAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.DOColor(Color.red, 0.1f));
        sequence.Append(image.DOColor(originalColor, 0.1f));
    }

    public void PlayFaintAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.transform.DOLocalMoveY(orginalPos.y - 150f, 0.5f));
        sequence.Join(image.DOFade(0f, 0.5f));
    }
}
