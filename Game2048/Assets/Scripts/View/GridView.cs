using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GridView : MonoBehaviour
{
    public Vector2 Position { get; private set; }

    public Vector2 Size { get; private set; }
    
    public void Init(float length)
    {
        var rectTransform = ((RectTransform) transform);

        rectTransform.sizeDelta = new Vector2(length, length);

        Size = rectTransform.sizeDelta;

        Position = rectTransform.anchoredPosition;
    }
}
