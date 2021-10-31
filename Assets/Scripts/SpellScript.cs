using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{
	public GameObject mat;
	private void Update()
	{
		Destroy(gameObject, 3);
	}
	private void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.CompareTag("Enemy"))
		{
			//! effect here
			EffectManager.me.ProcessEffects(mat, collision.gameObject);
			//print("hit enemy with a projectile");
		}
		if (!collision.gameObject.CompareTag("Player"))
		{
			Destroy(gameObject);
		}
	}
}
