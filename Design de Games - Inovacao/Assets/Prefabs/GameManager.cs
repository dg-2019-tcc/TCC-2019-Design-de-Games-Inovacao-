using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public BoolVariableArray acabou01;
    public BoolVariable demo;
    public BoolVariable escolheFase;
    public BoolVariable pularModoHistoria;
    public FloatVariable faseEscolhida;
    private int faseEsc;

    public static bool inRoom;
    public static bool pausaJogo;
    public static bool historiaMode;
    public static bool sequestrado;
    private int sequestradoPrefs;
    public static int levelIndex;
    public static int ganhouDoKley;
    private string sceneName;

    public enum Fase {Coleta, Futebol, Moto, Corrida, Start, Loja, Tutorial, Hub, Volei, Podium}
    public Fase fase = Fase.Start;
    public Fase lastFase;

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
        pularModoHistoria = Resources.Load<BoolVariable>("PularModoHistoria");

        if (escolheFase.Value || pularModoHistoria.Value)
        {
            EscolheFase();
        }

    }
    #endregion

    public void Start()
    {
        ChecaFase();
        demo = Resources.Load<BoolVariable>("Demo");

        if (demo.Value == true)
        {
            OfflineMode.Instance.AtivaOffline(true);
        }
    }


    public void SaveGame(int index)
    {
        lastFase = fase;
        PlayerPrefs.SetInt("LevelIndex", index);
        Debug.Log("O level salvo foi:" + index);
    }

    public void LoadGame()
    {
        sequestradoPrefs = PlayerPrefs.GetInt("Sequestrado");
        levelIndex = PlayerPrefs.GetInt("LevelIndex");
        ganhouDoKley = PlayerPrefs.GetInt("GanhouDoKlay");
        if(levelIndex < 9)
        {
            historiaMode = true;
            if (sequestradoPrefs == 1)
            {
                sequestrado = true;
            }
            else
            {
                sequestrado = false;
            }
        }
        
        Debug.Log("O level carregado foi: " + levelIndex + " O modo história é: " + historiaMode);
    }

    //Função para escolher a fase ou resetar o jogo
    public void EscolheFase()
    {
        if (pularModoHistoria.Value == false)
        {
            PlayerPrefs.SetInt("Sequestrado", 0);
            faseEsc = Mathf.RoundToInt(faseEscolhida.Value);
            SaveGame(faseEsc);
            escolheFase.Value = false;
            faseEscolhida.Value = 0;
        }
        else
        {
            SaveGame(9);
            escolheFase.Value = false;
            faseEscolhida.Value = 0;
        }
    }

    public void ChecaFase()
    {
        inRoom = PhotonNetwork.InRoom;
        sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "HUB":
                fase = Fase.Hub;
                break;

            case "Tutorial2":
                fase = Fase.Tutorial;
                break;

            case "Coleta":
                fase = Fase.Coleta;
                break;

            case "Corrida":
                fase = Fase.Corrida;
                break;

            case "Moto":
                fase = Fase.Moto;
                break;

            case "Volei":
                fase = Fase.Volei;
                break;

            case "MenuPrincipal":
                fase = Fase.Start;
                break;

            case "Cabelo":
                fase = Fase.Loja;
                break;

            case "Customiza":
                fase = Fase.Loja;
                break;

            case "Shirt":
                fase = Fase.Loja;
                break;

            case "Tenis":
                fase = Fase.Loja;
                break;

            case "TelaVitoria":
                fase = Fase.Podium;
                break;
        }
        Debug.Log("Checando qual fase: " + fase + "InRoom é: " + inRoom);
    }
}
