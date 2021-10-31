using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateAttacking : AIStateBase
{
    public float AtkTimer;
    public override void StartState(Enemy myEnemy)
    {
        myEnemy.myTrigger.myMR.enabled = true;

        if (myEnemy.attackable)
        { 
            if (myEnemy.phase == Enemy.AIPhase.InBattle1)
            {
                Debug.Log("knowck");
                myEnemy.KnowckBackAtk();
            }
            else if (myEnemy.phase == Enemy.AIPhase.InBattle2)
            {
                myEnemy.SoundWaveAtk();
            }
         }
    }

    public override void Update(Enemy myEnemy)
    {
        if (myEnemy.attackable)
        {
            AtkTimer += Time.fixedDeltaTime;//change to after animation is over


            if (AtkTimer > myEnemy.atkTime)
            {
                myEnemy.myAC.ChangeState(myEnemy.myAC.postAttackState);
            }
        }
        else if (!myEnemy.attackable)
        {
            myEnemy.myAC.ChangeState(myEnemy.myAC.idleState);
        }


    }

    public override void LeaveState(Enemy myEnemy)
    {
        AtkTimer = 0;
    }

}
