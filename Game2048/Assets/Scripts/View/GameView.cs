using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField]
    private Transform mRoot;

    [SerializeField]
    private Text mScore;

    private GridView[,] mGrids;

    public void Init()
    {
        var prefab = mRoot.transform.GetChild(0).
            GetComponent<GridView>();

        mGrids = new GridView[4,4];

        for (var i = 0; i < mGrids.GetLength(0); i++)
        {
            for (var j = 0; j < mGrids.GetLength(1); j++)
            {
                mGrids[i,j] = Instantiate(prefab, mRoot.transform);

                mGrids[i, j].Init();

                var rect = (RectTransform)mGrids[i, j].transform;

                rect.anchoredPosition = 
                    new Vector2(i * 100 + (i + 1) * 10,
                                j * 100 + (j + 1) * 10);

                mGrids[i,j].gameObject.SetActive(true);
                
                mGrids[i, j].gameObject.name = $"{i},{j}";
            }
        }
    }

    public void ChangeView(int[,] data)
    {
        for (int i = 0; i < mGrids.GetLength(0); i++)
        {
            for (int j = 0; j < mGrids.GetLength(1); j++)
            {
                mGrids[i, j].SetText(data[i, j]);
            }
        }
    }

    public void ChangeScore(int score)
    {
        mScore.text = score.ToString();
    }
}
