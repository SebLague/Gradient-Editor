using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomGradient {

    [SerializeField]
    List<ColourKey> keys = new List<ColourKey>();

    public Color Evaluate(float time)
    {
        if (keys.Count == 0)
        {
            return Color.white;
        }

        ColourKey keyLeft = keys[0];
        ColourKey keyRight = keys[keys.Count - 1];

        for (int i = 0; i < keys.Count-1; i++)
        {
            if (keys[i].Time <= time && keys[i + 1].Time >= time)
            {
                keyLeft = keys[i];
                keyRight = keys[i + 1];
                break;
            }
        }

        float blendTime = Mathf.InverseLerp(keyLeft.Time, keyRight.Time, time);
        return Color.Lerp(keyLeft.Colour, keyRight.Colour, blendTime);
    }

    public void AddKey(Color colour, float time)
    {
        ColourKey newKey = new ColourKey(colour, time);
        for (int i = 0; i < keys.Count; i++)
        {
            if (newKey.Time < keys[i].Time)
            {
                keys.Insert(i, newKey);
                return;
            }
        }

        keys.Add(newKey);
    }

    public int NumKeys
    {
        get
        {
            return keys.Count;
        }
    }

    public ColourKey GetKey(int i)
    {
        return keys[i];
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

    [System.Serializable]
    public struct ColourKey
    {
        [SerializeField]
        Color colour;
        [SerializeField]
        float time;

        public ColourKey(Color colour, float time)
        {
            this.colour = colour;
            this.time = time;
        }

        public Color Colour
        {
            get
            {
                return colour;
            }
        }

        public float Time
        {
            get
            {
                return time;
            }
        }
    }

}
