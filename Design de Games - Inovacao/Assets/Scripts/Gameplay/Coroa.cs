using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Coroa : MonoBehaviour
{
	public Transform ganhador;
	public GameObject IAStarter;
	public GameObject IASetup;


	bool botGanhou;

	
	private void Start()
	{
		botGanhou = true;

		for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
		{
			if ((int)PhotonNetwork.PlayerList[i].CustomProperties["Ganhador"] != 0)
			{
				botGanhou = false;
			}
		}
		
	}
	

	void Update()
    {
		if (ganhador != null)
		{
			transform.position = ganhador.position;

			PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 0;
		}
		else if(botGanhou)
		{
			IAStarter.SetActive(true);
			IASetup.SetActive(true);
		}


	}


	[PunRPC]
	public void euGanhei(Transform sync)
	{
		ganhador = sync;
	}
}
