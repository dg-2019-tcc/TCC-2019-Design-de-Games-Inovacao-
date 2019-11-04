using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDisplay : MonoBehaviour
{
    public PropsCustom legs;
    public PropsCustom shirt;
    public PropsCustom hair;

    public GameObject[] hairModels;

    public GameObject[] shirtModels;

    public GameObject[] legModels;

    public SkinnedMeshRenderer[] hairColor;

    public SkinnedMeshRenderer[] legsColor;

    public SkinnedMeshRenderer[] shirtsColor;

    public PlayerAvatar playerCustom;

    void Awake()
    {

		if (GetComponent<PhotonView>() != null && GetComponent<PhotonView>().IsMine)
		{
			gameObject.GetComponent<PhotonView>().RPC("TrocaCabelo", RpcTarget.All, playerCustom.hairIndex);

			gameObject.GetComponent<PhotonView>().RPC("TrocaMaterialCabelo", RpcTarget.All, playerCustom.hairColorIndex);

			gameObject.GetComponent<PhotonView>().RPC("TrocaCalca", RpcTarget.All, playerCustom.legsIndex);


			gameObject.GetComponent<PhotonView>().RPC("TrocaMaterialCamisa", RpcTarget.All, playerCustom.shirtColorIndex);	

            gameObject.GetComponent<PhotonView>().RPC("TrocaCamisa", RpcTarget.All, playerCustom.shirtIndex);

			gameObject.GetComponent<PhotonView>().RPC("TrocaMaterialCalca", RpcTarget.All, playerCustom.legsColorIndex);
		}
		else if (!PhotonNetwork.InRoom)
		{
			TrocaCabelo(playerCustom.hairIndex);
			TrocaMaterialCabelo(playerCustom.hairColorIndex);
			TrocaCamisa(playerCustom.shirtIndex);
			TrocaMaterialCamisa(playerCustom.shirtColorIndex);
			TrocaCalca(playerCustom.legsIndex);
			TrocaMaterialCalca(playerCustom.legsColorIndex);

		}
    }


    [PunRPC]
    private void TrocaCabelo(int onlineIndex)
    {
        for (int i = 0; i < hairModels.Length; i++)
        {
            hairModels[i].SetActive(false);
        }
        hairModels[onlineIndex].SetActive(true);
    }

    [PunRPC]
    private void TrocaMaterialCabelo(int onlineIndex)
    {
        for (int i = 0; i < hairColor.Length; i++)
        {
            hairColor[i].material = hair.color[0].corData[playerCustom.hairColorIndex];
        }

        hairColor[playerCustom.hairIndex].material = hair.color[0].corData[onlineIndex];
    }

    [PunRPC]
    private void TrocaCamisa(int onlineIndex)
    {
        for (int i = 0; i < shirtModels.Length; i++)
        {
            shirtModels[i].SetActive(false);
        }
        shirtModels[onlineIndex].SetActive(true);
    }

    [PunRPC]
    private void TrocaMaterialCamisa(int onlineIndex)
    {
        for (int i = 0; i < shirtsColor.Length; i++)
        {
            shirtsColor[i].material = hair.color[0].corData[playerCustom.shirtColorIndex];
        }

        shirtsColor[playerCustom.shirtIndex].material = shirt.color[playerCustom.shirtIndex].corData[onlineIndex];
    }

    [PunRPC]
    private void TrocaCalca(int onlineIndex)
    {
        for (int i = 0; i < legModels.Length; i++)
        {
            legModels[i].SetActive(false);
        }
        legModels[onlineIndex].SetActive(true);
    }

    [PunRPC]
    private void TrocaMaterialCalca(int onlineIndex)
    {
        for (int i = 0; i < legsColor.Length; i++)
        {
            legsColor[i].material = legs.color[0].corData[playerCustom.legsColorIndex];
        }
        legsColor[playerCustom.legsIndex].material = legs.color[playerCustom.legsIndex].corData[onlineIndex];
    }

}
