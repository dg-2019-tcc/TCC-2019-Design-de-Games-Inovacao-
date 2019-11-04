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
        //Debug para dizer que o jogador está sendo criado
        //Debug.Log("Criando Jogador");
        yield return new WaitForSeconds(delay);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position, Quaternion.identity);
    }
}
