using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAOEScript : MonoBehaviour
{
	public List<GameObject> targetsInAoe;
	public GameObject mat;
	public ParticleSystem aoe_burst;
	public ParticleSystem.ShapeModule sm;

	private void Start()
	{
		
		Vector3 pos = new Vector3(transform.position.x, 1, transform.position.z);
		float targetRadius = transform.localScale.x / 1.5f * 2f;
		ParticleSystem aoeB = Instantiate(aoe_burst, pos, Quaternion.identity);
		sm = aoeB.shape;
		sm.radius = targetRadius;
	}

	void Update()
	{
		// destroy it after 3 seconds
		Destroy(gameObject, 0.5f);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("RealPlayer"))
		{
			// !effect here
			EffectManager.me.ProcessEffects(mat, other.gameObject);
			//print("enemy hit by aoe once");
		}
		else if (other.gameObject.CompareTag("InteractableObject"))
		{
			if (other.gameObject.GetComponent<InteractableObjectScript>().reactionMat == mat)
			{
				other.gameObject.SendMessage("Reaction");
			}
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
