using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy}

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHUD playerHud;
    [SerializeField] BattleHUD enemyHud;
    [SerializeField] BattleDialogBox dialogBox;

    public event Action<bool> OnBattleOver;

    BattleState state;
    int currentAction;
    int currentMove;

    public void StartBattle()
    {
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        playerUnit.Setup();
        enemyUnit.Setup();
        playerHud.SetData(playerUnit.character);
        enemyHud.SetData(enemyUnit.character);

        dialogBox.SetMoveNames(playerUnit.character.Moves);

        yield return dialogBox.TypeDialog($"A ravenous {enemyUnit.character.Base.Name} appeared!");

        yield return new WaitForSeconds(1f);

        PlayerAction();
    }

    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choose an action"));
        dialogBox.EnableActionSelector(true);
    }

    void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy;

        var move = playerUnit.character.Moves[currentMove];
        yield return dialogBox.TypeDialog($"{playerUnit.character.Base.Name} used {move.Base.Name}");

        playerUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);

        enemyUnit.PlayHitAnimation();
        var damageDetails = enemyUnit.character.TakeDamage(move, playerUnit.character);
        yield return enemyHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{enemyUnit.character.Base.Name} was defeated!");
            enemyUnit.PlayFaintAnimation();

            yield return new WaitForSeconds(2f);
            OnBattleOver(true);
        }
        else
        {
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;

        var move = enemyUnit.character.GetRandomMove();
        yield return dialogBox.TypeDialog($"{enemyUnit.character.Base.Name} used {move.Base.Name}");

        enemyUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);

        playerUnit.PlayHitAnimation();
        var damageDetails = playerUnit.character.TakeDamage(move, playerUnit.character);
        yield return playerHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{playerUnit.character.Base.Name} was defeated!");
            playerUnit.PlayFaintAnimation();

            yield return new WaitForSeconds(2f);
            OnBattleOver(false);
        }
        else
        {
            PlayerAction();
        }
    }

    IEnumerator ShowDamageDetails(DamageDetails damageDetails)
    {
        if (damageDetails.Critical > 1f)
            yield return dialogBox.TypeDialog("Critcal Strike!");

        if (damageDetails.TypeEffectiveness > 1f)
            yield return dialogBox.TypeDialog("It was a Powerful Strike!");
        else if (damageDetails.TypeEffectiveness < 1f)
            yield return dialogBox.TypeDialog("It was a Weak Strike!");
    }

    public void HandleUpdate()
    {
        if (state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        }
        else if(state == BattleState.PlayerMove)
        {
            HandleMoveSelection();
        }
    }

    void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentAction < 1)
                ++currentAction;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentAction > 0)
                --currentAction;
        }

        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (currentAction == 0)
            {
                // Fight
                PlayerMove();
            }
            else if (currentAction == 1)
            {
                // Run

            }
        }
    }

    void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentMove < playerUnit.character.Moves.Count - 1)
                ++currentMove;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentMove > 0)
                --currentMove;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentMove < playerUnit.character.Moves.Count - 2)
                currentMove += 2;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentMove > 1)
                currentMove -= 2;
        }

        dialogBox.UpdateMoveSelection(currentMove, playerUnit.character.Moves[currentMove]);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PerformPlayerMove());
        }
    }
}
