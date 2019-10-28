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
        mModel = new GameDataModel(Size);

        mView = FindObjectOfType<GameView>();

        mInputModule = new KeyboardInput();

        mView.Init(Size);

        StartGame();
    }

    public void StartGame()
    {
        mModel.Start();

        ChangeView(mModel);

        ChangeScore(mModel);

        Playing = true;
    }

    public void ChangeView(GameDataModel model)
    {
        mView.ChangeView(model.GetValue());
    }

    public void ChangeScore(GameDataModel model)
    {
        mView.ChangeScore(model.Score);
    }

    public void Move(Direction direction)
    {
        mModel.Move(direction);

        ChangeView(mModel);

        ChangeScore(mModel);

        StartCoroutine(AnimationPlay());
    }

    IEnumerator AnimationPlay()
    {
        Playing = false;

        yield return new WaitForSeconds(0.2f);

        mModel.AddNumber();

        ChangeView(mModel);

        Playing = true;
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
