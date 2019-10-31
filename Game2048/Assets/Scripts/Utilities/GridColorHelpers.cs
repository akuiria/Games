using System.Collections.Generic;
using UnityEngine;

public static class TileColorHelpers
{
    private static Dictionary<int, Color> mDict;

    static TileColorHelpers()
    {
        mDict = new Dictionary<int, Color>
        {
            {2, new Color(238 / 255f, 228 / 255f, 218 / 255f)},
            {4, new Color(236 /255f, 224 / 255f, 200 / 255f)},
            {8, new Color(242 / 255f, 177 / 255f, 121 / 255f)},
            {16, new Color(245 / 255f, 149 / 255f, 99 / 255f) },
            {32, new Color(245 / 255f, 124 / 255f, 95 / 255f) },
            {64, new Color(246 / 255f, 93 / 255f, 59 / 255f) },
            {128, new Color(237 / 255f, 206 / 255f, 113 / 255f) },
            {256, new Color(237 / 255f, 204 / 255f, 97 / 255f) },
            {512, new Color(236 / 255f, 200 / 255f, 80 / 255f) },
            {1024, new Color(237 / 255f, 197 / 255f, 63 / 255f) },
            {2048, new Color(61 / 255f, 58 / 255f, 51 / 255f) },
        };
    }

     

    public static Color GetColor(int number)
    {
        if (number < 2048)
        {
            return mDict[number];
        }

        return mDict[2048];
    }
}
