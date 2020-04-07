using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Coroa : MonoBehaviour
{
	public Transform ganhador;

	/*
	private void Start()
	{
		if ((int)PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] == 1)
		{
            euGanhei();
		}
	}*/
	

	void Update()
    {
		if (ganhador != null)
		{
			transform.position = ganhador.position;

			PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 0;
		}
		


	}


	[PunRPC]
	public void euGanhei(Transform sync)
	{
		ganhador = sync;
	}
}
