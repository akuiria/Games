using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Controller
{
    public override void Execute(object data)
    {
        if (data.ToString() == EventNameCollection.NewGame)
        {
            GetModel<GameModel>().Start();
        }
        else if (data.ToString() == EventNameCollection.AddTileModel)
        {
            GetModel<GameModel>().CreateNumber();
        }
        else if ((Direction) data == Direction.Right)
        {
            GetModel<GameModel>().Move(Direction.Right);
        }
        else if ((Direction)data == Direction.Left)
        {
            GetModel<GameModel>().Move(Direction.Left);
        }
        else if ((Direction)data == Direction.Up)
        {
            GetModel<GameModel>().Move(Direction.Up);
        }
        else if ((Direction)data == Direction.Down)
        {
            GetModel<GameModel>().Move(Direction.Down);
        }
    }
}
