using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    private int Best;

    [SerializeField]
    private Text mBest;

    [SerializeField]
    private Text mGame;

    public void Init(int best)
    {
        Best = best;

        mBest.text = best.ToString();

        mGame.text = "0";
    }

    public void UpdateScore(int score)
    {
        mGame.text = score.ToString();

        if (score > Best)
        {
            Best = score;

            mBest.text = score.ToString();
        }
    }
}
