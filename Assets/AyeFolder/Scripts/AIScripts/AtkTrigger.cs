using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkTrigger : MonoBehaviour
{
    public bool onAtkTrigger = false;
    public MeshRenderer myMR;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "RealPlayer")
        {
            onAtkTrigger = true;
            //myMR.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "RealPlayer")
        {
            
            onAtkTrigger = false;
            //myMR.enabled = false;
        }
    }

}
