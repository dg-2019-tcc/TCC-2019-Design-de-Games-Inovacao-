using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class WinnerManager : MonoBehaviour
{
	public PlayerMovement player;
	private PhotonView pv;



	[Header("Coletáveis")]

	public bool ganhouCorrida;
	public bool perdeuCorrida;
	[SerializeField]
	private float delayForWinScreen;


	private void Start()
	{
		pv = GetComponent<PhotonView>();

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
			player = FindObjectOfType<PlayerMovement>();
		}
		else
		{
			/*if (LinhaDeChegada.changeRoom == true)
			{
				StartCoroutine(Venceu());
			}*/

			if (ganhouCorrida)
			{
				GanhouCorrida();
			}

			if (perdeuCorrida)
			{
				PerdeuCorrida();
			}
		}

    }

	[PunRPC]
	void GanhouCorrida()
	{

        Debug.Log("Ganhou");
		PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;
		player.ganhouSom.Play();
		//player.playerAnimations.playerAC.SetTrigger(player.playerAnimations.animatorWon);
		

		gameObject.GetComponent<PhotonView>().RPC("ZeraPontuacao", RpcTarget.All);

		gameObject.GetComponent<PhotonView>().RPC("TrocaSala", RpcTarget.All);
		ganhouCorrida = false;
		PlayerMovement.acabou = true;

	}

	[PunRPC]
	void PerdeuCorrida()
	{
		player.perdeuSom.Play();
		perdeuCorrida = true;
		PlayerMovement.acabou = true;
		PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 0;
		gameObject.GetComponent<PhotonView>().RPC("TrocaSala", RpcTarget.MasterClient);
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

		gameObject.GetComponent<PhotonView>().RPC("ZeraPontuacao", RpcTarget.All);

		gameObject.GetComponent<PhotonView>().RPC("TrocaSala", RpcTarget.All);

	}


}
