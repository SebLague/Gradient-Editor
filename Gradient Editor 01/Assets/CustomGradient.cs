using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomGradient {


    public Color Evaluate(float time)
    {
        return Color.Lerp(Color.white, Color.black, time);
    }

    public Texture2D GetTexture(int width)
    {
        Texture2D texture = new Texture2D(width, 1);
        Color[] colours = new Color[width];
        for (int i = 0; i < width; i++)
        {
            colours[i] = Evaluate((float)i / (width - 1));
        }
        texture.SetPixels(colours);
        texture.Apply();
        return texture;
    }

}
