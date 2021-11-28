using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScriptNew : MonoBehaviour
{
	public static PlayerScriptNew me;
	[Header("Basics")]
	public int hp;
	public float spd;
	public float rot_spd;
	private GameObject enemy;
    private Animator anim;
    public GameObject actualPlayer;
	[Header("Mat")]
	public List<GameObject> selectedMats; // mats activated
	public List<GameObject> matSlots; // inventory
	[Header("Temp UI")]
	public TextMeshProUGUI mat1;
	public TextMeshProUGUI mat2;
	public TextMeshProUGUI mat3;
	public TextMeshProUGUI mat4;

	// backswing cancel
	private GameObject lastMat;

	private void Awake()
	{
		me = this;
	}

	private void Start()
    {
        anim = actualPlayer.GetComponent<Animator>();
		enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			GetComponentInChildren<Rigidbody>().AddForce(new Vector3(1,0,1) * 50, ForceMode.Impulse);
		}
		// check for attack button press
		if (selectedMats.Count > 0 &&  // check if player has mat activated
			(anim.GetCurrentAnimatorStateInfo(0).IsName("testIdle") || // if player in idle state
			anim.GetCurrentAnimatorStateInfo(0).IsName("testWalk")))  // if player in walk state
		{
			if (Input.GetMouseButtonUp(0)) // if left click
			{
				bool goodToGo = true;
				foreach (var mat in selectedMats)
				{
					if (mat.GetComponent<MatScriptNew>().amount <= 0)
					{
						goodToGo = false;
					}
				}
				if (goodToGo)
				{
					foreach (var mat in selectedMats)
					{
						mat.GetComponent<MatScriptNew>().amount--;
					}
					anim.Play("testWindup"); // player anticipation clip and call effect manager's casting event in clip
				}
				else
				{
					print("YOU DON'T HAVE ENOUGH MATERIALS!!!");
				}
			}
		}
		#region Temp UI
		if (selectedMats.Contains(matSlots[0]))
		{
			mat1.text = matSlots[0].name + ": " + matSlots[0].GetComponent<MatScriptNew>().amount;
		}
		else
		{
			mat1.text = "mat 1 not selected";
		}
		if (selectedMats.Contains(matSlots[1]))
		{
			mat2.text = matSlots[1].name + ": " + matSlots[1].GetComponent<MatScriptNew>().amount;
		}
		else
		{
			mat2.text = "mat 2 not selected";
		}
		if (selectedMats.Contains(matSlots[2]))
		{
			mat3.text = matSlots[2].name + ": " + matSlots[2].GetComponent<MatScriptNew>().amount;
		}
		else
		{
			mat3.text = "mat 3 not selected";
		}
		if (selectedMats.Contains(matSlots[3]))
		{
			mat4.text = matSlots[3].name + ": " + matSlots[3].GetComponent<MatScriptNew>().amount;
		}
		else
		{
			mat4.text = "mat 4 not selected";
		}
		#endregion
		#region activate and deactivate mats
		// activate mats
		if (Input.GetKeyUp(KeyCode.Alpha1))
		{
			if (selectedMats.Contains(matSlots[0]))
			{
				selectedMats.Remove(matSlots[0]);
			}
			else
			{
				selectedMats.Add(matSlots[0]);
			}
			EffectManagerNew.me.RefreshCurrentMats();
		}
		else if (Input.GetKeyUp(KeyCode.Alpha2))
		{
			if (selectedMats.Contains(matSlots[1]))
			{
				selectedMats.Remove(matSlots[1]);
			}
			else
			{
				selectedMats.Add(matSlots[1]);
			}
			EffectManagerNew.me.RefreshCurrentMats();
		}
		else if (Input.GetKeyUp(KeyCode.Alpha3))
		{
			if (selectedMats.Contains(matSlots[2]))
			{
				selectedMats.Remove(matSlots[2]);
			}
			else
			{
				selectedMats.Add(matSlots[2]);
			}
			EffectManagerNew.me.RefreshCurrentMats();
		}
		else if (Input.GetKeyUp(KeyCode.Alpha4))
		{
			if (selectedMats.Contains(matSlots[3]))
			{
				selectedMats.Remove(matSlots[3]);
			}
			else
			{
				selectedMats.Add(matSlots[3]);
			}
			EffectManagerNew.me.RefreshCurrentMats();
		}
		#endregion
		#region movement
		// simple movement for now
		if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) &&
			anim.GetCurrentAnimatorStateInfo(0).IsName("testIdle")) // if in idel state and a movement key pressed, go into walk state
		{
			anim.Play("testWalk");
		}
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("testWalk")) // if in walk state, walk
		{
			// walking diagonally
			if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
			{
				transform.position = new Vector3(transform.position.x - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
			}
			else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
			{
				transform.position = new Vector3(transform.position.x + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
			}
			else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
			{
				transform.position = new Vector3(transform.position.x - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
			}
			else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
			{
				transform.position = new Vector3(transform.position.x + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
			}
			// walking in one axis
			else if (Input.GetKey(KeyCode.W))
			{
				transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + spd * Time.deltaTime);
			}
			else if (Input.GetKey(KeyCode.S))
			{
				transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - spd * Time.deltaTime);
			}
			else if (Input.GetKey(KeyCode.A))
			{
				transform.position = new Vector3(transform.position.x - spd * Time.deltaTime, transform.position.y, transform.position.z);
			}
			else if (Input.GetKey(KeyCode.D))
			{
				transform.position = new Vector3(transform.position.x + spd * Time.deltaTime, transform.position.y, transform.position.z);
			}
			else
			{
				anim.Play("testIdle");
			}
		}

		// look at mouse pos(not changing y-axis)
		//! if this doesn't work properly, check game objects' layers, and make sure the mouse manager ignores the proper layers
		if (Input.GetMouseButton(1))
		{
			var target = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - transform.position), rot_spd * Time.deltaTime);
		}
		else
		{
			var target = new Vector3(MouseManager.me.mousePos.x, transform.position.y, MouseManager.me.mousePos.z);
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - transform.position), rot_spd * Time.deltaTime);
		}
		#endregion
	}
}
