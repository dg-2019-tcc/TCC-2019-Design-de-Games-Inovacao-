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
	public BoolVariable playerGanhou;
	public int moedasGanhas = 100;


	private bool isloading;

	private SceneType old;

	private void Awake()
	{
		pv = GetComponent<PhotonView>();
		isloading = false;
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
		playerGanhou = Resources.Load<BoolVariable>("PlayerGanhou");


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


	public void Won()
	{
		if (isloading) return;

		playerGanhou.Value = true;
		pv.RPC("TrocaSala", RpcTarget.All);
		PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;
	}

	public void Lost()
	{
		if (isloading) return;

		playerGanhou.Value = false;
		pv.RPC("TrocaSala", RpcTarget.All);
		PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 0;
	}

	

	

	[PunRPC]
	private void TrocaSala()
	{
		StartCoroutine(TrocaSalaDelay(3f));
	}

	private IEnumerator TrocaSalaDelay(float tempo)
	{
		yield return new WaitForSeconds(tempo);
		moedas.Add(moedasGanhas);
		Debug.Log("Adicionando " + moedasGanhas + " moedas");
		FailMessageManager.manualShutdown = true;
		AdMobManager.instance.ShowInterstitialAd();
		isloading = true;
		PhotonNetwork.LoadLevel("TelaVitoria");
	}

	[PunRPC]
	void ZeraPontuacao()
	{
		pv.Controller.SetScore(0);
	}
}
