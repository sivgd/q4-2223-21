using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{

    public BaseEnemy enemy;

    public enum TurnState
    {
        PROCESSING,
        ADDTOLIST,
        WAITING,
        SELECTING,
        ACTION,
        DEAD
    }

    public TurnState currentState;
    //for the progress bar
    private float Cur_Cooldown = 0f;
    private float Max_Cooldown = 5f;

    // Start is called before the first frame update
    void Start()
    {
        currentState = TurnState.PROCESSING;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case (TurnState.PROCESSING):

                UpgradeProgressBar();
                break;

            case (TurnState.ADDTOLIST):

                break;

            case (TurnState.WAITING):

                break;

            case (TurnState.SELECTING):

                break;

            case (TurnState.ACTION):

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
            currentState = TurnState.ADDTOLIST;
        }
    }
}
