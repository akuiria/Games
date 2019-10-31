using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public int X { get; set; }
    public int Y { get; set; }

    public int Value { get; set; }

    public Tile(int x, int y, int value)
    {
        X = x;
        Y = y;
        Value = value;
    }
}
