using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{

    public Level(LevelType type)
    {

        if (type.Equals(LevelType.Hub))
        {
            Initialize(type, false, false, 0, 1);
        }

        else if (type.Equals(LevelType.Corrida))
        {
            Initialize(type, true, false, 1, 4);
        }

        else if (type.Equals(LevelType.Corrida))
        {
            Initialize(type, true, false, 7, 4);
        }

        else if (type.Equals(LevelType.Podium))
        {
            Initialize(type, false, true, 0, 4);
        }
    }

    public Level(LevelType type, bool isGame, bool haveWinner, int tokens, int players)
    {
        Initialize(type, isGame, haveWinner, tokens, players);
    }

    public void Initialize(LevelType type, bool isGame, bool haveWinner, int tokens, int players)
    {
        if (isGame)
        {
            // Instancia as fases
        }

        else
        {
            if (!haveWinner)
            {
                //Instancia a HUB
            }

            else
            {
                //Instancia a tela do Podium
            }
        }
    }


    public enum LevelType
    {
        Hub,
        Corrida,
        Coleta,
        Podium
    }
}
