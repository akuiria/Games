using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    private GridCollectionView mCollectionView;
    
    [SerializeField]
    private ScoreView mScoreView;
    
    public void Init(int size, int best)
    {
        mCollectionView = GetComponentInChildren<GridCollectionView>();

        mCollectionView.Init(size);

        mScoreView.Init(best);
    }
    
    public void AddView(List<CreateMessage> messages, UnityAction action)
    {
        mCollectionView.AddView(messages, action);
    }

    public void MoveView(List<MoveMessage> messages, UnityAction action)
    {
        mCollectionView.MoveView(messages, action);
    }

    public void UpdateScore(int score)
    {
        mScoreView.UpdateScore(score);
    }

    public void Clear()
    {
        mCollectionView.Clear();
    }
}
