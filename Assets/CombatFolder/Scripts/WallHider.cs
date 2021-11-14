using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
			print(gameObject.name);
			List<GameObject> masks = GameObject.FindGameObjectsWithTag("Wall Hide Mask").ToList();
			foreach (var mask in masks)
			{
				mask.SetActive(false);
			}
			if (wallsToShow.Count > 0)
				foreach (var wall in wallsToShow)
				{
					wall.SetActive(true);
				}
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
			
			
		}
	}
}
