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
	public GameObject canvasUI;

	private int index = 0;
	public List<string> textToShow;

	private void Awake()
	{
		me = this;
	}

	public void ShowText(List<string> content)
	{
		canvasUI.SetActive(false);
		objectDes_ui.gameObject.SetActive(true);
		if (textBg != null)
		{
			textBg.SetActive(true);
		}
		if (pressE != null)
		{
			pressE.SetActive(true);
		}
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
					canvasUI.SetActive(true);
					objectDes_ui.gameObject.SetActive(false);
					if (textBg != null)
						textBg.SetActive(false);
					if (pressE != null)
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
