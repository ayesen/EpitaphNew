using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Enemy : MonoBehaviour
{
    [Header("BASIC")]
    public int health;
    public int maxHealth;
    public int moveSpeed;
    public int shield;
    public int maxShield;
    public int atkSpd;
    public int attack;
    public int preAtkSpd;
    public int atkTime;
    public int postAtkSpd;
    public int changePhaseTime;
    public int healthLimit;
    public int changeLimit = 2;
    public float knockbackAmount;

    public AIController myAC;
    public enum AIPhase { NotInBattle, InBattle1, InBattle2 };
    public AIPhase phase;

    [Header("NAV MESH")]
    public NavMeshAgent ghostRider;
    public GameObject target;

    [Header("ATTACK")]
    public AtkTrigger myTrigger;
    public GameObject myTriggerObj;
    public Color Origin = new Color(1, 0.5f, 0.5f, 0.3f);
    public Color TempAtkColor = new Color(1, 0, 0, 0.3f);
    public MotherController Mother;

    [Header("HITTED CTRL")]
    public bool attackable;
    public bool walkable;
    public TextMeshProUGUI hittedStates;

    [Header("Supply")]
    public float dropMeter;
    public float dropMeterMax;

    [Header("SCRIPTED EVENTS")]
    public Transform eventTarget;
    public float stopDis;
    public GameObject dialogueTrigger;

    [Header("Effect Process")]
    public List<EffectStructNew> effectsInflictedOnMe;

    private void Awake()
    {
        ghostRider = GetComponent<NavMeshAgent>();
        myAC = GetComponent<AIController>();
        health = maxHealth;
        PhaseSetting();
        Mother = GetComponent<MotherController>();
    }

    private void Update()
    {
        HittedStatesIndication();
        AIDead();
        PhaseSetting();
    }

    public void ChangePhase(AIPhase phaseName, int time)
    {
        myTrigger.myMR.enabled = false;
        phase = phaseName;
        changePhaseTime = time;
        myAC.ChangeState(myAC.changePhaseState);
    }
    public void PhaseSetting()
    {
        if (phase == AIPhase.InBattle1)
        {
            atkSpd = 2;
            preAtkSpd = 2;
            atkTime = 1;
            postAtkSpd = 2;
            myTriggerObj = GameObject.Find("Atk1Trigger");
            if (shield <= 0)
            {
                ChangePhase(AIPhase.InBattle2, 10);
            }
        }
        else if (phase == AIPhase.InBattle2)
        {
            atkSpd = 5;
            preAtkSpd = 7;
            atkTime = 1;
            postAtkSpd = 3;
            myTriggerObj = GameObject.Find("Atk2Trigger");
            if (health < healthLimit && changeLimit > 0)
            {
                shield = maxShield;
                ChangePhase(AIPhase.InBattle1, 10);
            }
        }
        myTrigger = myTriggerObj.GetComponent<AtkTrigger>();
    }

    public void AIDead()
    {
        if (health <= 0)
        {
            myAC.ChangeState(myAC.dieState);
        }
    }

    public void LoseHealth(int hurtAmt)
    {
        if (shield <= 0)
        {
            if (health - hurtAmt >= 0)
            {
                health -= hurtAmt;
            }
            else
            {
                health = 0;
            }
        }
        else
            shield -= hurtAmt;
    }

    public void ChangeSpd(int ChangeAmt)
    {
        moveSpeed += ChangeAmt; // get navmesh move spd
    }

    public void ChaseTarget()
    {
        ghostRider.SetDestination(target.transform.position);
    }

    public void Idleing()
    {
        if (InRange())
        {
            myTrigger.myMR.enabled = true;
            myTrigger.myMR.material.color = Origin;
        }
        if (!InRange())
        {
            myTrigger.myMR.enabled = false;
        }

    }
    public void HittedStatesIndication()
    {
        if (myAC.currentState != myAC.dieState)
        {
            if (walkable && attackable == false)
            {
                hittedStates.text = "cant attack";
            }
            else if (attackable && walkable == false)
            {
                hittedStates.text = "cant walk";
            }
            else if (!walkable && !attackable)
            {
                hittedStates.text = "cant anything";
            }

            else if (walkable && attackable)
            {
                if (hittedStates != null)
				{
                    hittedStates.text = "";
                }
            }
        }
    }
    public void TempPre(float time)
    {
        myTrigger.myMR.material.color = Color.Lerp(Origin, TempAtkColor, time);
    }

    public void TempPost(float time)
    {
        myTrigger.myMR.material.color = Color.Lerp(TempAtkColor, Origin, time);
    }
    public void KnockBackAtk()
    {
        myTrigger.myMR.material.color = new Color(1, 1, 1, 1);

        if (InRange())
        {
            EffectManager.me.KnockBack(knockbackAmount, gameObject, PlayerScript.me.gameObject);
            /*deal damage here*/
        }

    }

    public void SoundWaveAtk()
    {
        myTrigger.myMR.material.color = new Color(0, 0.5f, 1, 1);
        float dmgRange = 8;
        float soundWaveDmg = 8 - dmgRange; //can change later

        if (AIToPlayerDist() <= dmgRange)
        {
            Debug.Log("player in dmg range");
            /*deal damage here*/
            /*apply DOT to player here*/
        }

    }

    public float AIToPlayerDist()
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    public bool InRange()
    {
        if (myTrigger.onAtkTrigger)
        {
            return true;
        }
        else
            return false;
    }

    public void BackToFighting()
	{
        target = GameObject.FindGameObjectWithTag("Player");
        phase = AIPhase.InBattle1;
	}

    public void GotoLoc()
    {
        // go to specific location and stand still for dialogue
        myAC.ChangeState(myAC.walkingState);
        target = eventTarget.gameObject;
    }
}
