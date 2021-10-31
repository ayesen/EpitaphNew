using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool showCursor;
    void Start()
    {
        Cursor.visible = showCursor;
    }
}
