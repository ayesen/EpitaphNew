using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectInspectorManagerScript : MonoBehaviour
{
    static public ObjectInspectorManagerScript me;
    public TMP_Text objectDes_ui;
	public GameObject textBg;
	public GameObject pressE;
	private bool textShowing = false;

	private int index = 0;
	public List<string> textToShow;

	private void Awake()
	{
		me = this;
	}

	public void ShowText(List<string> content)
	{
		objectDes_ui.gameObject.SetActive(true);
		textBg.SetActive(true);
		pressE.SetActive(true);
		textToShow = content;
		objectDes_ui.text = content[index];
		PlayerScript.me.GetComponentInChildren<Animator>().Play("readingText");
		StartCoroutine(SetTextShowingToTrue());
	}

	private void Update()
	{
		if (textShowing)
		{
			if (Input.GetKeyDown(KeyCode.E)) // loop through text and hide
			{
				if (index < textToShow.Count - 1)
				{
					index++;
					objectDes_ui.text = textToShow[index];
				}
				else
				{
					index = 0;
					objectDes_ui.gameObject.SetActive(false);
					textBg.SetActive(false);
					pressE.SetActive(false);
					objectDes_ui.text = "";
					textShowing = false;
					PlayerScript.me.GetComponentInChildren<Animator>().Play("testIdle");
				}
			}
		}
	}

	IEnumerator SetTextShowingToTrue()
	{
		yield return new WaitForSeconds(0.1f);
		textShowing = true;
	}
}
