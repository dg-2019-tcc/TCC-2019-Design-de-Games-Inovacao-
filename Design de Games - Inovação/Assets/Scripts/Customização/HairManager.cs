using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairManager : MonoBehaviour
{
    public GameObject[] hairModel;

	int index;



	public void Start()
    {

        index = (int)PhotonNetwork.LocalPlayer.CustomProperties["hairIndex"];

		if (GetComponent<PhotonView>() != null && GetComponent<PhotonView>().IsMine)
			gameObject.GetComponent<PhotonView>().RPC("TrocaCabelo", RpcTarget.All, index);

	}

	[PunRPC]
	private void TrocaCabelo(int onlineIndex)
	{
		for (int i = 0; i < hairModel.Length; i++)
		{
			hairModel[i].SetActive(false);
		}
		hairModel[onlineIndex].SetActive(true);
	}
}
