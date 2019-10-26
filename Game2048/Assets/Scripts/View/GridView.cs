using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridView : MonoBehaviour
{
    private Transform mRoot;

    private Image mImage;

    private Text mText;

    public void Init()
    {
        mRoot = transform.GetChild(0);

        mImage = mRoot.GetComponent<Image>();

        mText = mRoot.GetComponentInChildren<Text>();
    }

    public void SetText(int number)
    {
        if (number == 0)
        {
            mRoot.gameObject.SetActive(false);
        }
        else
        {
            mRoot.gameObject.SetActive(true);

            mText.text = number.ToString();

            mText.color = number == 2 || number == 4 ?
                new Color(124 / 255f, 115 / 255f, 106 / 255f) :
                new Color(1, 247 / 255f, 235 / 255f);

            SetBackgroundColor(number);
        }
    }

    private void SetBackgroundColor(int number)
    {
        mImage.color = TileColorHelpers.GetColor(number);
    }
}
