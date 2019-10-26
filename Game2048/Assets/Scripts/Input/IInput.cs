using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInput
{
    bool GetUpCommand();
    bool GetDownCommand();
    bool GetRightCommand();

    bool GetLeftCommand();
}
