using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : MonoBehaviour
{

    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION
    }

    public PerformAction battleStates;

    // Start is called before the first frame update
    void Start()
    {
        battleStates = PerformAction.WAIT;
    }

    // Update is called once per frame
    void Update()
    {
        switch (battleStates)
        {
            case (PerformAction.WAIT):

                break;

            case (PerformAction.TAKEACTION):

                break;

            case (PerformAction.PERFORMACTION):

                break;
        }
    }
}
