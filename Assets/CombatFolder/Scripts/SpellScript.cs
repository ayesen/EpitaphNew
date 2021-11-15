using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{
	public GameObject mat;
	public ParticleSystem burst;
	public ParticleSystem fragments;
	public List<EffectStructNew> myEffects;
	public int hit_amount;
	public float hit_interval;

	private void Update()
	{
		Destroy(gameObject, 3);
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (!collision.gameObject.CompareTag("Player"))
		{
			StartCoroutine(Detection(hit_amount, collision, collision.GetContact(0).point));
			GetComponent<BoxCollider>().enabled = false;
			GetComponent<MeshRenderer>().enabled = false;
		}
		else if (collision.gameObject.CompareTag("InteractableObject"))
		{
			if (collision.gameObject.GetComponent<InteractableObjectScript>().reactionMat == mat)
			{
				collision.gameObject.SendMessage("Reaction");
			}
		}
	}

	IEnumerator Detection(int hitAmount, Collision hit, Vector3 hitPos)
	{
		int amount = hitAmount;
		while (amount > 0)
		{
			if (hit.gameObject.CompareTag("Enemy")) // if hit enemy, inflict effects on enemy and spawn fragments vfx
			{
				// apply effects
				foreach (var effect in myEffects)
				{
					if (effect.toWhom == EffectStructNew.Target.enemy)
					{
						hit.gameObject.GetComponent<Enemy>().effectsInflictedOnMe.Add(effect);
					}
				}
				// vfx
				if (fragments != null)
				{
					ParticleSystem f = Instantiate(fragments);
					f.transform.position = hitPos;
				}
			}
			if (burst != null) // if hit, spawn burst vfx
			{
				// vfx
				ParticleSystem b = Instantiate(burst);
				b.transform.position = hitPos;
			}
			amount--;
			yield return new WaitForSeconds(hit_interval);
		}
	}
}
