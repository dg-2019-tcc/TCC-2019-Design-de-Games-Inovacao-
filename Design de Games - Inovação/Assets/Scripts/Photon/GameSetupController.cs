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



	private void OnEnable()
	{
		if (GameSetupController.GS == null)
		{
			GameSetupController.GS = this;
		}
	}

	void Start()
    {
		StartCoroutine( "CreatePlayer", delayToCreate);

		
    }

	private IEnumerator CreatePlayer(float delay)
	{

		yield return new WaitForSeconds(delay);
			Debug.Log("Criando Jogador");
			PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), spawnPoints[Random.Range(0, spawnPoints.Length-1)].position, Quaternion.identity);
		

	}
}
