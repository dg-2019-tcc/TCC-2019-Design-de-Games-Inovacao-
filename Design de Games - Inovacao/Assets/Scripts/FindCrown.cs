using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FindCrown : MonoBehaviour
{
    void Start()
    {
		if ((int)PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] == 1)
		{


			GameObject.FindObjectOfType<Coroa>().ganhador = transform;

			GameObject.FindObjectOfType<Coroa>().GetComponent<PhotonView>().RPC("euGanhei", RpcTarget.All, transform);
		}
	}
	
}
