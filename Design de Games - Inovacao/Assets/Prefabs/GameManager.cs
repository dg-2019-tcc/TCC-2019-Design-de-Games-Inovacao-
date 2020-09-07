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
    public BoolVariable escolheFase;
    public BoolVariable pularModoHistoria;
    public BoolVariable resetaPlayerPrefs;
    public FloatVariable faseEscolhida;
    private int faseEsc;

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

   // public enum Fase {Coleta, Futebol, Moto, Corrida, Start, Loja, Tutorial, Hub, Volei, Podium}
    //public Fase fase = Fase.Start;
    //public Fase lastFase;
    public static SceneType sceneAtual;
    public SceneType sceneOld;

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
        resetaPlayerPrefs = Resources.Load<BoolVariable>("ResetaPlayerPrefs");

        if (escolheFase.Value || pularModoHistoria.Value)
        {
            if (pularModoHistoria.Value)
            {
                historiaMode = false;
            }
            EscolheFase();
        }

        /*if (resetaPlayerPrefs.Value == true)
        {
            Debug.Log("ResetaPlayerPrefs  é true");
            PlayerPrefsManager.Instance.prefsVariables.ResetPlayerPrefs();
            PlayerPrefsManager.Instance.SavePlayerPrefs("IsFirstTime", 0);
            resetaPlayerPrefs.Value = false;
        }

        else
        {
            Debug.Log("ResetaPlayerPrefs  é false");
            PlayerPrefsManager.Instance.LoadPlayerPref("All");
        }*/

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

        //PlayerPrefsManager.Instance.LoadPlayerPref("All");

        /*if (PlayerPrefsManager.Instance.prefsVariables.isFirstTime == 0 && GameManager.historiaMode == false)
        {
            PlayerPrefsManager.Instance.prefsVariables.ResetPlayerPrefs();
            PlayerPrefsManager.Instance.SavePlayerPrefs("IsFirstTime", 1);
        }

        else
        {
            PlayerPrefsManager.Instance.LoadPlayerPref("All");
        }*/
    }


    public void SaveGame(int indexFase,int indexFala)
    {
        //lastFase = fase;
        PlayerPrefsManager.Instance.SavePlayerPrefs("LevelIndex", indexFase);
        PlayerPrefsManager.Instance.SavePlayerPrefs("FalasIndex", indexFala);
        //PlayerPrefs.SetInt("LevelIndex", index);
        //Debug.Log("O level salvo foi:" + indexFase + "a fala salva: "+ indexFala);
    }

    public void LoadGame()
    {
        ChecaFase();
        //sequestradoPrefs = PlayerPrefs.GetInt("Sequestrado");
        //levelIndex = PlayerPrefs.GetInt("LevelIndex");
        //ganhouDoKley = PlayerPrefs.GetInt("GanhouDoKlay");
        /*PlayerPrefsManager.Instance.LoadPlayerPref("LevelIndex");
        PlayerPrefsManager.Instance.LoadPlayerPref("FalasIndex");
        PlayerPrefsManager.Instance.LoadPlayerPref("Sequestrado");
        PlayerPrefsManager.Instance.LoadPlayerPref("GanhouDoKlay");

        levelIndex = PlayerPrefsManager.Instance.prefsVariables.levelIndex;
        falaIndex = PlayerPrefsManager.Instance.prefsVariables.falasIndex;
        sequestradoPrefs = PlayerPrefsManager.Instance.prefsVariables.sequestrado;
        ganhouDoKley = PlayerPrefsManager.Instance.prefsVariables.ganhouDoKlay;*/
        //PlayerPrefsManager.Instance.LoadPlayerPref("All");

        if (PlayerPrefsManager.Instance.prefsVariables.levelIndex < 8)
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

    //Função para escolher a fase ou resetar o jogo
    public void EscolheFase()
    {
        if (pularModoHistoria.Value == false)
        {
            //PlayerPrefs.SetInt("Sequestrado", 0);
            PlayerPrefsManager.Instance.SavePlayerPrefs("Sequestrado", 0);
            faseEsc = Mathf.RoundToInt(faseEscolhida.Value);
            SaveGame(faseEsc, faseEsc);
            escolheFase.Value = false;
            faseEscolhida.Value = 0;
        }
        else
        {
            historiaMode = false;
            SaveGame(8,8);
            escolheFase.Value = false;
            faseEscolhida.Value = 0;
        }
    }

    public void ChecaFase()
    {
        inRoom = PhotonNetwork.InRoom;
        if(sceneAtual == SceneType.Coleta || sceneAtual == SceneType.Corrida || sceneAtual == SceneType.Futebol || sceneAtual == SceneType.Moto || sceneAtual == SceneType.Volei || sceneAtual == SceneType.HUB || sceneAtual == SceneType.Tutorial2)
        {
            if (!buildPC)
            {
                needMobileHUD = true;
            }
            if(sceneAtual != SceneType.HUB)
            {
                isGame = false;
            }
            isLoja = false;
        }
        else if (sceneAtual == SceneType.Cabelo|| sceneAtual == SceneType.Shirt || sceneAtual == SceneType.Tenis || sceneAtual == SceneType.Customiza)
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
}
