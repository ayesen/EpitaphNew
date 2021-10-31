using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIStateWalking : AIStateBase
{

    public override void StartState(Enemy myEnemy)
    {
        myEnemy.myTrigger.myMR.enabled = false;
        myEnemy.ghostRider.isStopped = false;
    }

    public override void Update(Enemy myEnemy)
    {

        if (myEnemy.walkable)
        {
            myEnemy.ChaseTarget();
            if (myEnemy.InRange())
            {
                if (myEnemy.attackable)
                {
                    myEnemy.myAC.ChangeState(myEnemy.myAC.preAttackState);
                }
                else if (!myEnemy.attackable)
                {
                    myEnemy.myAC.ChangeState(myEnemy.myAC.idleState);
                }
            }
        }
        else if(!myEnemy.walkable)
        {
            myEnemy.myAC.ChangeState(myEnemy.myAC.idleState);
        }
    }

    public override void LeaveState(Enemy myEnemy)
    {
        myEnemy.ghostRider.isStopped = true;
    }
}
