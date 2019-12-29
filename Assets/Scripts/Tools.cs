using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum gameState { Playing };

public static class Tools
{
    public static float[] dest1 = { 0 };
    public static float[] dest2 = { -1.5f, 1.5f };
    public static float[] dest3 = { -2, 0, 2 };
    public static float[] dest4 = { -2.4f, -0.8f, 0.8f, 2.4f };
    public static float[] dest5 = { -2.8f, -1.4f, 0, 1.4f, 2.8f };

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


