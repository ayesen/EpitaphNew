using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateIdle : AIStateBase
{
    public float IdleTimer;
    public override void StartState(Enemy myEnemy)
    {
        myEnemy.ghostRider.isStopped = true;
    }

    public override void Update(Enemy myEnemy)
    {
        if (myEnemy.phase != Enemy.AIPhase.NotInBattle)
        {
            myEnemy.Idleing();
            if (myEnemy.InRange())
            {

                if (myEnemy.attackable)
                {
                    IdleTimer += Time.fixedDeltaTime;
                    if (IdleTimer > myEnemy.atkSpd)
                    {
                        myEnemy.myAC.ChangeState(myEnemy.myAC.preAttackState);
                    }
                }
                else if (!myEnemy.attackable)
                {

                }
            }
            else if (!myEnemy.InRange())
            {
                if (myEnemy.walkable)
                {
                    myEnemy.myAC.ChangeState(myEnemy.myAC.walkingState);
                }
                else if (!myEnemy.walkable)
                {

                }

            }
        }
    }

    public override void LeaveState(Enemy myEnemy)
    {
        IdleTimer = 0;
    }
}
