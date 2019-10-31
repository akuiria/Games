using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : View
{
    [SerializeField]
    private Text mBest;

    [SerializeField]
    private Text mGame;
    
    public override string Name { get; } = "ScoreView";
    public override void HandleEvent(string name, object data)
    {
        if (name == EventNameCollection.NewGame)
        {
            mGame.text = "0";
            mBest.text = Config.BestScore.ToString();
        }
        else if (name == EventNameCollection.UpdateScore)
        {
            var score = (int) data;

            mGame.text = score.ToString();

            if (score > Config.BestScore)
            {
                Config.BestScore = score;
                mBest.text = Config.BestScore.ToString();
            }
        }
    }

    public override void RegisterAttentionEvent()
    {
        AttentionList.Add(EventNameCollection.NewGame);

        AttentionList.Add(EventNameCollection.UpdateScore);
    }
}
