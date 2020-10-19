using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityCore.Scene;


public class GameManager : MonoBehaviour
{
    public BoolVariableArray acabou01;
    public BoolVariable demo;
    public BoolVariable pularModoHistoria;
    public BoolVariable resetaPlayerPrefs;

    public static bool inRoom;
    public static bool pausaJogo;
    public static bool isPaused;
    public static bool historiaMode;
    public static bool sequestrado;
    public static int languageIndex;
    private int sequestradoPrefs;
    public static int levelIndex;
    public static int falaIndex;
    public static int ganhouDoKley;
    private string sceneName;
    public static bool buildPC;
    public static bool needMobileHUD;
    public static bool isLoja;
    public static bool isGame;
    public static bool acabouFase = false;
    public static bool ganhou;
    public static bool perdeu;
    public static bool precisaFalarTV;

    public static SceneType sceneAtual;
    public SceneType scene;
    public SceneType sceneOld;

    #region Unity Function

    #region Singleton
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
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
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public void Start()
    {
        ChecaFase();
        demo = Resources.Load<BoolVariable>("Demo");

        if (demo.Value == true) { OfflineMode.Instance.AtivaOffline(true);}
    }

    #endregion

    #region Public Functions

    public void LoadGame()
    {
        ChecaFase();

        if (CheckPointController.checkPointIndex < 15)
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
        else
        {
            historiaMode = false;
        }

        //Debug.Log("O level carregado foi: " + PlayerPrefsManager.Instance.prefsVariables.levelIndex + " A falaIndex é: " + PlayerPrefsManager.Instance.prefsVariables.falasIndex + " O modo história é: " + historiaMode);
    }


    public void ChecaFase()
    {
        inRoom = PhotonNetwork.InRoom;
        if (sceneAtual == SceneType.Coleta || sceneAtual == SceneType.Corrida || sceneAtual == SceneType.Futebol || sceneAtual == SceneType.Moto || sceneAtual == SceneType.Volei || sceneAtual == SceneType.HUB || sceneAtual == SceneType.Tutorial2)
        {
            if (!buildPC)
            {
                needMobileHUD = true;
            }
            if (sceneAtual != SceneType.HUB)
            {
                isGame = false;
            }
            isLoja = false;
        }
        else if (sceneAtual == SceneType.Cabelo || sceneAtual == SceneType.Shirt || sceneAtual == SceneType.Tenis || sceneAtual == SceneType.Customiza)
        {
            isGame = false;
            isLoja = true;
            needMobileHUD = false;
        }
        else
        {
            isGame = false;
            isLoja = false;
            needMobileHUD = false;
        }
    }

    #endregion

    #region Private Functions

    #endregion
}
