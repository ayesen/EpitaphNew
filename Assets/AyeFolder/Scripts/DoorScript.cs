using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Animator DoorAnimator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            OpenFront();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            CloseFront();
        }
    }

    public void OpenFront()
    {
        DoorAnimator.Play("DoorFrontOpen");
    }

    public void CloseFront()
    {
        DoorAnimator.Play("DoorFrontClose");
    }

    public void OpenBack()
    {
        DoorAnimator.Play("DoorBackOpen");
    }

    public void CloseBack()
    {
        DoorAnimator.Play("DoorBackClose");
    }



}
