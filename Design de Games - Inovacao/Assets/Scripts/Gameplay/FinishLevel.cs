using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine.SceneManagement;
using UnityCore.Scene;


public class FinishLevel : MonoBehaviour
{
	public static FinishLevel instance;
	private string currentScene;

	private PhotonView pv;
	public Points moedas;
	public int moedasGanhas = 100;


	private SceneType old;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		currentScene = SceneManager.GetActiveScene().name;


		if (GameManager.historiaMode)
		{
			switch (currentScene)
			{
				default:
					old = SceneType.Corrida;    //no código antigo tava falando pra mudar pra esse quando a cena não era a da moto e quando era a da moto, deixei aqui de forma a mudar fácil se n for pra ser assim
					break;
			}
		}


		pv = GetComponent<PhotonView>();
		

		if (pv.IsMine)
		{
			pv.RPC("ZeraPontuacao", RpcTarget.All);

			pv.Controller.SetScore(0);
		}
	}



	public void Lost()
	{

	}

	public void Won()
	{
		pv.RPC("PunWon", RpcTarget.All);
		PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;
	}

	[PunRPC]
	public void PunWon()
	{
		moedas.Add(moedasGanhas);
		Debug.Log("Adicionando " + moedasGanhas + " moedas");
		TrocaSala();
	}

	[PunRPC]
	private void TrocaSala()
	{
		PhotonNetwork.LoadLevel("TelaVitoria");
	}

	[PunRPC]
	void ZeraPontuacao()
	{
		pv.Controller.SetScore(0);
	}
}
