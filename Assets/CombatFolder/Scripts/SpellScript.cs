using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{
	public GameObject mat;
	public ParticleSystem burst;
	public ParticleSystem fragments;

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
			ParticleSystem b = Instantiate(burst);
			ParticleSystem f = Instantiate(fragments);
			b.transform.position = collision.GetContact(0).point;
			f.transform.position = collision.GetContact(0).point;
		}
		if (!collision.gameObject.CompareTag("Player"))
		{
			Destroy(gameObject);
			ParticleSystem b = Instantiate(burst);
			ParticleSystem f = Instantiate(fragments);
			b.transform.position = collision.GetContact(0).point;
			f.transform.position = collision.GetContact(0).point;

			f.transform.rotation = PlayerScript.me.transform.rotation;
		}
	}
}
