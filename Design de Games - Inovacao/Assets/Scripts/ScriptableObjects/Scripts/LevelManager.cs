using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Debug.Log("Start");
        pv = gameObject.AddComponent<PhotonView>();
    }

    public void Ganhou()
    {
        Debug.Log("Ganhou");
        PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;
        if (GameManager.historiaMode == true)
        {
            GanhouDoKlay();
        }
        else
        {
            GoVitoria();
        }
    }

    public void Perdeu()
    {
        PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 0;
        if (GameManager.historiaMode == false)
        {
            PerdeuDoKlay();
        }
    }


    public void GanhouDoKlay()
    {
        PlayerPrefs.SetInt("GanhouDoKlay", 1);
        SceneManager.LoadScene("Historia");

    }

    public void PerdeuDoKlay()
    {
        PlayerPrefs.SetInt("GanhouDoKlay", 0);
        SceneManager.LoadScene("HUB");
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
            PhotonNetwork.LoadLevel("TelaVitoria");
            stopSarrada = true;
        }
    }
}
