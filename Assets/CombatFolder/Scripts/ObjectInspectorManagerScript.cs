using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectInspectorManagerScript : MonoBehaviour
{
    static public ObjectInspectorManagerScript me;
    public TMP_Text objectDes_ui_cht;
    public TMP_Text objectDes_ui_eng;
	public GameObject imageDisplayer;
	public GameObject imageBG;
	public GameObject optionPrefab;
	public GameObject optionSelection;
	private bool textShowing = false;
	public GameObject canvasUI;
	public GameObject canvasDialogue;
	private int index = 0;
	public List<DialogueStruct> dialogueToShow;
	private bool restrictMovement; // does this dialogue make player stand still and do nothing?
	private bool burnAfterReading; // is this a one time only
	private DialogueScript dT; // the dialogue trigger currently being used

	// for options
	public List<GameObject> options;
	private bool optionsDisplaying = false;
	public float optionSpacing;
	private int optionIndex = 0;

	// for auto display text
	private bool autoAdvance;
	private float timer;

	// for customizable end action
	private GameObject doer;
	private string funcToCall;

	private void Awake()
	{
		me = this;
		objectDes_ui_cht.text = "";
		objectDes_ui_eng.text = "";
	}

	public void ShowText(DialogueScript ds)
	{
		dT = ds;
		dialogueToShow = ds.texts;
		restrictMovement = ds.restrictMovement;
		autoAdvance = ds.autoAdvance;
		burnAfterReading = ds.oneTimeDialogue;
		doer = ds.actor;
		funcToCall = ds.funcToCall;
		canvasUI.SetActive(false);
		objectDes_ui_cht.gameObject.SetActive(true);
		objectDes_ui_eng.gameObject.SetActive(true);
		objectDes_ui_cht.text = dialogueToShow[index].description_cht;
		objectDes_ui_eng.text = dialogueToShow[index].description_eng;
		if (restrictMovement) // if this dialogue prohibit player from moving when reading
		{
			PlayerScript.me.GetComponentInChildren<Animator>().Play("readingText");
		}
		StartCoroutine(SetTextShowingToTrue());
		
		// show options
		if (dialogueToShow[index].options.Count > 0) // if there are options for this line
		{
			ShowOptions();
		}

		// show image
		if (ds.image != null)
		{
			imageBG.SetActive(true);
			imageDisplayer.SetActive(true);
			imageDisplayer.GetComponent<Image>().sprite = ds.image;
		}

		timer = dialogueToShow[index].time;
	}

	private void Update()
	{
		if (textShowing)
		{
			if (autoAdvance) // if the dialogue is dispalyed automatically
			{
				if (timer > 0)
				{
					timer -= Time.deltaTime;
					objectDes_ui_cht.text = dialogueToShow[index].description_cht;
					objectDes_ui_eng.text = dialogueToShow[index].description_eng;
				}
				else
				{
					if (index < dialogueToShow.Count - 1)
					{
						index++;
						objectDes_ui_cht.text = dialogueToShow[index].description_cht;
						objectDes_ui_eng.text = dialogueToShow[index].description_eng;
						timer = dialogueToShow[index].time;
					}
					else // when the dialogue ends
					{
						index = 0;
						canvasUI.SetActive(true);
						objectDes_ui_cht.text = "";
						objectDes_ui_eng.text = "";
						textShowing = false;
						if (restrictMovement)
						{
							PlayerScript.me.GetComponentInChildren<Animator>().Play("testIdle");
						}
						imageDisplayer.SetActive(false);
						imageBG.SetActive(false);
						if (doer != null)
						{
							doer.SendMessage(funcToCall);
						}
						if (burnAfterReading)
						{
							Destroy(dT.gameObject);
						}
					}
				}
			}
			else // if the dialogue requires player to press a button to proceed
			{
				if (Input.GetKeyUp(KeyCode.E) && !optionsDisplaying) // if no options being displayed, loop through text
				{
					if (index < dialogueToShow.Count - 1)
					{
						index++;
						objectDes_ui_cht.text = dialogueToShow[index].description_cht;
						objectDes_ui_eng.text = dialogueToShow[index].description_eng;
						if (dialogueToShow[index].options.Count > 0) // if there are options after this line
						{
							ShowOptions();
						}
					}
					else // when the dialogue ends
					{
						index = 0;
						canvasUI.SetActive(true);
						objectDes_ui_cht.text = "";
						objectDes_ui_eng.text = "";
						textShowing = false;
						if (restrictMovement)
						{
							PlayerScript.me.GetComponentInChildren<Animator>().Play("testIdle");
						}
						imageDisplayer.SetActive(false);
						imageBG.SetActive(false);
						if (doer != null)
						{
							doer.SendMessage(funcToCall);
						}
						if (burnAfterReading)
						{
							Destroy(dT.gameObject);
						}
					}
				}
				if (optionsDisplaying) // let player choose
				{
					optionSelection.SetActive(true);
					RectTransform rt = optionSelection.GetComponent<RectTransform>();
					rt.position = new Vector3(rt.position.x, options[optionIndex].GetComponent<RectTransform>().position.y + 23f, rt.position.z);
					if (Input.GetKeyUp(KeyCode.DownArrow))
					{
						if (optionIndex < options.Count - 1)
						{
							optionIndex++;
						}
						else
						{
							optionIndex = 0;
						}
					}
					else if (Input.GetKeyUp(KeyCode.UpArrow))
					{
						if (optionIndex > 0)
						{
							optionIndex--;
						}
						else
						{
							optionIndex = options.Count - 1;
						}
					}
					else if (Input.GetKeyDown(KeyCode.E))
					{
						foreach (var dialogue in dialogueToShow[index].options[optionIndex].dialogues)
						{
							dialogueToShow.Insert(index + 1, dialogue);
						}
						optionSelection.SetActive(false);
						foreach (var option in options)
						{
							Destroy(option.gameObject);
						}
						options.Clear();
						optionsDisplaying = false;
					}
				}
			}
		}
	}

	IEnumerator SetTextShowingToTrue()
	{
		yield return new WaitForSeconds(0.1f);
		textShowing = true;
	}

	private void ShowOptions()
	{
		DialogueStruct thisLine = dialogueToShow[index];
		foreach (var option in thisLine.options)
		{
			GameObject optn = Instantiate(optionPrefab, canvasDialogue.transform);
			options.Add(optn);
			optn.GetComponentInChildren<TMP_Text>().text = option.optionContent;
			optionsDisplaying = true;
		}
		// space out options
		int amountOnOneSide = options.Count / 2;
		if (options.Count % 2 == 0) // if even number of options
		{
			for (int i = 0; i < options.Count; i++)
			{
				RectTransform rt = options[i].GetComponent<RectTransform>();
				if (i < amountOnOneSide) // place these half on top
				{
					float targetY = options[i].GetComponent<RectTransform>().position.y + ((amountOnOneSide - i) * optionSpacing - optionSpacing / 2);
					rt.position = new Vector3(rt.position.x, targetY, rt.position.z);
				}
				else // place these half below
				{
					float targetY = options[i].GetComponent<RectTransform>().position.y - ((i + 1 - amountOnOneSide) * optionSpacing - optionSpacing / 2);
					rt.position = new Vector3(rt.position.x, targetY, rt.position.z);
				}
			}
		}
		else // if uneven unber of options
		{
			for (int i = 0; i < options.Count; i++)
			{
				RectTransform rt = options[i].GetComponent<RectTransform>();
				if (i < amountOnOneSide) // place these half on top
				{
					float targetY = options[i].GetComponent<RectTransform>().position.y + (amountOnOneSide - i) * optionSpacing;
					rt.position = new Vector3(rt.position.x, targetY, rt.position.z);
				}
				else if (i == amountOnOneSide) // doesn't move this one, it's in the middle
				{

				}
				else // place these half below
				{
					float targetY = options[i].GetComponent<RectTransform>().position.y - (i - amountOnOneSide) * optionSpacing;
					rt.position = new Vector3(rt.position.x, targetY, rt.position.z);
				}
			}
		}
	}
}
