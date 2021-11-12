using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateDie : AIStateBase
{
    public AIStateBase oldState;
    public override void StartState(Enemy myEnemy)
    {
        myEnemy.myTrigger.myMR.enabled = false;
        myEnemy.Mother.BackKids();
    }

    public override void Update(Enemy myEnemy)
    {

        myEnemy.hittedStates.text = "DEAD";

    }

    public override void LeaveState(Enemy myEnemy)
    {

    }
}
