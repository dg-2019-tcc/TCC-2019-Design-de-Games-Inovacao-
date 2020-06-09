using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDisplay : MonoBehaviour
{
    public Prop2D legs;
    public Prop2D shirt;
    public Prop2D hair;
    public Prop2D shoe;
    public Prop2D oculos;
    public Prop2D cilios;
    public Prop2D mask;
    public Prop2D bone;

    public ChangeMultipleCustom[] hairModels;
    public ChangeMultipleCustom[] shirtModels;
    public ChangeMultipleCustom[] legModels;
    public ChangeMultipleCustom[] shoeModels;
    public ChangeMultipleCustom[] oculosModels;
    public ChangeMultipleCustom[] ciliosModels;
    public ChangeMultipleCustom[] maskModels;
    public ChangeMultipleCustom[] boneModels;

    public ChangeMultipleCustom[] hair2Models;
    public ChangeMultipleCustom[] shirt2Models;
    public ChangeMultipleCustom[] leg2Models;
    public ChangeMultipleCustom[] shoe2Models;
    public ChangeMultipleCustom[] oculos2Models;
    public ChangeMultipleCustom[] cilios2Models;
    public ChangeMultipleCustom[] mask2Models;
    public ChangeMultipleCustom[] bone2Models;


    private PhotonView pv;

    void Start()
    {
		pv = GetComponent<PhotonView>();

        //hair.prop2DInd = 1;//utilizei pra resetar os valores do cabelo

        PhotonNetwork.LocalPlayer.CustomProperties["hairIndex"] = hair.prop2DInd;
        PhotonNetwork.LocalPlayer.CustomProperties["shirtIndex"] = shirt.prop2DInd;
        PhotonNetwork.LocalPlayer.CustomProperties["legsIndex"] = legs.prop2DInd;
        PhotonNetwork.LocalPlayer.CustomProperties["shoeIndex"] = shoe.prop2DInd;
        PhotonNetwork.LocalPlayer.CustomProperties["oculosIndex"] = oculos.prop2DInd;
        PhotonNetwork.LocalPlayer.CustomProperties["ciliosIndex"] = cilios.prop2DInd;
        PhotonNetwork.LocalPlayer.CustomProperties["maskIndex"] = mask.prop2DInd;
        //PhotonNetwork.LocalPlayer.CustomProperties["boneIndex"] = bone.prop2DInd;



        if (!PhotonNetwork.InRoom)
		{
			TrocaCabelo(hair.prop2DInd);
			//TrocaMaterialCabelo(hair.colorIndex);
			TrocaCamisa(shirt.prop2DInd);
			//TrocaMaterialCamisa(shirt.colorIndex);
			TrocaCalca(legs.prop2DInd);
			//TrocaMaterialCalca(legs.colorIndex);
			TrocaSapato(shoe.prop2DInd);
			TrocaOculos(oculos.prop2DInd);
			TrocaCilios(cilios.prop2DInd);
            TrocaMask(mask.prop2DInd);
            //TrocaMask(bone.prop2DInd);
            //TrocaMaterialSapato(shoe.colorIndex);

        }

        else
        {
            TrocaCabelo((int)pv.Owner.CustomProperties["hairIndex"]);
            //TrocaMaterialCabelo((int)pv.Owner.CustomProperties["hairColorIndex"]);
            TrocaCamisa((int)pv.Owner.CustomProperties["shirtIndex"]);
            //TrocaMaterialCamisa((int)pv.Owner.CustomProperties["shirtColorIndex"]);
            TrocaCalca((int)pv.Owner.CustomProperties["legsIndex"]);
            //TrocaMaterialCalca((int)pv.Owner.CustomProperties["legsColorIndex"]);
            TrocaSapato((int)pv.Owner.CustomProperties["shoeIndex"]);
            TrocaOculos((int)pv.Owner.CustomProperties["oculosIndex"]);
            TrocaCilios((int)pv.Owner.CustomProperties["ciliosIndex"]);
            TrocaMask((int)pv.Owner.CustomProperties["maskIndex"]);
            //TrocaMask((int)pv.Owner.CustomProperties["boneIndex"]);
            //TrocaMaterialSapato((int)pv.Owner.CustomProperties["shoeColorIndex"]);


        }
    }


    [PunRPC]
    private void TrocaCabelo(int onlineIndex)
    {
        for (int i = 0; i < hairModels.Length; i++)
        {
            hairModels[i].ChangeCustom(false);
            hair2Models[i].ChangeCustom(false);
        }
        hairModels[onlineIndex].ChangeCustom(true);
        hair2Models[onlineIndex].ChangeCustom(true);
    }



    [PunRPC]
    private void TrocaCamisa(int onlineIndex)
    {
        for (int i = 0; i < shirtModels.Length; i++)
        {
            shirtModels[i].ChangeCustom(false);
            shirt2Models[i].ChangeCustom(false);
        }
        shirtModels[onlineIndex].ChangeCustom(true);
        shirt2Models[onlineIndex].ChangeCustom(true);
    }



    [PunRPC]
    private void TrocaCalca(int onlineIndex)
    {
        for (int i = 0; i < legModels.Length; i++)
        {
            legModels[i].ChangeCustom(false);
            leg2Models[i].ChangeCustom(false);
        }
        legModels[onlineIndex].ChangeCustom(true);
        leg2Models[onlineIndex].ChangeCustom(true);
    }



    [PunRPC]
    private void TrocaSapato(int onlineIndex)
    {
        for (int i = 0; i < shoeModels.Length; i++)
        {
            shoeModels[i].ChangeCustom(false);
            shoe2Models[i].ChangeCustom(false);
        }
        shoeModels[onlineIndex].ChangeCustom(true);
        shoe2Models[onlineIndex].ChangeCustom(true);
    }

    [PunRPC]
    private void TrocaOculos(int onlineIndex)
    {
        for (int i = 0; i < oculosModels.Length; i++)
        {
            oculosModels[i].ChangeCustom(false);
            oculos2Models[i].ChangeCustom(false);
        }
        if (onlineIndex <= oculos2Models.Length)
        {
            oculosModels[onlineIndex].ChangeCustom(true);
            oculos2Models[onlineIndex].ChangeCustom(true);
        }
    }

    [PunRPC]
    private void TrocaCilios(int onlineIndex)
    {
        for (int i = 0; i < ciliosModels.Length; i++)
        {
            ciliosModels[i].ChangeCustom(false);
            cilios2Models[i].ChangeCustom(false);
        }/*
        ciliosModels[onlineIndex].ChangeCustom(true);
        cilios2Models[onlineIndex].ChangeCustom(true);*/
    }

    [PunRPC]
    private void TrocaMask(int onlineIndex)
    {
        for (int i = 0; i < maskModels.Length; i++)
        {
            maskModels[i].ChangeCustom(false);
            mask2Models[i].ChangeCustom(false);
        }

        if (onlineIndex <= mask2Models.Length)
        {
            maskModels[onlineIndex].ChangeCustom(true);
            mask2Models[onlineIndex].ChangeCustom(true);
        }
    }

    [PunRPC]
    private void TrocaBone(int onlineIndex)
    {
        for (int i = 0; i < maskModels.Length; i++)
        {
            boneModels[i].ChangeCustom(false);
            bone2Models[i].ChangeCustom(false);
        }

        if (onlineIndex <= mask2Models.Length)
        {
            boneModels[onlineIndex].ChangeCustom(true);
            bone2Models[onlineIndex].ChangeCustom(true);
        }
    }

    }
