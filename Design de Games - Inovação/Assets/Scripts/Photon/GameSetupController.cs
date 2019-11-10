using Photon.Pun;
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
	}

	[PunRPC]
	private void CreatePlayer(float alterPlayerCount)
	{
		if (alterPlayerCount > allPlayersInSession)														//Contador pra sincronizar e adicionar quantos players entraram na cena
			allPlayersInSession = alterPlayerCount;

		allPlayersInSession++;
		if (PhotonNetwork.PlayerList.Length == allPlayersInSession)                                     //Checando se todos entraram, se sim, todos são criados ao mesmo tempo(se falhar, outro player vai passar pelo mesmo)
		{
			StartCoroutine("UniteSynchronization", delayToCreate);
		}
	}

	
	public IEnumerator UniteSynchronization(float delay)
	{		
		yield return new WaitForSeconds(delay);
		PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position, Quaternion.identity);
	}
}
