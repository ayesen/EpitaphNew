using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzelManager : MonoBehaviour
{
    public GameObject Door;
    public List<GameObject> Triggers = new List<GameObject>();
    public bool canOpen = false;

    void Start()
    {
        
    }


    void Update()
    {
        for (int i = 0; i < Triggers.Count; i++)
        {
            //if triger.bool = true, can open = true

        }

        if(canOpen == true)
        {
            Door.GetComponent<DoorScript>().OpenFront();
        }

    }
}