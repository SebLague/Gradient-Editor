using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GradientEditor : EditorWindow {

    CustomGradient gradient;
    const int borderSize = 10;
    const float keyWidth = 10;
    const float keyHeight = 20;

    private void OnGUI()
    {
        Event guiEvent = Event.current;

        Rect gradientPreviewRect = new Rect(borderSize, borderSize, position.width - borderSize * 2, 25);
        GUI.DrawTexture(gradientPreviewRect, gradient.GetTexture((int)gradientPreviewRect.width));

        for (int i = 0; i < gradient.NumKeys; i++)
        {
            CustomGradient.ColourKey key = gradient.GetKey(i);
            Rect keyRect = new Rect(gradientPreviewRect.x + gradientPreviewRect.width * key.Time - keyWidth / 2f, gradientPreviewRect.yMax + borderSize, keyWidth, keyHeight);
            EditorGUI.DrawRect(keyRect, key.Colour);
        }

        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0)
        {
            Color randomColour = new Color(Random.value, Random.value, Random.value);
            float keyTime = Mathf.InverseLerp(gradientPreviewRect.x, gradientPreviewRect.xMax, guiEvent.mousePosition.x);
            gradient.AddKey(randomColour, keyTime);
            Repaint();
        }
    }

    public void SetGradient(CustomGradient gradient)
    {
        this.gradient = gradient;
    }

    private void OnEnable()
    {
        titleContent.text = "Gradient Editor";
    }
}
