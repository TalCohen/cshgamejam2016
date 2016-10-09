using UnityEngine;
using System;
using System.Collections;

public static class Utilities {

    private static int MIN_ENEMY_SPAWN_RATE = 2;
    private static int MAX_ENEMY_SPAWN_RATE = 5;

    private static float BASIC_ENEMY_SPAWN_CHANCE = 0.8f;

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

    public static ColorType[] basicColorTypes = {ColorType.Blue, ColorType.Yellow, ColorType.Red};
    public static ColorType[] advancedColorTypes = {ColorType.Green, ColorType.Purple, ColorType.Orange};

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

    public static ColorType GetWeightedRandomColorType()
    {
        ColorType[] colorTypes;

        // Get a random percentage

        float percentage = UnityEngine.Random.Range(0, 100) / 100.0f;

        // Determine if this should be a basic or advanced enemy
        if (percentage < BASIC_ENEMY_SPAWN_CHANCE)
        {
            colorTypes = basicColorTypes;
        }
        else
        {
            colorTypes = advancedColorTypes;
        }

        // Return a random color type in that list
        int index = UnityEngine.Random.Range(0, colorTypes.Length);
        return colorTypes [index];
    }

    public static float GetNextEnemySpawnTime()
    {
        return UnityEngine.Random.Range(MIN_ENEMY_SPAWN_RATE, MAX_ENEMY_SPAWN_RATE);
    }

    public static float GetAveragedAxis(string axis)
    {
        // Get the two axes
        float j1Axis = Input.GetAxis ("J1" + axis);
        float j2Axis = Input.GetAxis ("J2" + axis);
        
        // Return their average
        return (j1Axis + j2Axis) / 2;
    }

    public static IEnumerator FadeOut(GameObject obj, int numberOfFrames)
    {
        SpriteRenderer[] spriteRenderers = obj.GetComponentsInChildren<SpriteRenderer>();
        SpriteRenderer spriteRenderer;
        for (int i = 0; i < numberOfFrames; i++)
        {
            for (int j = 0; j < spriteRenderers.Length; j++)
            {
                spriteRenderer = spriteRenderers [j];
                Color oldColor = spriteRenderer.color;
                Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, oldColor.a - (1.0f / numberOfFrames));
                spriteRenderer.color = newColor;
            }
            yield return null;
        }
        GameObject.Destroy(obj);
    }
}
