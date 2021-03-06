using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeInventory : MonoBehaviour
{
    public Image choosenSquare;
    public Image choosenCircle;
    public int choosenMatIndex;
    public DisplayInventory DI;
    public int choosenMat;
    public TextMeshProUGUI description;
    public bool isChanging;

    public int X_Start;
    public int Y_Start;
    public int X_Space_Between_Items;
    public int Number_Of_Column;
    public int Y_Space_Between_Items;

    void Start()
    {
        choosenMatIndex = 4;
        DI = gameObject.GetComponent<DisplayInventory>();
    }

    void Update()
    {
        //Move the square, choose inventory
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            choosenMatIndex += 1;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            choosenMatIndex -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!isChanging)
            {
                if (choosenMatIndex <= DI.Amount_Of_Inventory - 1)
                {
                    choosenMatIndex += 4;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!isChanging)
            {
                if (choosenMatIndex - 4 >= 4)
                {
                    choosenMatIndex -= 4;
                }
            }
        }

        if (!isChanging)
        {
            //choosen mat
            if (Input.GetKeyDown(KeyCode.Space))
            {
                choosenMat = choosenMatIndex;
                choosenMatIndex = 0;
                isChanging = true;
            }
            //Limit range
            if (choosenMatIndex - 4 > DI.Amount_Of_Inventory - 1)
            {
                choosenMatIndex = DI.Amount_Of_Inventory + 3;
            }
            else if (choosenMatIndex < 4)
            {
                choosenMatIndex = 4;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeMat(choosenMatIndex, choosenMat);
                DI.CreateDisplay();
                isChanging = false;
                choosenMatIndex = 4;
            }

            if(choosenMatIndex < 0)
            {
                choosenMatIndex = 0;
            }
            else if(choosenMatIndex > 2)
            {
                choosenMatIndex = 2;
            }
        }

        //Draw the square
        if (!isChanging)
        {
            choosenSquare.color = Color.white;
            choosenSquare.GetComponent<RectTransform>().localPosition = GetPosition(choosenMatIndex - 4);
            choosenCircle.enabled = false;
        }
        else
        {
            choosenSquare.color = new Color32(227, 103, 31, 255);
            choosenCircle.enabled = true;
            choosenCircle.GetComponent<RectTransform>().localPosition = DisplayInventory.Me.GetPosition(choosenMatIndex - 4) + Vector3.up * 190f;
        }
        //Show description
        description.text = DI.inventory[choosenMatIndex].Mats.name;
    }

    public void ChangeMat(int choosenMat, int targetMat)
    {
        InventoryDict temp = PlayerScript.me.tempInventory[choosenMat];
        PlayerScript.me.tempInventory[choosenMat] = PlayerScript.me.tempInventory[targetMat];
        PlayerScript.me.tempInventory[targetMat] = temp;

        InventoryDict tem = DI.inventory[choosenMat];
        DI.inventory[choosenMat] = DI.inventory[targetMat];
        DI.inventory[targetMat] = tem;
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_Start + (X_Space_Between_Items * (i % Number_Of_Column)), Y_Start + (-Y_Space_Between_Items * (i / Number_Of_Column)), 0f);
    }
}
