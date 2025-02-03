using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    //fields
    private bool cursorVisable;

    // Start is called before the first frame update
    void Start()
    {
        setVisable(false);
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = cursorVisable;
    }

    public void setVisable(bool visable)
    {
        cursorVisable = visable;
    }
}
