﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Direction
{
    Up,Right,Down,Left
}

public class GameModel : Model
{
    public override string Name { get; }

    private int mSize;
    
    private bool mMoved;

    private Tile[,] Data;

    private int Score;
    

    public GameModel()
    {
        Name = nameof(GameModel);
    }

    public void Start()
    {
        mSize = Config.TileNumber;

        Data = new Tile[mSize, mSize];

        for (var i = 0; i < mSize; i++)
        {
            for (var j = 0; j < mSize; j++)
            {
                Data[i, j] = new Tile(i, j, 0);
            }
        }

        mMoved = true;

        Score = 0;

        SendEvent(EventNameCollection.StartGame);
        
        SendEvent(EventNameCollection.UpdateScore, Score);
        
        CreateNumber();

        CreateNumber();
    }
    
    public void Move(Direction direction)
    {
        mMoved = false;

        var messages = new List<MoveMessage>();

        if (direction == Direction.Right)
        {
            MoveRight(messages);
        }
        else if (direction == Direction.Left)
        {
            MoveLeft(messages);
        }
        else if (direction == Direction.Up)
        {
            MoveUp(messages);
        }
        else if (direction == Direction.Down)
        {
            MoveDown(messages);
        }

        SendEvent(EventNameCollection.MoveTile, messages);
    }
    
    public void CreateNumber()
    {
        if (!mMoved) return;

        var messages = new List<CreateMessage>();

        RandomNumber(messages);

        if (messages.Count != 0)
        {
            SendEvent(EventNameCollection.AddTile, messages);
        }
    }

    public void AddScore(int score)
    {
        Score += score;

        SendEvent(EventNameCollection.UpdateScore, Score);
    }

    void MoveRight(ICollection<MoveMessage> messages)
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

                    messages.Add(new MoveMessage(i, j, target, j, Data[target, j].Value));
                    
                    AddScore(Data[target, j].Value);

                    mMoved = true;
                }
                else if (target != i)
                {
                    Data[target, j].Value = Data[i, j].Value;

                    Data[i, j].Value = 0;

                    messages.Add(new MoveMessage(i, j, target, j, Data[target, j].Value));

                    mMoved = true;
                }
            }
        }
    }

    void MoveLeft(ICollection<MoveMessage> messages)
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

                    AddScore(Data[target, j].Value);

                    messages.Add(new MoveMessage(i, j, target, j, Data[target, j].Value));

                    mMoved = true;
                }
                else if (target != i)
                {
                    Data[target, j].Value = Data[i, j].Value;

                    Data[i, j].Value = 0;

                    messages.Add(new MoveMessage(i, j, target, j, Data[target, j].Value));

                    mMoved = true;
                }
            }
        }
    }

    void MoveUp(ICollection<MoveMessage> messages)
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

                    AddScore(Data[i, target].Value);

                    messages.Add(new MoveMessage(i, j, i, target, Data[i, target].Value));
                    
                    mMoved = true;
                }
                else if (target != j)
                {
                    Data[i, target].Value = Data[i, j].Value;

                    Data[i, j].Value = 0;

                    messages.Add(new MoveMessage(i, j, i, target, Data[i, target].Value));

                    mMoved = true;
                }
            }
        }
    }

    void MoveDown(ICollection<MoveMessage> messages)
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

                    AddScore(Data[i, target].Value);

                    messages.Add(new MoveMessage(i, j, i, target, Data[i, target].Value));

                    mMoved = true;
                }
                else if (target != j)
                {
                    Data[i, target].Value = Data[i, j].Value;

                    Data[i, j].Value = 0;

                    messages.Add(new MoveMessage(i, j, i, target, Data[i, target].Value));

                    mMoved = true;
                }
            }
        }
    }

    void RandomNumber(ICollection<CreateMessage> messages)
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

            //add number
            messages.Add(new CreateMessage(x, y, 2));
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
}

public struct MoveMessage
{
    public int StartX, StartY, EndX, EndY, Value;

    public MoveMessage(int startX, int startY, int endX, int endY, int value)
    {
        StartX = startX;
        StartY = startY;
        EndX = endX;
        EndY = endY;

        Value = value;
    }
}

public struct CreateMessage
{
    public int X, Y, Value;

    public CreateMessage(int x, int y,  int value)
    {
        X = x;
        Y = y;

        Value = value;
    }
}
