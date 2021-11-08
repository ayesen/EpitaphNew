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
            if (myEnemy.target.CompareTag("Player")) // if chasing down player
			{
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
			else // if chasing down event target location
			{
                
                if (Vector3.Distance(myEnemy.gameObject.transform.position, myEnemy.eventTarget.position) < myEnemy.stopDis)
				{
                    Debug.Log("enter dialogue mode");
                    myEnemy.dialogueTrigger.SetActive(true);
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
