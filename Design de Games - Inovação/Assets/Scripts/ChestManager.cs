using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public GameObject[] shirtModel;

    public MeshRenderer shirtColor;

    public Material[] shirtMat;

	int index;


	public void Start()
    {


        int index = (int)PhotonNetwork.LocalPlayer.CustomProperties["chestIndex"];

		if (GetComponent<PhotonView>() != null && GetComponent<PhotonView>().IsMine)
			gameObject.GetComponent<PhotonView>().RPC("TrocaCamisa", RpcTarget.All, index);

    }

	[PunRPC]
	private void TrocaCamisa(int onlineIndex)
	{
		for (int i = 0; i < shirtModel.Length; i++)
		{
			shirtModel[i].SetActive(false);
		}
		shirtModel[onlineIndex].SetActive(true);
	}
}
