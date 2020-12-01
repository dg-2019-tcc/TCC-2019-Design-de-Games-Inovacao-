using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine.SceneManagement;
using UnityCore.Scene;

public class WinnerManager : MonoBehaviour
{
	public PlayerThings player;
	

	[Header("Coletáveis")]

	public bool ganhouCorrida;
	public bool perdeuCorrida;
	[SerializeField]
	private float delayForWinScreen;

    public BoolVariableArray acabou01;
    public BoolVariableArray aiGanhou;
    public BoolVariable playerGanhou;
    public FloatVariable flowIndex;
    public bool isMoto;

    private string faseNome;

    public FeedbackText feedback;

    public bool buildProfs;

	private bool isloading = false;


    SceneType old;

    #region Unity Function

    private void Start()
    {
        
        isloading = false;
        feedback = FindObjectOfType<FeedbackText>();

        playerGanhou = Resources.Load<BoolVariable>("PlayerGanhou");

        
    }

    void Update()
    {
        if (isloading) return;
        if (player == null)
        {
            player = FindObjectOfType<PlayerThings>();
        }
        else
        {
            if (!isloading)
            {
                if (perdeuCorrida)
                {
                    PerdeuCorrida();
                }
                else if (ganhouCorrida)
                {
                    GanhouCorrida();
                    Debug.Log("Ganhou");
                }
            }
        }

    }

    #endregion

    #region Public Functions

    [PunRPC]
    public void Terminou()
    {
        //PlayerMovement.acabouPartida = true;
    }

    #endregion

    #region Private Functions
	
    void GanhouCorrida()
    {
        if (!isloading)
        {
            feedback.Ganhou();
            playerGanhou.Value = true;
            isloading = true;

            if (!GameManager.historiaMode)
            {
                Debug.Log("Ganhou");
				FinishLevel.instance.Won();
                ganhouCorrida = false;
            }
        }
    }
	
    void PerdeuCorrida()
    {
        feedback.Perdeu();
        if (isloading) return;
        if (buildProfs == false)
        {
            if (acabou01.Value[7] == true/* || buildProfs == false*/)
            {
                //player.perdeuSom.Play();
                perdeuCorrida = true;
                //PlayerThings.acabou = true;
                PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 0;
                pv.RPC("TrocaSala", RpcTarget.MasterClient);
            }

            else
            {
                perdeuCorrida = true;
                playerGanhou.Value = false;
                if (isMoto)
                {
                    aiGanhou.Value[5] = true;
                }
                else
                {
                    aiGanhou.Value[7] = true;
                }
                faseNome = "HUB";
                FailMessageManager.manualShutdown = true;
                PhotonNetwork.Disconnect();
                StartCoroutine("AcabouFase");
                //PhotonNetwork.LoadLevel("HUB");
            }
        }

        else
        {
            perdeuCorrida = true;
            playerGanhou.Value = false;
            aiGanhou.Value[4] = true;
            faseNome = "HUB";
            StartCoroutine("AcabouFase");
        }
    }

    [PunRPC]
    void TrocaSala()
    {
        isloading = true;
        Debug.Log("TrocaSala");
        ganhouCorrida = false;
        perdeuCorrida = false;
       
    }

    

    IEnumerator AcabouFase()
    {
        isloading = true;
        yield return new WaitForSeconds(3f);
        AdMobManager.instance.ShowInterstitialAd();
        LoadingManager.instance.LoadNewScene(SceneType.Historia, old, false);
    }

    IEnumerator Venceu()
    {
        Debug.Log("Venceu");
        player.cameraManager.SendMessage("ActivateCamera", false);
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if ((int)PhotonNetwork.PlayerList[i].CustomProperties["Ganhador"] == 1)
            {
                StopCoroutine(Venceu());
            }
        }
        PhotonNetwork.LocalPlayer.SetScore(-1);
        ganhouCorrida = false;
        yield return new WaitForSeconds(delayForWinScreen);
        PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;

        //gameObject.GetComponent<PhotonView>().RPC("ZeraPontuacao", RpcTarget.All);

        pv.RPC("TrocaSala", RpcTarget.All);

    }

    #endregion
}
