using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int Size = 4;


    private GameView mView;

    private GameDataModel mModel;

    private bool Playing;
    
    private IInput mInputModule;

    void Start()
    {
        mModel = new GameDataModel(this, Size);

        mView = FindObjectOfType<GameView>();

        mInputModule = new KeyboardInput();

        mView.Init(Size, 0);

        StartGame();
    }

    public void StartGame()
    {
        mModel.Start();
        
        Playing = true;
    }

    public void RestartGame()
    {
        mView.Clear();

        mModel.Restart();

        UpdateScore(mModel.Score);
    }

    public void Move(Direction direction)
    {
        mModel.Move(direction);
    }

    public void AddView(List<CreateMessage> messages)
    {
        if (messages == null || messages.Count == 0) return;

        mView.AddView(messages, () =>
        {
            Playing = true;
        });
    }

    public void MoveView(List<MoveMessage> messages)
    {
        if (messages == null || messages.Count == 0) return;
        
        mView.MoveView(messages, () =>
        {
            UpdateScore(mModel.Score);

            mModel.CreateNumber();

            Playing = true;
        });
    }

    public void UpdateScore(int score)
    {
        mView.UpdateScore(score);
    }
   
    void Update()
    {
        if (!Playing) return;

        if (mInputModule.GetUpCommand())
        {
            Move(Direction.Up);
        }
        else if (mInputModule.GetDownCommand())
        {
            Move(Direction.Down);
        }else if (mInputModule.GetRightCommand())
        {
            Move(Direction.Right);
        }else if (mInputModule.GetLeftCommand())
        {
            Move(Direction.Left);
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
                    Move(Direction.Right);
                }
                else if (offsetPosition.x < MinOffset)
                {
                    Move(Direction.Left);
                }
            }
            else
            {
                if (offsetPosition.y > MinOffset)
                {
                    Move(Direction.Up);
                }
                else if (offsetPosition.y < MinOffset)
                {
                    Move(Direction.Down);
                }
            }
        }
    }


}
