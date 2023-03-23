using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStateMachine : MonoBehaviour
{
    public BaseHero hero;

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
    public Image ProgressBar;



    void Start()
    {
        currentState = TurnState.PROCESSING;
    }

    void Update()
    {
        //Debug.Log(currentState);
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
        float Calc_Cooldown = Cur_Cooldown / Max_Cooldown;
        ProgressBar.transform.localScale = new Vector3(Mathf.Clamp(Calc_Cooldown, 0, 1), ProgressBar.transform.localScale.y, ProgressBar.transform.localScale.z);
        if (Cur_Cooldown >= Max_Cooldown)
        {
            currentState = TurnState.ADDTOLIST;
        }
    }
}
