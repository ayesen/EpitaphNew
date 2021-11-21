using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script controls a gameobject for holding an effect, it is stored in an effect holders holder, which is attached to a game object that can be attacked
public class EffectHolderScript : MonoBehaviour
{
	public GameObject myOwner;
    public EffectStructNew myEffect;
	public bool destroy;
	private float timer;

	private void Start()
	{
		timer = myEffect.forHowLong;
	}

	private void Update()
	{
		if (myEffect.forHowLong > 0)
		{
			if (timer > 0)
			{
				timer -= Time.deltaTime;
			}
			else
			{
				myOwner.GetComponent<EffectHoldersHolderScript>().effectHolders.Remove(gameObject);
				Destroy(gameObject);
			}
		}
		else
		{
			if (destroy)
			{
				myOwner.GetComponent<EffectHoldersHolderScript>().effectHolders.Remove(gameObject);
				Destroy(gameObject);
			}
		}
	}
}
