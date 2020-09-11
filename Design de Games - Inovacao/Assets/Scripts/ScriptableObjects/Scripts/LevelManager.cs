using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityCore.Scene;

[RequireComponent(typeof(PhotonView))]
public class LevelManager : MonoBehaviour
{
    public PhotonView pv;

    //public FloatVariable CurrentLevelIndex;
    private bool stopSarrada;

    public int coletaMax = 7;

      #region Singleton
    private static LevelManager _instance;

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(LevelManager).Name;
                    _instance = go.AddComponent<LevelManager>();
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

    private void Start()
    {
        if (pv == null)
        {
            pv = gameObject.AddComponent<PhotonView>();
            pv.ObservedComponents = new List<Component>();
            pv.ObservedComponents.Add(this);
        }
    }

    public void Ganhou()
    {
        GameManager.acabouFase = true;
        GameManager.ganhou = true;
        Debug.Log("Ganhou");
        PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;
        StartCoroutine(DelayToNextScene(true));
    }

    public void Perdeu()
    {
        GameManager.acabouFase = true;
        GameManager.perdeu = true;
        PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 0;
        StartCoroutine(DelayToNextScene(false));
    }

    public IEnumerator DelayToNextScene(bool ganhou)
    {
        GameManager.isPaused = true;
        GameManager.pausaJogo = true;
        yield return new WaitForSeconds(3f);
        if (ganhou)
        {
            if (GameManager.historiaMode == true){GanhouDoKlay();}
            else { GoVitoria();}
        }
        else
        {
            if (GameManager.historiaMode == false){GoDerrota();}
            else{PerdeuDoKlay();}
        }
    }


    public void GanhouDoKlay()
    {
        //if (GameManager.Instance.fase.Equals(GameManager.Fase.Moto))
        if(GameManager.sceneAtual == UnityCore.Scene.SceneType.Moto)
        {
            GameManager.sequestrado = true;
            //PlayerPrefs.SetInt("Sequestrado", 1);
            PlayerPrefsManager.Instance.SavePlayerPrefs("Sequestrado", 1);
        }

        //if (GameManager.Instance.fase.Equals(GameManager.Fase.Corrida))
        if (GameManager.sceneAtual == UnityCore.Scene.SceneType.Corrida)
        {
            GameManager.sequestrado = false;
            //PlayerPrefs.SetInt("Sequestrado", 0);
            PlayerPrefsManager.Instance.SavePlayerPrefs("Sequestrado", 0);
            //PlayerPrefsManager.Instance.SavePlayerPrefs("LevelIndex", 8);
        }
        PlayerPrefsManager.Instance.SavePlayerPrefs("GanhouDoKlay", 1);
        PlayerPrefsManager.Instance.SavePlayerPrefs("LevelIndex", PlayerPrefsManager.Instance.prefsVariables.levelIndex + 1);

        //PlayerPrefs.SetInt("GanhouDoKlay", 1);
        //SceneManager.LoadScene("Historia");
        LoadingManager.instance.LoadNewScene(SceneType.Historia, GameManager.sceneAtual, false);
    }

    public void PerdeuDoKlay()
    {
        FailMessageManager.manualShutdown = true;
        PhotonNetwork.Disconnect();
        PlayerPrefsManager.Instance.SavePlayerPrefs("GanhouDoKlay", 0);
        //PlayerPrefs.SetInt("GanhouDoKlay", 0);
        //SceneManager.LoadScene("HUB");
        LoadingManager.instance.LoadNewScene(SceneType.HUB, GameManager.sceneAtual, false);
    }


    [PunRPC]
     public void GoVitoria()
    {
        PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;
        //CurrentLevelIndex.Value = 3;
        pv.GetComponent<PhotonView>().RPC("GoPodium", RpcTarget.All);
        // Manda o jogador q ganhou pra tela como vitorioso
        // e ativa o GoDerrota() para todos os outros
        Debug.Log("GoVitoria");
    }

    [PunRPC]
    void GoDerrota()
    {
        PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 0;
        //Só é ativado quando alguem ganha
        //CurrentLevelIndex.Value = 3;
        pv.GetComponent<PhotonView>().RPC("GoPodium", RpcTarget.MasterClient);
    }

    [PunRPC]
    public void GoPodium()
    {
        Debug.Log("Podium");
        if (stopSarrada == false)
        {
            Debug.Log("Podium2");
            // PhotonNetwork.LoadLevel("TelaVitoria");
            LoadingManager.instance.LoadNewScene(SceneType.TelaVitoria, GameManager.sceneAtual, true);
            stopSarrada = true;
        }
    }
}
