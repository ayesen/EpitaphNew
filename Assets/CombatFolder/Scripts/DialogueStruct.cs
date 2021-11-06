using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DialogueStruct
{
    [TextArea]
    public string description_cht;
    [TextArea]
    public string description_eng;
    public float time; // if this dialogue should be played automatically, this is the time for its duration
    public List<OptionStruct> options;
}
