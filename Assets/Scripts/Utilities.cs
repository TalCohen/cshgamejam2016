using UnityEngine;
using System;
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
        return (ColorType)(c1 | c2);
    }

    public static Color ColorTypeToColor(ColorType colorType)
    {
        switch (colorType)
        {
            case ColorType.Blue:
                return new Color(63/255.0f, 115/255.0f, 184/255.0f);
            case ColorType.Yellow:
                return new Color(217/255.0f, 234/255.0f, 12/255.0f);
            case ColorType.Green:
                return new Color(63/255.0f, 184/255.0f, 80/255.0f);
            case ColorType.Red:
                return new Color(184/255.0f, 63/255.0f, 63/255.0f);
            case ColorType.Purple:
                return new Color(132/255.0f, 63/255.0f, 184/255.0f);
            case ColorType.Orange:
                return new Color(232/255.0f, 126/255.0f, 14/255.0f);
            default:
                return new Color(0, 0, 0);
        }
    }

    public static ColorType StringToColorType(string colorString)
    {
        return (ColorType)Enum.Parse(typeof(ColorType), colorString);
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
