using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameView : View
{
    private IInput inputModule = new KeyboardInput();

    private bool mPlaying = false;

    public override string Name { get; } = "GameView";

    public override void HandleEvent(string name, object data)
    {
        if (name == EventNameCollection.StartGame)
        {
            mPlaying = true;
        }
        else if (name == EventNameCollection.AddTile)
        {
            //mPlaying = false;
        }

        else if (name == EventNameCollection.MoveTile)
        {
            //mPlaying = false;
        }
        else if (name == EventNameCollection.ViewComplete)
        {
            //mPlaying = true;
        }
    }

    public override void RegisterAttentionEvent()
    {
        AttentionList.Add(EventNameCollection.StartGame);

        AttentionList.Add(EventNameCollection.AddTile);

        AttentionList.Add(EventNameCollection.MoveTile);

        AttentionList.Add(EventNameCollection.ViewComplete);
    }

    void Update()
    {
        if (!mPlaying) return;

        if (inputModule.GetRightCommand())
        {
            SendEvent(EventNameCollection.MoveTileModel, Direction.Right);
        }
        else if (inputModule.GetLeftCommand())
        {
            SendEvent(EventNameCollection.MoveTileModel, Direction.Left);
        }
        else if (inputModule.GetUpCommand())
        {
            SendEvent(EventNameCollection.MoveTileModel, Direction.Up);
        }
        else if (inputModule.GetDownCommand())
        {
            SendEvent(EventNameCollection.MoveTileModel, Direction.Down);
        }

        MouseInput();
    }

    private Vector3 downPosition;

    private Vector3 offsetPosition;

    private const float MinOffset = 100;

    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            downPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            offsetPosition = Input.mousePosition - downPosition;

            if (Mathf.Abs(offsetPosition.x) < MinOffset &&
                Mathf.Abs(offsetPosition.y) < MinOffset) return;

            if (Mathf.Abs(offsetPosition.x) > Mathf.Abs(offsetPosition.y))
            {
                if (offsetPosition.x > MinOffset)
                {
                    SendEvent(EventNameCollection.MoveTileModel, Direction.Right);
                }
                else if (offsetPosition.x < MinOffset)
                {
                    SendEvent(EventNameCollection.MoveTileModel, Direction.Left);
                }
            }
            else
            {
                if (offsetPosition.y > MinOffset)
                {
                    SendEvent(EventNameCollection.MoveTileModel, Direction.Up);
                }
                else if (offsetPosition.y < MinOffset)
                {
                    SendEvent(EventNameCollection.MoveTileModel, Direction.Down);
                }
            }
        }
    }

    void OnGUI()
    {
        GUILayout.Label("Input State: " + mPlaying.ToString());
    }
    
    public void StartGame()
    {
        SendEvent(EventNameCollection.NewGame, EventNameCollection.NewGame);
    }
}
