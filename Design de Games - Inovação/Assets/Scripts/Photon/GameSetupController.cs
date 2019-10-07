using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSetupController : MonoBehaviour
{
	public static GameSetupController GS;

	public Transform[] spawnPoints;

	public int testIndex;



	private void OnEnable()
	{
		if (GameSetupController.GS == null)
		{
			GameSetupController.GS = this;
		}
	}

	void Start()
    {
		CreatePlayer();

		
    }

	private void CreatePlayer()
	{
		
			Debug.Log("Criando Jogador");
			PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), spawnPoints[Random.Range(0, spawnPoints.Length-1)].position, Quaternion.identity);
		

	}
}
