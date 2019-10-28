using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    private GridLayoutGroup mRoot;

    [SerializeField]
    private Text mScore;

    private GridView[,] mGrids;

    public void Init(int size)
    {
        mRoot = GetComponentInChildren<GridLayoutGroup>();

        var length = (mRoot.GetComponent<RectTransform>().sizeDelta.x -
                      (size + 1) * mRoot.spacing.x) / size;

        mRoot.cellSize = new Vector2(length, length);
        
        var prefab = mRoot.transform.GetChild(0).
            GetComponent<GridView>();

        mGrids = new GridView[size, size];

        for (var i = 0; i < mGrids.GetLength(0); i++)
        {
            for (var j = 0; j < mGrids.GetLength(1); j++)
            {
                mGrids[i,j] = Instantiate(prefab, mRoot.transform);

                mGrids[i, j].Init();
                
                mGrids[i,j].gameObject.SetActive(true);
                
                mGrids[i, j].gameObject.name = $"{i},{j}";
            }
        }
    }

    public void ChangeView(int[,] data)
    {
        for (var i = 0; i < mGrids.GetLength(0); i++)
        {
            for (var j = 0; j < mGrids.GetLength(1); j++)
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
