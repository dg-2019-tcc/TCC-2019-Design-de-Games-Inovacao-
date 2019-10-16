using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsManager : MonoBehaviour
{
    public GameObject[] pantsModel;

	int index;


	public void Start()
    {

        int index = (int)PhotonNetwork.LocalPlayer.CustomProperties["legsIndex"];

		if(GetComponent<PhotonView>() != null && GetComponent<PhotonView>().IsMine)
			gameObject.GetComponent<PhotonView>().RPC("TrocaCalca", RpcTarget.All, index);

		
    }

	[PunRPC]
	private void TrocaCalca(int onlineIndex)
	{
		for (int i = 0; i < pantsModel.Length; i++)
		{
			pantsModel[i].SetActive(false);
		}
		pantsModel[onlineIndex].SetActive(true);
	}
}
