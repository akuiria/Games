using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum Direction
{
    Up,Right,Down,Left
}

public class GameDataModel
{
    public int[,] Data { get; }

    public int Score { get; private set; }

    private bool mMoved;

    public GameDataModel()
    {
        Data = new int[4,4];
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
        for (var j = Data.GetLength(1) - 1; j >= 0; j--)
        {
            for (var i = Data.GetLength(0) - 1; i >= 0; i--)
            {
                if (i == 0) continue;

                if (Data[i, j] != 0)
                {
                    for (var k = i - 1; k >= 0; k--)
                    {
                        if (Data[k, j] == 0) continue;

                        if (Data[k, j] != Data[i, j]) break;

                        AddScore(Data[i, j]);

                        Data[i, j] *= 2;

                        Data[k, j] = 0;
                        
                        mMoved = true;

                        break;
                    }
                }
                else
                {
                    for (var k = i - 1; k >= 0; k--)
                    {
                        if (Data[k, j] == 0) continue;
                        
                        if (Data[i, j] == 0)
                        {
                            Data[i, j] = Data[k, j];

                            Data[k, j] = 0;

                            mMoved = true;
                        }
                        else if(Data[i, j] == Data[k, j])
                        {
                            AddScore(Data[i, j]);

                            Data[i, j] *= 2;

                            Data[k, j] = 0;

                            mMoved = true;
                        }
                    }
                }
            }
        }
    }

    void MoveLeft()
    {
        for (var j = Data.GetLength(1) - 1; j >= 0; j--)
        {
            for (var i = 0; i < Data.GetLength(0); i++)
            {
                if (i == Data.GetLength(0) - 1) continue;

                if (Data[i, j] != 0)
                {
                    for (var k = i + 1; k < Data.GetLength(0); k++)
                    {
                        if (Data[k, j] == 0) continue;

                        if(Data[k, j] != Data[i, j]) break;

                        AddScore(Data[i, j]);

                        Data[i, j] *= 2;

                        Data[k, j] = 0;

                        mMoved = true;

                        break;
                    }
                }
                else
                {
                    for (var k = i + 1; k < Data.GetLength(0); k++)
                    {
                        if (Data[k, j] == 0) continue;

                        if (Data[i, j] == 0)
                        {
                            Data[i, j] = Data[k, j];

                            Data[k, j] = 0;

                            mMoved = true;
                        }else if (Data[i, j] == Data[k, j])
                        {
                            AddScore(Data[i, j]);

                            Data[i, j] *= 2;

                            Data[k, j] = 0;

                            mMoved = true;
                        }
                    }
                }
            }
        }
    }

    void MoveUp()
    {
        for (var i = 0; i < Data.GetLength(0); i++)
        {
            for (var j = Data.GetLength(1) - 1; j >= 0; j--)
            {
                if (j == 0) continue;

                if (Data[i, j] != 0)
                {
                    //scan down
                    for (var k = j - 1; k >= 0; k--)
                    {
                        if (Data[i, k] == 0) continue;

                        if (Data[i, k] != Data[i, j]) break;

                        AddScore(Data[i, j]);

                        Data[i, j] *= 2;

                        Data[i, k] = 0;

                        mMoved = true;

                        break;
                    }
                }
                else
                {
                    for (var k = j - 1; k >= 0; k--)
                    {
                        if (Data[i, k] == 0) continue;

                        if (Data[i, j] == 0)
                        {
                            Data[i, j] = Data[i, k];

                            Data[i, k] = 0;

                            mMoved = true;
                        }
                        else if (Data[i, j] == Data[i, k])
                        {
                            AddScore(Data[i, j]);

                            Data[i, j] *= 2;

                            Data[i, k] = 0;

                            mMoved = true;
                        }
                    }
                }
            }
        }
    }

    void MoveDown()
    {
        for (var i = 0; i < Data.GetLength(0); i++)
        {
            for (var j = 0; j < Data.GetLength(1); j++)
            {
                if (j == Data.GetLength(1) - 1) continue;

                if (Data[i, j] != 0)
                {
                    for (var k = j + 1; k < Data.GetLength(1); k++)
                    {
                        if (Data[i, k] == 0) continue;

                        if (Data[i, k] != Data[i, j]) break;

                        AddScore(Data[i, j]);

                        Data[i, j] *= 2;

                        Data[i, k] = 0;

                        mMoved = true;

                        break;
                    }
                }
                else
                {
                    for (var k = j + 1; k < Data.GetLength(1); k++)
                    {
                        if (Data[i, k] == 0) continue;

                        if (Data[i, j] == 0)
                        {
                            Data[i, j] = Data[i, k];

                            Data[i, k] = 0;

                            mMoved = true;
                        }
                        else if (Data[i, j] == Data[i, k])
                        {
                            AddScore(Data[i, j]);

                            Data[i, j] *= 2;

                            Data[i, k] = 0;

                            mMoved = true;
                        }
                    }
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
                x = Random.Range(0, Data.GetLength(0));

                y = Random.Range(0, Data.GetLength(1));
            } while (Data[x, y] != 0);

            Data[x, y] = 2;
        }
    }

    bool GameOver()
    {
        for (var i = 0; i < Data.GetLength(0); i++)
        {
            for (var j = 0; j < Data.GetLength(1); j++)
            {
                if (Data[i, j] == 0) return false;
            }
        }

        return true;
    }
}
