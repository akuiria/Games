using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridCollectionView : MonoBehaviour
{
    private GridView mGridPrefab;

    private TileView mTilePrefab;

    private float mBorder = 10;

    private float mTime = 0.2f;

    private float mGridSize;

    public GridView[,] Grids { get; private set; }

    private TileView[,] Tiles { get; set; }

    public void Init(int size)
    {
        mGridPrefab = transform.GetChild(0).GetComponent<GridView>();

        mTilePrefab = transform.GetChild(1).GetComponent<TileView>();

        var maxLength = ((RectTransform) transform).rect.width;

        mGridSize = (maxLength - (size + 1) * mBorder) / size;

        Grids = new GridView[size, size];

        Tiles = new TileView[size, size];

        for (var i = 0; i < Grids.GetLength(0); i++)
        {
            for (var j = 0; j < Grids.GetLength(1); j++)
            {
                var position = new Vector3(
                    (mGridSize + mBorder) * i + mBorder + mGridSize / 2,
                    (mGridSize + mBorder) * j + mBorder + mGridSize / 2,
                    0);
                
                Grids[i, j] = Instantiate(mGridPrefab, transform);

                ((RectTransform)Grids[i, j].transform).anchoredPosition = position;
                
                Grids[i, j].Init(mGridSize);

                Grids[i, j].gameObject.SetActive(true);

                Grids[i, j].gameObject.name = $"{i},{j}";
            }
        }

        for (var i = 0; i < Tiles.GetLength(0); i++)
        {
            for (var j = 0; j < Tiles.GetLength(1); j++)
            {
                Tiles[i, j] = Instantiate(mTilePrefab);

                Tiles[i, j].Init(Grids[i, j]);
            }
        }
    }

    public void AddView(List<CreateMessage> messages, UnityAction action)
    {
        foreach (var message in messages)
        {
            Tiles[message.X, message.Y].ShowText(message.Value, mTime);
        }

        StartCoroutine(OnMoveOver(action));
    }
    
    public void MoveView(List<MoveMessage> messages, UnityAction action)
    {
        foreach (var message in messages)
        {
            Tiles[message.StartX, message.StartY].Move(
                Grids[message.EndX, message.EndY].Position,
                mTime,
                () =>
                {
                    Tiles[message.EndX, message.EndY].SetText(message.Value);
                });
        }

        StartCoroutine(OnMoveOver(action));
    }

    IEnumerator OnMoveOver(UnityAction action)
    {
        yield return new WaitForSeconds(mTime);

        action.Invoke();
    }

    public void Clear()
    {
        for (var i = 0; i < Tiles.GetLength(0); i++)
        {
            for (var j = 0; j < Tiles.GetLength(1); j++)
            {
                Tiles[i, j].SetText(0);
            }
        }
    }
}
