using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAOEScript : MonoBehaviour
{
	public List<GameObject> targetsInAoe;
	public GameObject mat;

	void Update()
	{
		// destroy it after 3 seconds
		Destroy(gameObject, 0.5f);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
		{
			// !effect here
			EffectManager.me.ProcessEffects(mat, other.gameObject);
			//print("enemy hit by aoe once");
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			// !effect here
			//print("enemy in aoe");
		}
	}
}
