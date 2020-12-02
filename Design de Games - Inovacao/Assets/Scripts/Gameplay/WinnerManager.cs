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
    public FloatVariable flowIndex;
    public bool isMoto;

    private string faseNome;

    public FeedbackText feedback;
	

    SceneType old;

    #region Unity Function

    private void Start()
    {
        feedback = FindObjectOfType<FeedbackText>();
    }

    void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerThings>();
        }
        else
        {

			if (perdeuCorrida)
			{
				PerdeuCorrida();
			}
			else if (ganhouCorrida)
			{
				GanhouCorrida();
			}
		}
    }

    #endregion

    #region Public Functions


    #endregion

    #region Private Functions
	
    void GanhouCorrida()
    {
		feedback.Ganhou();

            if (!GameManager.historiaMode)
            {
                Debug.Log("Ganhou");
				LevelManager.Instance.Ganhou();
                ganhouCorrida = false;
            }
        
    }
	
    void PerdeuCorrida()
    {
		
		feedback.Perdeu();
		perdeuCorrida = true;
		if (acabou01.Value[7] == true)
		{
			LevelManager.Instance.Perdeu();
			//player.perdeuSom.Play();
			//PlayerThings.acabou = true;
		}

		else
		{
			if (isMoto)
			{
				aiGanhou.Value[5] = true;
			}
			else
			{
				aiGanhou.Value[7] = true;
			}
			faseNome = "HUB";
			PhotonNetwork.Disconnect();
			StartCoroutine("AcabouFaseHistoria");
			//PhotonNetwork.LoadLevel("HUB");
		}
	}

    [PunRPC]
    void TrocaSala()
    {
        Debug.Log("TrocaSala");
        ganhouCorrida = false;
        perdeuCorrida = false;
       
    }

    
	
    private IEnumerator AcabouFaseHistoria()
    {
        yield return new WaitForSeconds(3f);
		AdMobManager.instance.ShowInterstitialAd();
		FailMessageManager.manualShutdown = true;
		LoadingManager.instance.LoadNewScene(SceneType.Historia, old, false);
    }
	/*
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

    }*/

    #endregion
}
