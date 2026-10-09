using System;
using UnityEngine;

public sealed class Game
{
    private static Game _instance;
    public static Game Instance => _instance;

    public Game()
    {

    }

    public void Initialize()
    {

    }

    public void Update()
    {
    }

    public void Dispose()
    {
        if (_instance == this)
            _instance = null;
    }
}
