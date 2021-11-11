using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHider : MonoBehaviour
{
    public List<GameObject> wallsToHide;
	public List<GameObject> masksToShow;
	public List<GameObject> wallsToShow;
	public List<GameObject> masksToHide;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("RealPlayer"))
		{
			if (wallsToHide.Count > 0)
				foreach (var wall in wallsToHide)
				{
					wall.SetActive(false);
				}
			if (masksToShow.Count > 0)
				foreach (var mask in masksToShow)
				{
					mask.SetActive(true);
				}
			if (wallsToShow.Count > 0)
				foreach (var wall in wallsToShow)
				{
					wall.SetActive(true);
				}
			if (masksToHide.Count > 0)
				foreach (var mask in masksToHide)
				{
					mask.SetActive(false);
				}
		}
	}
}
