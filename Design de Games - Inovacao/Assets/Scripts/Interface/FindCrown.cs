using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FindCrown : MonoBehaviour
{
    private NewPlayerMovent player;
    void Start()
    {
        player = GetComponent<NewPlayerMovent>();
		if ((int)PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] == 1)
		{
            player.ganhou = true;
			GameObject.FindObjectOfType<Coroa>().ganhador = transform;

			GameObject.FindObjectOfType<Coroa>().GetComponent<PhotonView>().RPC("euGanhei", RpcTarget.All, transform);
		}
	}
	
}
