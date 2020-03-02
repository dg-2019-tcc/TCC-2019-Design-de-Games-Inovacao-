using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : GameManager
{
    public new static LevelManager Instance => GameManager.Instance as LevelManager;

    public Level[] levels;
    public Level CurrentLevel => levels[CurrentLevelIndex];
    public int CurrentLevelIndex = 0;
    public Level LastLevel => levels[levels.Length - 1];



    protected override void Awake()
    {
        base.Awake();
        levels = GetLevels();
    }

    private Level GetLevels()
    {

    }


    public void GoHub()
    {

    }

    public void GoCorrida()
    {

    }

    public void GoColeta()
    {

    }

    public void GoPodium()
    {

    }
}
