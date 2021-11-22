using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeInventory : MonoBehaviour
{
    public Image choosenSquare;
    public int choosenMatIndex;
    public DisplayInventory DI;
    public GameObject choosenMat;
    public TextMeshProUGUI description;

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
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            choosenMatIndex += 1;
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            choosenMatIndex -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (choosenMatIndex <= DI.Amount_Of_Inventory - 1)
            {
                choosenMatIndex += 4;
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (choosenMatIndex - 4 >= 4)
            {
                choosenMatIndex -= 4;
            }
        }
        //Choose Mat
        if (Input.GetKeyDown(KeyCode.Space))
        {
            choosenMat = DI.inventory[choosenMatIndex].Mats;
        }
        //Limit the square
        if (choosenMatIndex - 4 > DI.Amount_Of_Inventory - 1)
        {
            choosenMatIndex = DI.Amount_Of_Inventory + 3;
        }
        else if(choosenMatIndex < 4)
        {
            choosenMatIndex = 4;
        }
        //Draw the square
        choosenSquare.GetComponent<RectTransform>().localPosition = GetPosition(choosenMatIndex - 4);
        //Show description
        description.text = DI.inventory[choosenMatIndex].Mats.name;
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_Start + (X_Space_Between_Items * (i % Number_Of_Column)), Y_Start + (-Y_Space_Between_Items * (i / Number_Of_Column)), 0f);
    }
}
