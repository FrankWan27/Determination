using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum gameState { Playing };

public static class Tools
{
    public static float[] dest1 = { 0 };
    public static float[] dest2 = { -2, 2 };
    public static float[] dest3 = { -2, 0, 2 };
    public static float[] dest4 = { -3, -1, 1, 3 };
    public static float[] dest5 = { -3, -1.5f, 0, 1.5f, 3 };

    public static float[] SetDest(int num)
    {
        switch (num)
        {
            case 1:
                return dest1;
            case 2:
                return dest2;
            case 3:
                return dest3;
            case 4:
                return dest4;
            case 5:
                return dest5;
            default:
                return dest1;
        }
    }
}
