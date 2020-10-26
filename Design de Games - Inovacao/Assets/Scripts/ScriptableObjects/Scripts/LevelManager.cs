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

    private bool stopSarrada;
    private bool calledNewScene;

    public int coletaMax = 7;

    #region Unity Function

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

    #endregion

    #region Public Functions

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

    #endregion

    #region Private Functions

    private IEnumerator DelayToNextScene(bool ganhou)
    {
        calledNewScene = false;

        GameManager.isPaused = true;
        GameManager.pausaJogo = true;
        LoadingManager.isLoading = false;

        yield return new WaitForSeconds(3f);
        if (ganhou)
        {
            if (GameManager.historiaMode == true) { GanhouDoKlay(); }
            else { GoVitoria(); }
        }
        else
        {
            if (GameManager.historiaMode == false) { GoDerrota(); }
            else { PerdeuDoKlay(); }
        }
        StopCoroutine("DelayToNextScene");
        Debug.Log("[LevelManager] DelayToNextScene");
    }

    private void GanhouDoKlay()
    {

        if (GameManager.sceneAtual == UnityCore.Scene.SceneType.Moto)
        {
            GameManager.sequestrado = true;
            PlayerPrefsManager.Instance.SavePlayerPrefs("Sequestrado", 1);
        }

        if (GameManager.sceneAtual == UnityCore.Scene.SceneType.Corrida)
        {
            GameManager.sequestrado = false;
            PlayerPrefsManager.Instance.SavePlayerPrefs("Sequestrado", 0);
        }

        PlayerPrefsManager.Instance.SavePlayerPrefs("GanhouDoKlay", 1);
        CheckPointController.instance.WonGameCheckPoint();
        Debug.Log("[LevelManager] Ganhou do Klay");
        LoadingManager.instance.LoadNewScene(SceneType.Historia, GameManager.sceneAtual, false);
    }

    private void PerdeuDoKlay()
    {
        if (calledNewScene == false)
        {
            FailMessageManager.manualShutdown = true;
            PhotonNetwork.Disconnect();
            PlayerPrefsManager.Instance.SavePlayerPrefs("GanhouDoKlay", 0);
            LoadingManager.instance.LoadNewScene(SceneType.HUB, GameManager.sceneAtual, false);
            calledNewScene = true;
            Debug.Log("[LevelManager] PerdeuDoKlay");
        }
    }

    [PunRPC]
    private void GoVitoria()
    {
        PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;
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

    #endregion
}
