using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridCollectionView : View
{
    [SerializeField]
    private GridView mGridPrefab;

    [SerializeField]
    private TileView mTilePrefab;

    private float mBorder = 10;

    private float mTime = 0.2f;

    private float mGridSize;

    private GridView[,] Grids { get; set; }

    private TileView[,] Tiles { get; set; }

    void Init(int size)
    {
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

    void AddView(List<CreateMessage> messages)
    {
        foreach (var message in messages)
        {
            Tiles[message.X, message.Y].ShowText(message.Value, mTime);
        }

        StartCoroutine(OnViewComplete());
    }
    
    void MoveView(List<MoveMessage> messages)
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

        StartCoroutine(OnViewComplete());

        StartCoroutine(OnMoveComplete());
    }

    IEnumerator OnViewComplete()
    {
        yield return new WaitForSeconds(mTime);
        
        SendEvent(EventNameCollection.ViewComplete);
    }

    IEnumerator OnMoveComplete()
    {
        yield return new WaitForSeconds(mTime);

        SendEvent(EventNameCollection.AddTileModel, EventNameCollection.AddTileModel);
    }

    void Clear()
    {
        if (Grids != null)
        {
            for (var i = 0; i < Grids.GetLength(0); i++)
            {
                for (var j = 0; j < Grids.GetLength(1); j++)
                {
                    DestroyImmediate(Grids[i, j].gameObject);
                }
            }

            Grids = null;
        }

        if (Tiles != null)
        {
            for (var i = 0; i < Tiles.GetLength(0); i++)
            {
                for (var j = 0; j < Tiles.GetLength(1); j++)
                {
                    DestroyImmediate(Tiles[i, j].gameObject);
                }
            }

            Tiles = null;
        }
    }
    
    public override string Name { get; } = "GridCollectionView";

    public override void HandleEvent(string name, object data)
    {
        if (name == EventNameCollection.StartGame)
        {
            Clear();

            Init(Config.TileNumber);
        }
        else if (name == EventNameCollection.MoveTile)
        {
            MoveView((List<MoveMessage>)data);
        }
        else if (name == EventNameCollection.AddTile)
        {
            AddView((List<CreateMessage>) data);
        }
    }

    public override void RegisterAttentionEvent()
    {
        AttentionList.Add(EventNameCollection.StartGame);

        AttentionList.Add(EventNameCollection.MoveTile);

        AttentionList.Add(EventNameCollection.AddTile);
    }
}
