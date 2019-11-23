﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSetupController : MonoBehaviour
{
	public static GameSetupController GS;

	public Transform[] spawnPoints;

	public float delayToCreate;

	private float allPlayersInSession;
	private GameObject PlayerInst;


	private void OnEnable()
	{
		if (GameSetupController.GS == null)
		{
			GameSetupController.GS = this;
		}
	}

	void Start()
	{
		
		gameObject.GetComponent<PhotonView>().RPC("CreatePlayer", RpcTarget.All, allPlayersInSession);
		PlayerInst = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position, Quaternion.identity);
		PlayerInst.SetActive(false);
	}

	[PunRPC]
	private void CreatePlayer(float alterPlayerCount)
	{
		if (alterPlayerCount > allPlayersInSession)														//Contador pra sincronizar e adicionar quantos players entraram na cena
			allPlayersInSession = alterPlayerCount;

		allPlayersInSession++;
		if (PhotonNetwork.PlayerList.Length == allPlayersInSession || alterPlayerCount == 0)                                     //Checando se todos entraram, se sim, todos são criados ao mesmo tempo(se falhar, outro player vai passar pelo mesmo)
		{
			StartCoroutine("UniteSynchronization", delayToCreate);
		}
	}

	void Conectou()
	{
		//Instantiate(Resources.Load("PhotonPrefabs/PhotonPlayer"), spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position, Quaternion.identity);
		//CreatePlayer(0);
	}

	
	public IEnumerator UniteSynchronization(float delay)
	{		
		yield return new WaitForSeconds(delay);
		/*if (gameObject.GetComponent<PhotonView>().IsMine)
		{
			Debug.Log(gameObject.GetComponent<PhotonView>().Owner.UserId);
			PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position, Quaternion.identity);
		}
		*/
		PlayerInst.SetActive(true);
	}
}
