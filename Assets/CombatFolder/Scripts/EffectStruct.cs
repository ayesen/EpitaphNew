using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] ///// structs are not serializable so we need to add this in order to see it in the inspector
public struct EffectStruct
{
    [Header("DMG")]
    public int damageAmount;
    public bool DOT;
    public float DOT_interval;
    public float DOT_duration;
    public bool freezeFrame;

    [Header("CTRL")]
    public EffectManager.CtrlTypes myCtrlType;
    public float ctrl_duration;
    public float knockback_amount;

    [Header("RECOV")]
    public float healAmount;
    public bool HOT;
    public float HOT_interval;
    public float HOT_duration;

    [Header("SPLY")]
    public List<GameObject> matProduce; // for testing only
    public float dropMatAmount;
}
