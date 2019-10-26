using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IInput
{
    public bool GetUpCommand()
    {
        return Input.GetKeyDown(KeyCode.UpArrow);
    }

    public bool GetDownCommand()
    {
        return Input.GetKeyDown(KeyCode.DownArrow);
    }

    public bool GetRightCommand()
    {
        return Input.GetKeyDown(KeyCode.RightArrow);
    }

    public bool GetLeftCommand()
    {
        return Input.GetKeyDown(KeyCode.LeftArrow);
    }
}
