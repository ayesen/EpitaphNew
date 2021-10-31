using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Enemy myEnemy;

    public AIStateBase currentState;

    public AIStateBase idleState = new AIStateIdle();
    public AIStateBase walkingState = new AIStateWalking();
    public AIStateBase preAttackState = new AIStatePreAttack();
    public AIStateBase attackState = new AIStateAttacking();
    public AIStateBase postAttackState = new AIStatePostAttack();
    public AIStateBase dieState = new AIStateDie();


    public void ChangeState(AIStateBase newState)
    {
        if(currentState != null)
        {
            currentState.LeaveState(myEnemy);
        }

        currentState = newState;

        if(currentState != null)
        {
            currentState.StartState(myEnemy);
        }

    }


    private void Awake()
    {
        currentState = idleState;
        myEnemy = GetComponent<Enemy>();
    }

    void Start()
    {
        ChangeState(walkingState);
    }

    void Update()
    {
        currentState.Update(myEnemy);
    }
}
