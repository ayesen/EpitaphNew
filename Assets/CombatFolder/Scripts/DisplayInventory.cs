using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public List<InventoryDict> inventory;

    public Image imagePrefab;

    public int Amount_Of_Inventory;

    public int X_Start;
    public int Y_Start;
    public int X_Space_Between_Items;
    public int Number_Of_Column;
    public int Y_Space_Between_Items;

    void Start()
    {
        CopyList();
        CreateDisplay();
    }

    void Update()
    {
        
    }
    
    public void CreateDisplay()
    {
        for (int i = 4; i < inventory.Count; i++)
        {
            if(inventory[i].Mats != null || inventory[i].matAmounts > 0)
            {
                Image obj = Instantiate(imagePrefab, Vector3.zero, Quaternion.identity, transform);
                obj.sprite = inventory[i].Mats.GetComponent<MatScript>().Icon;
                obj.GetComponent<RectTransform>().localPosition = GetPosition((i-4));
                Amount_Of_Inventory += 1;
            }
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_Start + (X_Space_Between_Items *(i % Number_Of_Column)), Y_Start + (-Y_Space_Between_Items * (i/Number_Of_Column)), 0f);
    }

    private void CopyList()
    {
        foreach(var obj in GameObject.Find("Player").GetComponent<PlayerScript>().tempInventory)
        {
            inventory.Add(obj);
        }
    }
}
