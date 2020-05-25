using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class WinnerManager : MonoBehaviour
{
	public PlayerThings player;
	private PhotonView pv;



	[Header("Coletáveis")]

	public bool ganhouCorrida;
	public bool perdeuCorrida;
	[SerializeField]
	private float delayForWinScreen;

    public BoolVariable acabou01;
    public BoolVariable aiGanhou;
    public BoolVariable playerGanhou;
    public FloatVariable flowIndex;
    public bool isMoto;

	private void Start()
	{
		pv = GetComponent<PhotonView>();

        acabou01 = Resources.Load<BoolVariable>("Acabou01");
        aiGanhou = Resources.Load<BoolVariable>("AIGanhou");
        playerGanhou = Resources.Load<BoolVariable>("PlayerGanhou");
        flowIndex = Resources.Load<FloatVariable>("FlowIndex");

        if (pv.IsMine)
		{
			gameObject.GetComponent<PhotonView>().RPC("ZeraPontuacao", RpcTarget.All);

			pv.Controller.SetScore(0);
		}
	}



	// Update is called once per frame
	void Update()
    {
		if (player == null)
		{
			player = FindObjectOfType<PlayerThings>();
		}
		else
		{
            /*if (LinhaDeChegada.changeRoom == true)
			{
				StartCoroutine(Venceu());
			}*/

                if (perdeuCorrida && ganhouCorrida)
                {
                    PerdeuCorrida();
                }
                else if (ganhouCorrida)
                {
                    GanhouCorrida();
                }

			/*
			if (perdeuCorrida)
			{
				PerdeuCorrida();
			}
			*/
		}

    }

	[PunRPC]
	void GanhouCorrida()
	{
        if (acabou01.Value == false)
        {

            if (!isMoto)
            {
                //flowIndex.Value = 7;
                PhotonNetwork.LoadLevel("HistoriaFutebol");
            }
            else
            {
                flowIndex.Value = 6;
                PhotonNetwork.LoadLevel("HistoriaFutebol");
            }

        }

        else
        {

            //PhotonNetwork.LoadLevel("HistoriaFutebol");
            Debug.Log("Ganhou");
            PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;
            //player.ganhouSom.Play();
            //player.playerAnimations.playerAC.SetTrigger(player.playerAnimations.animatorWon);


            //	gameObject.GetComponent<PhotonView>().RPC("ZeraPontuacao", RpcTarget.All);

            gameObject.GetComponent<PhotonView>().RPC("TrocaSala", RpcTarget.All);
            ganhouCorrida = false;
            //PlayerMovement.acabou = true;
        }

    }

	[PunRPC]
	void PerdeuCorrida()
	{
        if (acabou01.Value == true)
        {
            //player.perdeuSom.Play();
            perdeuCorrida = true;
            //PlayerThings.acabou = true;
            PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 0;
            gameObject.GetComponent<PhotonView>().RPC("TrocaSala", RpcTarget.MasterClient);
        }

        else
        {
            PhotonNetwork.LoadLevel("HUB");
        }
	}


	[PunRPC]
	void TrocaSala()
	{
        Debug.Log("TrocaSala");
        ganhouCorrida = false;
		perdeuCorrida = false;
		PhotonNetwork.LoadLevel("TelaVitoria");
	}

	[PunRPC]
	void ZeraPontuacao()
	{
		pv.Controller.SetScore(0);
	}

	[PunRPC]
	public void Terminou()
	{
		PlayerMovement.acabouPartida = true;
	}

	IEnumerator Venceu()
	{
        Debug.Log("Venceu");
		player.cameraManager.SendMessage("ActivateCamera", false);
		pv.RPC("Terminou", RpcTarget.All);
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

		gameObject.GetComponent<PhotonView>().RPC("TrocaSala", RpcTarget.All);

	}


}
