using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectScript : MonoBehaviour
{
	public GameObject reactionMat;
	public List<GameObject> objectsToShow;
	public List<GameObject> objectsToHide;
	
	public string methodToCall;
	public GameObject methodsOwner;

	public void Reaction()
	{
		if (objectsToShow.Count > 0) // show things
		{
			foreach (var thing in objectsToShow)
			{
				thing.SetActive(true);
			}
		}
		if (objectsToHide.Count > 0) // hide things
		{
			foreach (var thing in objectsToHide)
			{
				thing.SetActive(false);
			}
		}
		methodsOwner.SendMessage(methodToCall);
	}
}
