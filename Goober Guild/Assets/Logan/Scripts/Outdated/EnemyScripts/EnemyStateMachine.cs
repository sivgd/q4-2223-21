using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{

    private BattleStateMachine BSM;
    public BaseEnemy enemy;

    public enum TurnState
    {
        PROCESSING,
        CHOOSEACTION,
        WAITING,
        ACTION,
        DEAD
    }

    public TurnState currentState;
    //for the progress bar
    private float Cur_Cooldown = 0f;
    private float Max_Cooldown = 5f;
    //this gameObject
    private Vector3 startPosition;
    //TimeForAction stuff
    private bool actionStarted = false;
    public GameObject HeroToAttack;
    private float animSpeed = 5f;

    void Start()
    {
        currentState = TurnState.PROCESSING;
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        startPosition = transform.position;
    }

    void Update()
    {
        switch (currentState)
        {
            case (TurnState.PROCESSING):

                UpgradeProgressBar();
                break;

            case (TurnState.CHOOSEACTION):

                ChooseAction();
                currentState = TurnState.WAITING;
                break;

            case (TurnState.WAITING):

                //idle state
                break;

            case (TurnState.ACTION):

                StartCoroutine(TimeForAction());
                break;

            case (TurnState.DEAD):

                break;
        }
    }

    void UpgradeProgressBar()
    {
        Cur_Cooldown = Cur_Cooldown + Time.deltaTime;
        if (Cur_Cooldown >= Max_Cooldown)
        {
            currentState = TurnState.CHOOSEACTION;
        }
    }

    void ChooseAction()
    {
        HandleTurn myAttack = new HandleTurn();
        myAttack.Attacker = enemy.name;
        myAttack.Type = "Enemy";
        myAttack.AttackersGameObject = this.gameObject;
        myAttack.AttackersTarget = BSM.HeroesInBattle[Random.Range(0, BSM.HeroesInBattle.Count)];
        BSM.CollectActions(myAttack);
    }

    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;

        //animate the enemy near the hero to attack
        Vector3 heroPosition = new Vector3(HeroToAttack.transform.position.x - 1.5f, HeroToAttack.transform.position.y, HeroToAttack.transform.position.z);
        while(MoveTowardsEnemy(heroPosition)) 
        {
            yield return null;
        }

        //wait a bit
        yield return new WaitForSeconds(0.5f);
        //do damage

        //animate back to startposition
        Vector3 firstPosition = startPosition;
        while (MoveTowardsStart(firstPosition))
        {
            yield return null;
        }

        //remove this performer from the list in BSM
        BSM.PerformList.RemoveAt(0);

        //reset BSM -> Wait
        BSM.battleStates = BattleStateMachine.PerformAction.WAIT;
        actionStarted = false;

        //reset the enemy state
        Cur_Cooldown = 0f;
        currentState = TurnState.PROCESSING;
    }

    private bool MoveTowardsEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }   
    private bool MoveTowardsStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }
}
