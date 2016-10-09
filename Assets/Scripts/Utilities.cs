using UnityEngine;
using System.Collections;

public static class Utilities {

    public enum ColorType 
    {
        None = 0,
        Blue = 1,
        Yellow = 2,
        Green = 3,
        Red = 4,
        Purple = 5,
        Orange = 6
    }

    public static ColorType GetCombinedColorType(ColorType c1, ColorType c2)
    {
        return (ColorType)(c1 & c2);
//        if (c1 == c2)
//        {
//            return c1;
//        }
//
//        if (c1 == ColorType.None)
//        {
//            return c2;
//        }
//
//        if (c2 == ColorType.None)
//        {
//            return c1;
//        }
//
//        switch(c1)
//        {
//            case ColorType.Blue:
//                if (c2 == ColorType.Yellow)
//                {
//                    return ColorType.Green
//                } 
//                else if (c2 == ColorType.Red)
//                {
//                    return ColorType.Purple;
//                }
//                else
//                {
//                    Debug.LogError("Invalid color combination!");
//                    return ColorType.None;
//                }
//                break;
//            case ColorType.Yellow:
//                if (c2 == ColorType.Blue)
//                {
//                    return ColorType.Green;
//                }
//                else if (c2 == ColorType.Red)
//                {
//                    return ColorType.Orange;
//                }
//                else
//                {
//                    Debug.LogError("Invalid color combination!");
//                    return ColorType.None;
//                }
//                break;
//            case ColorType.Red:
//                if (c2 == ColorType.
//        }
    }

    public static float GetAveragedAxis(string axis)
    {
        // Get the two axes
        float j1Axis = Input.GetAxis ("J1" + axis);
        float j2Axis = Input.GetAxis ("J2" + axis);
        
        // Return their average
        return (j1Axis + j2Axis) / 2;
    }
}
