using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateIdle : AIStateBase
{
    public float IdleTimer;
    private Enemy es;

    public override void StartState(Enemy myEnemy)
    {
        es = myEnemy;
        myEnemy.ghostRider.isStopped = true;
    }

    public override void Update(Enemy myEnemy)
    {
        if (myEnemy.phase != Enemy.AIPhase.NotInBattle) // in battle phase 1 or 2
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
		else // not in battle
		{
            GotoLoc();
        }
    }

    public override void LeaveState(Enemy myEnemy)
    {
        IdleTimer = 0;
    }

    public void GotoLoc()
	{
        if (Input.GetKeyUp(KeyCode.T))
		{
            // go to specific location and stand still for dialogue
            es.myAC.ChangeState(es.myAC.walkingState);
            es.target = es.eventTarget.gameObject;
		}
	}
}
