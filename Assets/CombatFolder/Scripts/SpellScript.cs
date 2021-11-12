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
			if (burst != null)
			{
				ParticleSystem b = Instantiate(burst);
				b.transform.position = collision.GetContact(0).point;
			}
			if (fragments != null)
			{
				ParticleSystem f = Instantiate(fragments);
				f.transform.position = collision.GetContact(0).point;
			}
		}
		else if (collision.gameObject.CompareTag("InteractableObject"))
		{
			if (collision.gameObject.GetComponent<InteractableObjectScript>().reactionMat == mat)
			{
				collision.gameObject.SendMessage("Reaction");
			}
		}
		if (!collision.gameObject.CompareTag("Player"))
		{
			Destroy(gameObject);
			if (burst != null)
			{
				ParticleSystem b = Instantiate(burst);
				b.transform.position = collision.GetContact(0).point;
			}
		}
	}
}
