using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateChangePhase : AIStateBase
{
    public float changePhaseTimer;
    public override void StartState(Enemy myEnemy)
    {

        if (myEnemy.phase == Enemy.AIPhase.InBattle2)
        {
            myEnemy.changeLimit -= 1;
        }

    }

    public override void Update(Enemy myEnemy)
    {
        changePhaseTimer += Time.fixedDeltaTime;
        if (changePhaseTimer > myEnemy.changePhaseTime)
        {
            myEnemy.myAC.ChangeState(myEnemy.myAC.idleState);
        }


    }

    public override void LeaveState(Enemy myEnemy)
    {
        if (myEnemy.phase == Enemy.AIPhase.InBattle2)
        {
            myEnemy.Mother.OutKids();
        }
        else if (myEnemy.phase == Enemy.AIPhase.InBattle1)
        {
            myEnemy.Mother.BackKids();
        }
    }
}
