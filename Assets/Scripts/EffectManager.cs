using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EffectManager : MonoBehaviour
{
	public static EffectManager me;
	public float droppedMat_flyAmount;
	public GameObject enemy;
	public Enemy enemyScript;

	public enum CtrlTypes
	{
		none,
		cantWalk,
		cantAttack,
		forceMove
	};

	private void Awake()
	{
		me = this;
	}

    private void Start()
    {
		enemyScript = enemy.GetComponent<Enemy>();
    }

    public void ProcessEffects(GameObject mat, GameObject target)
	{
		foreach (var effect in mat.GetComponent<MatScript>().myEffects)
		{
			DoDamage(target, effect);
			DoCtrl(target, effect);
			Heal(target, effect);
			DropMat(target, effect);
		}
	}

	public void DoDamage(GameObject target, EffectStruct effect)
	{
		if (target.tag == "Enemy" && effect.damageAmount > 0)
		{
			enemyScript.LoseHealth(effect.damageAmount);
			if (effect.freezeFrame)
			{
				StartCoroutine(FreezeFrame());
			}
			else
			{
				print(effect.freezeFrame);
			}
		}

        if (target.tag == "Enemy" && effect.DOT)
        {
			StartCoroutine(DoDOT(effect, target));
		}
	}

	public void DoCtrl(GameObject target, EffectStruct effect)
	{
		if (effect.myCtrlType != CtrlTypes.none)
		{
			if (effect.myCtrlType == CtrlTypes.forceMove)
			{
				// knock back based on amount
				KnockBack(effect.knockback_amount, PlayerScript.me.gameObject, target);
			}
			if (effect.myCtrlType == CtrlTypes.cantAttack)
			{
				if (target.GetComponent<Enemy>() != null)
				{
					target.GetComponent<Enemy>().attackable = false;
					StartCoroutine(ResetAttackability(effect.ctrl_duration, target));
				}
			}
			if (effect.myCtrlType == CtrlTypes.cantWalk)
			{
				if (target.GetComponent<Enemy>() != null)
				{
					target.GetComponent<Enemy>().walkable = false;
					StartCoroutine(ResetMoveability(effect.ctrl_duration, target));
				}
			}
		}
	}

	public void DropMat(GameObject target, EffectStruct effect)
	{
		if (target.GetComponent<Enemy>() != null &&
			effect.matProduce.Count>0)
		{

			Enemy eS = target.GetComponent<Enemy>();
			eS.dropMeter += effect.dropMatAmount;
			if (eS.dropMeter >= eS.dropMeterMax)
			{
				eS.dropMeter = 0;
				SpawnMat(target, effect);
			}
		}
	}

	public void Heal(GameObject target, EffectStruct effect)
	{
		//print("I am debugging healing");
		if (target.GetComponent<PlayerScript>() != null && effect.healAmount > 0)
		{
			print("healed " + target.name + " " + effect.healAmount);
			target.GetComponent<PlayerScript>().hp += effect.healAmount;
		}

        if (target.GetComponent<PlayerScript>() != null && effect.HOT)
        {
			StartCoroutine(DoHOT(effect, target));
        }
	}

	public void KnockBack(float amount, GameObject er, GameObject ee)
	{
		if (ee.GetComponent<NavMeshAgent>())
		{
			ee.GetComponent<NavMeshAgent>().enabled = false;
			ee.GetComponent<Rigidbody>().isKinematic = false;
		}
		Vector3 dir = ee.transform.position - er.transform.position;
		ee.GetComponent<Rigidbody>().AddForce(dir.normalized * amount, ForceMode.Impulse);
	}

	IEnumerator ResetAttackability(float duration, GameObject target)
	{
		float timer = 0f;
		while (timer < duration)
		{
			//print(timer);
			timer += Time.deltaTime;
			yield return null;
		}
		if (target.GetComponent<Enemy>() != null &&
			timer >= duration)
		{
			target.GetComponent<Enemy>().attackable = true;
		}
	}

	IEnumerator ResetMoveability(float duration, GameObject target)
	{
		float timer = 0f;
		while (timer < duration)
		{
			timer += Time.deltaTime;
			yield return null;
		}
		if (target.GetComponent<Enemy>() != null &&
			timer >= duration)
		{
			target.GetComponent<Enemy>().walkable = true;
		}
	}

	IEnumerator DoDOT(EffectStruct effect, GameObject target)
    {
		int timer = 1;
		yield return new WaitForSeconds(1f);
		while(timer <= effect.DOT_duration)
        {
			timer += 1;
			print("dealt " + effect.DOT_interval + " DOT damage to " + target.name);
			enemyScript.LoseHealth((int)effect.DOT_interval);
			yield return new WaitForSeconds(1f);
		}
		if(timer > effect.DOT_duration)
        {
			StopCoroutine(DoDOT(effect, target));
        }
    }

	IEnumerator DoHOT(EffectStruct effect, GameObject target)
	{
		int timer = 1;
		yield return new WaitForSeconds(1f);
		while (timer <= effect.HOT_duration)
		{
			timer += 1;
			print("healed " + effect.HOT_interval + " HOT HP to " + target.name);
			target.GetComponent<PlayerScript>().hp += effect.HOT_interval;
			yield return new WaitForSeconds(1f);
		}
		if (timer > effect.HOT_duration)
		{
			StopCoroutine(DoHOT(effect, target));
		}
	}

	private void SpawnMat(GameObject target, EffectStruct effect)
	{
		GameObject matDropped = effect.matProduce[Random.Range(0, effect.matProduce.Count)];
		Vector3 spawnPos = new Vector3(target.transform.position.x, target.transform.position.y + 0.7f, target.transform.position.z);
		GameObject droppedMat = Instantiate(matDropped, spawnPos, Random.rotation);
		droppedMat.GetComponent<Rigidbody>().AddForce(
			new Vector3(Random.Range(-droppedMat_flyAmount, droppedMat_flyAmount),
			3,
			Random.Range(-droppedMat_flyAmount, droppedMat_flyAmount)),
			ForceMode.Impulse);
	}

	private IEnumerator FreezeFrame()
	{
		Time.timeScale = 0.01f;
		yield return new WaitForSeconds(0.002f);
		Time.timeScale = 1f;
	}
}