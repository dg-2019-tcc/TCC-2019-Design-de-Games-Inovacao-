using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public BoolVariableArray acabou01;
    public BoolVariable demo;
    public BoolVariable escolheFase;
    public FloatVariable faseEscolhida;
    private int faseEsc;

    public static bool pausaJogo;
    public static bool historiaMode;
    public static int levelIndex;
    public static int ganhouDoKley;

    public enum Fase {Coleta, Futebol, Moto, Corrida, Start, Tenis, Bazar, Cabelo}
    public Fase fase = Fase.Start;

    #region Singleton
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if(_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(GameManager).Name;
                    _instance = go.AddComponent<GameManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        escolheFase = Resources.Load<BoolVariable>("EscolheFase");
        faseEscolhida = Resources.Load<FloatVariable>("FaseEscolhida");

        if (escolheFase.Value)
        {
            EscolheFase();
        }
    }
    #endregion

    public void Start()
    {
        demo = Resources.Load<BoolVariable>("Demo");
        if(demo.Value == true)
        {
            OfflineMode.Instance.AtivaOffline(true);
        }
    }


    public void SaveGame(int index)
    {
        PlayerPrefs.SetInt("LevelIndex", index);
        Debug.Log("O level salvo foi:" + index);
    }

    public void LoadGame()
    {
        levelIndex = PlayerPrefs.GetInt("LevelIndex");
        ganhouDoKley = PlayerPrefs.GetInt("GanhouDoKlay");
        if(levelIndex < 9)
        {
            historiaMode = true;
        }
        Debug.Log("O level carregado foi:" + levelIndex + "O modo história é: " + historiaMode);
    }

    //Função para escolher a fase ou resetar o jogo
    public void EscolheFase()
    {
        faseEsc = Mathf.RoundToInt(faseEscolhida.Value);
        SaveGame(faseEsc);
        escolheFase.Value = false;
    }
}
