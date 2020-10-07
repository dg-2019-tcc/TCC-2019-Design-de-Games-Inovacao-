using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.Scene;

public class CheckPointController : MonoBehaviour
{
    public static CheckPointController instance;

    public static int checkPointIndex;
    public static int nextFalaIndex;
    public static int nextFaseIndex;

    //Para conseguir ver no Inspector
    public int falaIndex;
    public int faseIndex;
    public int checkPoint;

    public static bool finishedGame;

    #region Singleton

    public static CheckPointController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CheckPointController>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(CheckPointController).Name;
                    instance = go.AddComponent<CheckPointController>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public void LoadCheckPoint()
    {
        checkPointIndex = PlayerPrefs.GetInt("CheckPointIndex");
        SetCheckPoint();
    }

    private void SaveCheckPoint()
    {
        PlayerPrefs.SetInt("CheckPointIndex", checkPointIndex);
        SetCheckPoint();
    }

    public void TalkCheckPoint(int value)
    {
        checkPointIndex = value;
        SaveCheckPoint();
    }

    public void WonGameCheckPoint()
    {
        switch (GameManager.sceneAtual)
        {
            case SceneType.Coleta:
                checkPointIndex = 4;
                break;

            case SceneType.Tenis:
                checkPointIndex = 6;
                break;

            case SceneType.Futebol:
                checkPointIndex = 8;
                break;

            case SceneType.Moto:
                checkPointIndex = 10;
                break;

            case SceneType.Shirt:
                checkPointIndex = 12;
                break;

            case SceneType.Corrida:
                checkPointIndex = 14;
                break;
        }
        SaveCheckPoint();
    }

    private void SetCheckPoint()
    {
        switch (checkPointIndex)
        {
            //Depois do Tutorial
            case 1:
                nextFalaIndex = 1;
                nextFaseIndex = 0;
                break;

            //Depois da 1° fala, antes da 2°
            case 2:
                nextFalaIndex = 2;
                nextFaseIndex = 0;
                break;

            //Depois da 2° fala, antes da coleta    
            case 3:
                nextFalaIndex = 0;
                nextFaseIndex = 1;
                break;

            //Depois da coleta, antes da 3° fala
            case 4:
                nextFalaIndex = 3;
                nextFaseIndex = 0;
                break;
            // Depois da 3° fala, antes do Tenis
            case 5:
                nextFalaIndex = 0;
                nextFaseIndex = 2;
                break;
            //Depois do tenis, antes da 4° fala
            case 6:
                nextFalaIndex = 4;
                nextFaseIndex = 0;
                break;
            //Depois da 4° fala, antes do Futebol
            case 7:
                nextFalaIndex = 0;
                nextFaseIndex = 3;
                break;
            //Depois do Futebol, antes da 5° fala
            case 8:
                nextFalaIndex = 5;
                nextFaseIndex = 0;
                break;
            //Depois da 5° fala, antes da moto
            case 9:
                nextFalaIndex = 0;
                nextFaseIndex = 4;
                break;
            //Depois da moto, antes da 6° fala
            case 10:
                nextFalaIndex = 6;
                nextFaseIndex = 0;
                break;
            //Depois da 6° fala, antes da roupas
            case 11:
                nextFalaIndex = 0;
                nextFaseIndex = 5;
                break;
            //Depois das Roupas, antes da 7° fala
            case 12:
                nextFalaIndex = 7;
                nextFaseIndex = 0;
                break;
        //Depois da 7° fala antes da corrida
            case 13:
                nextFalaIndex = 0;
                nextFaseIndex = 6;
                break;
            //Depois da corrida antes da 8° fala
            case 14:
                nextFalaIndex = 8;
                nextFaseIndex = 0;
                break;

            case 15:
                finishedGame = true;
                nextFalaIndex = 0;
                nextFaseIndex = 0;
                break;
        }

        checkPoint = checkPointIndex;
        falaIndex = nextFalaIndex;
        faseIndex = nextFaseIndex;
        Debug.Log("{CheckPointController] Setou CheckPoint");
    }

    private void OnApplicationQuit()
    {
        SaveCheckPoint();
    }
}
