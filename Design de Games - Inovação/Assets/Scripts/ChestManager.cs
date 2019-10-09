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


        int indexShirt = (int)PhotonNetwork.LocalPlayer.CustomProperties["chestIndex"];
        int indexShirtMaterial = (int)PhotonNetwork.LocalPlayer.CustomProperties["shirtIndex"];

        if (GetComponent<PhotonView>() != null && GetComponent<PhotonView>().IsMine)
        {
            gameObject.GetComponent<PhotonView>().RPC("TrocaCamisa", RpcTarget.All, indexShirt);
            gameObject.GetComponent<PhotonView>().RPC("TrocaMaterialCamisa", RpcTarget.All, indexShirtMaterial);
        }

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

    [PunRPC]
    private void TrocaMaterialCamisa(int onlineIndex)
    {
        shirtColor.material = shirtMat[onlineIndex];
    }
}
