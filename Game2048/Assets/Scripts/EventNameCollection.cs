using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventNameCollection
{
    public static string NewGame { get; } = "NewGame";

    public static string StartGame { get; } = "StartGame";

    public static string UpdateScore { get; } = "UpdateScore";

    public static string MoveTile { get; } = "MoveTile";

    public static string AddTile { get; } = "AddTile";

    public static string ViewComplete { get; } = "ViewComplete";

    public static string MoveTileModel { get; } = "MoveTileModel";

    public static string AddTileModel { get; } = "AddTileModel";
}
