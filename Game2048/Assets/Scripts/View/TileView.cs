using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TileView : MonoBehaviour
{
    private Image mImage;

    private Text mText;

    private RectTransform mRectTransform;

    private GridView mView;

    public void Init(GridView view)
    {
        mRectTransform = (RectTransform) transform;

        mView = view;

        mRectTransform.SetParent(mView.transform.parent);

        mRectTransform.localScale = Vector3.one;

        mRectTransform.anchoredPosition = mView.Position;

        mRectTransform.sizeDelta = mView.Size;
        
        mImage = GetComponent<Image>();

        mText = GetComponentInChildren<Text>();
    }


    public void Move(Vector2 position, float time, UnityAction action)
    {
        DOTween.To
        (
            () => mRectTransform.anchoredPosition,
            m => mRectTransform.anchoredPosition = m,
            position,
            time
        ).OnComplete(() =>
        {
            mRectTransform.anchoredPosition = mView.Position;

            SetText(0);

            action.Invoke();
        }).Play();
    }

    public void ShowText(int number, float time)
    {
        mRectTransform.localScale = Vector3.zero;

        ShowContent(number);

        gameObject.SetActive(true);
        
        mRectTransform.DOScale(Vector3.one, time);
    }

    public void SetText(int number)
    {
        if (number == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);

            ShowContent(number);
        }
    }

    private void ShowContent(int number)
    {
        mText.text = number.ToString();

        mText.color = number == 2 || number == 4 ?
            new Color(124 / 255f, 115 / 255f, 106 / 255f) :
            new Color(1, 247 / 255f, 235 / 255f);

        SetBackgroundColor(number);
    }

    private void SetBackgroundColor(int number)
    {
        mImage.color = TileColorHelpers.GetColor(number);
    }
}
