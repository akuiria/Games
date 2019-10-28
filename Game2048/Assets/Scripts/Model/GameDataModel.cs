using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Direction
{
    Up,Right,Down,Left
}

public class GameDataModel
{
    private readonly int mSize;

    private bool mMoved;

    private Func<int, int, int[]> mIteratorFunc = (start, end) =>
    {
        var length = Mathf.Abs(end - start);

        var result = new int[length];

        if (start < end)
        {
            for (var i = 0; i < length; i++)
            {
                result[i] = start + i;
            }
        }
        else
        {
            for (var i = 0; i < length; i++)
            {
                result[i] = start - i;
            }
        }

        return result;
    };

    public TileModel[,] Data { get; }

    public int Score { get; private set; }
    

    public GameDataModel(int size)
    {
        mSize = size;

        Data = new TileModel[mSize, mSize];

        for (var i = 0; i < mSize; i++)
        {
            for (var j = 0; j < mSize; j++)
            {
                Data[i, j] = new TileModel(i, j, 0);
            }
        }
    }

    public void Start()
    {
        mMoved = false;

        Score = 0;

        RandomNumber();

        RandomNumber();
    }

    public void Move(Direction direction)
    {
        mMoved = false;

        if (direction == Direction.Right)
        {
            MoveRight();
            
        }
        else if (direction == Direction.Left)
        {
            MoveLeft();
            
        }
        else if (direction == Direction.Up)
        {
            MoveUp();
            
        }
        else if (direction == Direction.Down)
        {
            MoveDown();
            
        }
    }
    
    public void AddNumber()
    {
        if (!mMoved) return;

        RandomNumber();
    }

    public void AddScore(int score)
    {
        Score += score;
    }

    void MoveRight()
    {
        for (var j = 0; j < mSize; j++)
        {
            for (var i = mSize - 1; i >= 0; i--)
            {
                if (Data[i, j].Value == 0) continue;

                var target = i;

                var added = false;

                for (var k = i + 1; k < mSize; k++)
                {
                    if (Data[k, j].Value == 0)
                    {
                        target = k;
                    }
                    else if (Data[k, j].Value == Data[i, j].Value)
                    {
                        target = k;

                        added = true;

                        break;
                    }
                    else
                    {
                        break;
                    }
                }

                if (added)
                {
                    Data[target, j].Value += Data[i, j].Value;

                    Data[i, j].Value = 0;

                    mMoved = true;
                }
                else if (target != i)
                {
                    Data[target, j].Value = Data[i, j].Value;

                    Data[i, j].Value = 0;

                    mMoved = true;

                    AddScore(Data[target, j].Value);
                }
            }
        }
    }

    void MoveLeft()
    {
        for (var j = 0; j < mSize; j++)
        {
            for (var i = 0; i < mSize; i++)
            {
                if (Data[i, j].Value == 0) continue;

                var target = i;

                var added = false;

                for (var k = i - 1; k >= 0; k--)
                {
                    if (Data[k, j].Value == 0)
                    {
                        target = k;
                    }
                    else if (Data[k, j].Value == Data[i, j].Value)
                    {
                        target = k;

                        added = true;

                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                
                if (added)
                {
                    Data[target, j].Value += Data[i, j].Value;

                    Data[i, j].Value = 0;

                    mMoved = true;
                }
                else if (target != i)
                {
                    Data[target, j].Value = Data[i, j].Value;

                    Data[i, j].Value = 0;

                    mMoved = true;

                    AddScore(Data[target, j].Value);
                }
            }
        }
    }

    void MoveUp()
    {
        for (var i = 0; i < mSize; i++)
        {
            for (var j = mSize - 1; j >= 0; j--)
            {
                if(Data[i, j].Value == 0) continue;

                var target = j;

                var added = false;

                for (var k = j + 1; k < mSize; k++)
                {
                    if (Data[i, k].Value == 0)
                    {
                        target = k;
                    }
                    else if (Data[i, k].Value == Data[i, j].Value)
                    {
                        target = k;

                        added = true;

                        break;
                    }
                    else
                    {
                        break;
                    }
                }

                if (added)
                {
                    Data[i, target].Value += Data[i, j].Value;

                    Data[i, j].Value = 0;

                    mMoved = true;
                }
                else if (target != j)
                {
                    Data[i, target].Value = Data[i, j].Value;

                    Data[i, j].Value = 0;

                    mMoved = true;

                    AddScore(Data[i, target].Value);
                }
            }
        }
    }

    void MoveDown()
    {
        for (var i = 0; i < mSize; i++)
        {
            for (var j = 0; j < mSize; j++)
            {
                if(Data[i, j].Value == 0) continue;

                var target = j;

                var added = false;

                for (var k = j - 1; k >= 0; k--)
                {
                    if (Data[i, k].Value == 0)
                    {
                        target = k;
                    }
                    else if (Data[i, k].Value == Data[i, j].Value)
                    {
                        target = k;

                        added = true;

                        break;
                    }
                    else
                    {
                        break;
                    }
                }

                if (added)
                {
                    Data[i, target].Value += Data[i, j].Value;

                    Data[i, j].Value = 0;

                    mMoved = true;
                }
                else if (target != j)
                {
                    Data[i, target].Value = Data[i, j].Value;

                    Data[i, j].Value = 0;

                    mMoved = true;

                    AddScore(Data[i, target].Value);
                }
            }
        }
    }

    void RandomNumber()
    {
        if (GameOver())
        {
            //over
        }
        else
        {
            int x, y;

            do
            {
                x = Random.Range(0, mSize);

                y = Random.Range(0, mSize);
            } while (Data[x, y].Value != 0);

            Data[x, y].Value = 2;
        }
    }

    bool GameOver()
    {
        for (var i = 0; i < mSize; i++)
        {
            for (var j = 0; j < mSize; j++)
            {
                if (Data[i, j].Value == 0) return false;
            }
        }

        return true;
    }


    public int[,] GetValue()
    {
        var array = new int[mSize, mSize];

        for (var i = 0; i < mSize; i++)
        {
            for (var j = 0; j < mSize; j++)
            {
                array[i, j] = Data[i, j].Value;
            }
        }

        return array;
    }
}
