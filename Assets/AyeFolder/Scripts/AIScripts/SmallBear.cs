using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBear : Enemy
{

    private void Awake()
    {
        myTrigger = myTriggerObj.GetComponent<AtkTrigger>();
        myAC = GetComponent<AIController>();
        health = maxHealth;
        ghostRider = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    void Start()
    {
        
    }


    void Update()
    {
        Debug.Log(myAC.currentState);
    }
    

}
