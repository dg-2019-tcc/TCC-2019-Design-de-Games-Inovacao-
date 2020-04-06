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
	public float countdown;

	private int index = 1;

	public GameObject[] number;

	private float allPlayersInSession;

    public string playerPrefabName;

	public BoolVariable partidaComecou;

	[HideInInspector]
	static public GameObject PlayerInst;


	private void OnEnable()
	{
		if (GameSetupController.GS == null)
		{
			GameSetupController.GS = this;
		}



        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {

            Debug.Log(i);
            Debug.Log(spawnPoints[i].position);
            Debug.Log(spawnPoints[0].position);
            Debug.Log(spawnPoints[1].position);

            PlayerInst = (GameObject)PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"),
                                spawnPoints[i].position, Quaternion.identity);
            Debug.Log(spawnPoints[i].position);
            Debug.Log(i);
            PlayerInst.SetActive(false);
           
        }
     
            gameObject.GetComponent<PhotonView>().RPC("SpawnPlayer", RpcTarget.All, allPlayersInSession);
	}


	[PunRPC]
	private void SpawnPlayer(float alterPlayerCount)
	{

        Debug.Log("Spawn");
        if (alterPlayerCount > allPlayersInSession)														//Contador pra sincronizar e adicionar quantos players entraram na cena
			allPlayersInSession = alterPlayerCount;

		allPlayersInSession++;
		if (PhotonNetwork.PlayerList.Length == allPlayersInSession || alterPlayerCount == 0)            //Checando se todos entraram, se sim, todos são criados ao mesmo tempo(se falhar, outro player vai passar pelo mesmo)
		{
			StartCoroutine("UniteSynchronization", delayToCreate);
		}
	}

	public IEnumerator UniteSynchronization(float delay)
	{
		yield return new WaitForSeconds(delay);
		if (number.Length > 0)
		{
			number[number.Length - index].SetActive(true);
			yield return new WaitForSeconds(1);
			number[number.Length - index].SetActive(false);
		}
			if(index<number.Length)
				StartCoroutine("UniteSynchronization", 0);
			else
				PlayerInst.SetActive(true);
		partidaComecou.Value = true;
		index++;
	}
}
