﻿using Photon.Pun;
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

    void Awake()
    {

		/* //hair.propIndex = PlayerPrefs.GetInt("hairIndex");
		 PhotonNetwork.LocalPlayer.CustomProperties["hairIndex"] = hair.propIndex;

		 //PlayerPrefs.SetInt("shirtIndex", shirt.propIndex);
		 //shirt.propIndex = PlayerPrefs.GetInt("shirtIndex");
		 PhotonNetwork.LocalPlayer.CustomProperties["shirtIndex"] = shirt.propIndex;

		 //PlayerPrefs.SetInt("legsIndex", legsIndex);
		 //legs.propIndex = PlayerPrefs.GetInt("legsIndex");
		 PhotonNetwork.LocalPlayer.CustomProperties["legsIndex"] = legs.propIndex;

		 //hair.colorIndex = PlayerPrefs.GetInt("hairColorIndex");
		 PhotonNetwork.LocalPlayer.CustomProperties["hairColorIndex"] = hair.colorIndex;

		 //shirt.colorIndex = PlayerPrefs.GetInt("shirtColorIndex");
		 PhotonNetwork.LocalPlayer.CustomProperties["shirtColorIndex"] = shirt.colorIndex;

		 //legs.colorIndex = PlayerPrefs.GetInt("legsColorIndex");
		 PhotonNetwork.LocalPlayer.CustomProperties["legsColorIndex"] = legs.colorIndex;*/


		/*hairModels[hair.propIndex].SetActive(true);
        shirtModels[shirt.propIndex].SetActive(true);
        legModels[legs.propIndex].SetActive(true);

        hairColor[hair.propIndex].material = hair.color[0].corData[hair.colorIndex];
        shirtsColor[shirt.propIndex].material = shirt.color[shirt.propIndex].corData[shirt.colorIndex];
        legsColor[legs.propIndex].material = legs.color[legs.propIndex].corData[legs.colorIndex];*/



		PhotonNetwork.LocalPlayer.CustomProperties["hairIndex"] = hair.propIndex;
		PhotonNetwork.LocalPlayer.CustomProperties["shirtIndex"] = shirt.propIndex;
		PhotonNetwork.LocalPlayer.CustomProperties["legsIndex"] = legs.propIndex;
		PhotonNetwork.LocalPlayer.CustomProperties["hairColorIndex"] = hair.colorIndex;
		PhotonNetwork.LocalPlayer.CustomProperties["shirtColorIndex"] = shirt.colorIndex;
		PhotonNetwork.LocalPlayer.CustomProperties["legsColorIndex"] = legs.colorIndex;



		/*if (GetComponent<PhotonView>() != null && GetComponent<PhotonView>().IsMine)
		{
			gameObject.GetComponent<PhotonView>().RPC("TrocaCabelo", RpcTarget.All, hair.propIndex);
			gameObject.GetComponent<PhotonView>().RPC("TrocaMaterialCabelo", RpcTarget.All, hair.colorIndex);
			gameObject.GetComponent<PhotonView>().RPC("TrocaCamisa", RpcTarget.All, shirt.propIndex);
			gameObject.GetComponent<PhotonView>().RPC("TrocaMaterialCamisa", RpcTarget.All, shirt.colorIndex);
			gameObject.GetComponent<PhotonView>().RPC("TrocaCalca", RpcTarget.All, legs.propIndex);
			gameObject.GetComponent<PhotonView>().RPC("TrocaMaterialCalca", RpcTarget.All, legs.colorIndex);
			
		}*/
		if (PhotonNetwork.InRoom)
		{
			TrocaCabelo((int)GetComponent<PhotonView>().Owner.CustomProperties["hairIndex"]);
			TrocaMaterialCabelo((int)GetComponent<PhotonView>().Owner.CustomProperties["hairColorIndex"]);
			TrocaCamisa((int)GetComponent<PhotonView>().Owner.CustomProperties["shirtIndex"]);
			TrocaMaterialCamisa((int)GetComponent<PhotonView>().Owner.CustomProperties["shirtColorIndex"]);
			TrocaCalca((int)GetComponent<PhotonView>().Owner.CustomProperties["legsIndex"]);
			TrocaMaterialCalca((int)GetComponent<PhotonView>().Owner.CustomProperties["legsColorIndex"]);
		}

		else if (!PhotonNetwork.InRoom)
		{
			TrocaCabelo(hair.propIndex);
			TrocaMaterialCabelo(hair.colorIndex);
			TrocaCamisa(shirt.propIndex);
			TrocaMaterialCamisa(shirt.colorIndex);
			TrocaCalca(legs.propIndex);
			TrocaMaterialCalca(legs.colorIndex);

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
            hairColor[i].material = hair.color.corData[onlineIndex];
        }

        hairColor[hair.propIndex].material = hair.color.corData[onlineIndex];
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
            shirtsColor[i].material = shirt.color.corData[onlineIndex];
        }

        shirtsColor[shirt.propIndex].material = shirt.color.corData[onlineIndex];
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
            legsColor[i].material = legs.color.corData[onlineIndex];
        }
        legsColor[legs.propIndex].material = legs.color.corData[onlineIndex];
    }
}
