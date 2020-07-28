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
    public Prop2D skin;
    public Prop2D pupila;
    public Prop2D sobrancelha;

    public ChangeMultipleCustom[] hairModels;
    public ChangeMultipleCustom[] shirtModels;
    public ChangeMultipleCustom[] legModels;
    public ChangeMultipleCustom[] shoeModels;
    public ChangeMultipleCustom[] oculosModels;
    public ChangeMultipleCustom[] ciliosModels;
    public ChangeMultipleCustom[] maskModels;
    public ChangeMultipleCustom[] boneModels;
    public ChangeMultipleCustom[] skinModels;
    public ChangeMultipleCustom[] pupilaModels;
    public ChangeMultipleCustom[] sobrancelhaModels;

    public ChangeMultipleCustom[] hair2Models;
    public ChangeMultipleCustom[] shirt2Models;
    public ChangeMultipleCustom[] leg2Models;
    public ChangeMultipleCustom[] shoe2Models;
    public ChangeMultipleCustom[] oculos2Models;
    public ChangeMultipleCustom[] cilios2Models;
    public ChangeMultipleCustom[] mask2Models;
    public ChangeMultipleCustom[] bone2Models;
    public ChangeMultipleCustom[] skin2Models;
    public ChangeMultipleCustom[] pupila2Models;
    public ChangeMultipleCustom[] sobrancelha2EsqModels;
    public ChangeMultipleCustom[] sobrancelha2DirModels;


    private PhotonView pv;


    public void AtivaRoupas()
    {
        pv = GetComponent<PhotonView>();

        PhotonNetwork.LocalPlayer.CustomProperties["hairIndex"] = hair.prop2DInd;
        PhotonNetwork.LocalPlayer.CustomProperties["shirtIndex"] = shirt.prop2DInd;
        PhotonNetwork.LocalPlayer.CustomProperties["legsIndex"] = legs.prop2DInd;
        PhotonNetwork.LocalPlayer.CustomProperties["shoeIndex"] = shoe.prop2DInd;
        PhotonNetwork.LocalPlayer.CustomProperties["oculosIndex"] = oculos.prop2DInd;
        PhotonNetwork.LocalPlayer.CustomProperties["ciliosIndex"] = cilios.prop2DInd;
        PhotonNetwork.LocalPlayer.CustomProperties["maskIndex"] = mask.prop2DInd;
        PhotonNetwork.LocalPlayer.CustomProperties["boneIndex"] = bone.prop2DInd;
        PhotonNetwork.LocalPlayer.CustomProperties["skinIndex"] = skin.prop2DInd;
        PhotonNetwork.LocalPlayer.CustomProperties["pupilaIndex"] = pupila.prop2DInd;
        PhotonNetwork.LocalPlayer.CustomProperties["sobrancelhaIndex"] = sobrancelha.prop2DInd;

        //hair.prop2DInd = 1;//utilizei pra resetar os valores do cabelo
        if (GameManager.inRoom == false)
        {
            TrocaOffline();
        }

        else
        {
            if (pv.IsMine)
            {
                //TrocaPvIsMine();
                TrocaOffline();
            }

            else
            {
                TrocaOtherPv();
            }
        }
    }

    void TrocaOtherPv()
    {
        TrocaCabelo((int)pv.Owner.CustomProperties["hairIndex"]);
        TrocaCamisa((int)pv.Owner.CustomProperties["shirtIndex"]);
        TrocaCalca((int)pv.Owner.CustomProperties["legsIndex"]);
        TrocaSapato((int)pv.Owner.CustomProperties["shoeIndex"]);
        TrocaOculos((int)pv.Owner.CustomProperties["oculosIndex"]);
        TrocaCilios((int)pv.Owner.CustomProperties["ciliosIndex"]);
        TrocaMask((int)pv.Owner.CustomProperties["maskIndex"]);
        TrocaBone((int)pv.Owner.CustomProperties["boneIndex"]);
        TrocaSkin((int)pv.Owner.CustomProperties["skinIndex"]);
        TrocaPupila((int)pv.Owner.CustomProperties["pupilaIndex"]);
        TrocaSobrancelha((int)pv.Owner.CustomProperties["sobrancelhaIndex"]);
    }

    void TrocaPvIsMine()
    {
        TrocaCabelo((int)PhotonNetwork.LocalPlayer.CustomProperties["hairIndex"]);
        TrocaCamisa((int)PhotonNetwork.LocalPlayer.CustomProperties["shirtIndex"]);
        TrocaCalca((int)PhotonNetwork.LocalPlayer.CustomProperties["legsIndex"]);
        TrocaSapato((int)PhotonNetwork.LocalPlayer.CustomProperties["shoeIndex"]);
        TrocaOculos((int)PhotonNetwork.LocalPlayer.CustomProperties["oculosIndex"]);
        TrocaCilios((int)PhotonNetwork.LocalPlayer.CustomProperties["ciliosIndex"]);
        TrocaMask((int)PhotonNetwork.LocalPlayer.CustomProperties["maskIndex"]);
        TrocaBone((int)PhotonNetwork.LocalPlayer.CustomProperties["boneIndex"]);
        TrocaSkin((int)PhotonNetwork.LocalPlayer.CustomProperties["skinIndex"]);
        TrocaPupila((int)PhotonNetwork.LocalPlayer.CustomProperties["pupilaIndex"]);
        TrocaSobrancelha((int)PhotonNetwork.LocalPlayer.CustomProperties["sobrancelhaIndex"]);
    }

    void TrocaOffline()
    {
        TrocaCabelo(hair.prop2DInd);
        TrocaCamisa(shirt.prop2DInd);
        TrocaCalca(legs.prop2DInd);
        TrocaSapato(shoe.prop2DInd);
        TrocaOculos(oculos.prop2DInd);
        TrocaCilios(cilios.prop2DInd);
        TrocaMask(mask.prop2DInd);
        TrocaBone(bone.prop2DInd);
        TrocaSkin(skin.prop2DInd);
        TrocaPupila(pupila.prop2DInd);
        TrocaSobrancelha(sobrancelha.prop2DInd);
        Debug.Log("TrocaOffline");
    }
    void TrocaOnline()
    {
        TrocaCabelo((int)pv.Owner.CustomProperties["hairIndex"]);
        TrocaCamisa((int)pv.Owner.CustomProperties["shirtIndex"]);
        TrocaCalca((int)pv.Owner.CustomProperties["legsIndex"]);
        TrocaSapato((int)pv.Owner.CustomProperties["shoeIndex"]);
        TrocaOculos((int)pv.Owner.CustomProperties["oculosIndex"]);
        TrocaCilios((int)pv.Owner.CustomProperties["ciliosIndex"]);
        TrocaMask((int)pv.Owner.CustomProperties["maskIndex"]);
        TrocaBone((int)pv.Owner.CustomProperties["boneIndex"]);
        TrocaSkin((int)pv.Owner.CustomProperties["skinIndex"]);
        TrocaPupila((int)pv.Owner.CustomProperties["pupilaIndex"]);
        TrocaSobrancelha((int)pv.Owner.CustomProperties["sobrancelhaIndex"]);
        Debug.Log(pv.Owner.CustomProperties["hairIndex"]);
        Debug.Log(pv.Owner.CustomProperties["shirtIndex"]);
        Debug.Log(pv.Owner.CustomProperties["legsIndex"]);
        Debug.Log("TrocaOnline");
    }


    [PunRPC]
    private void TrocaSkin(int onlineIndex)
    {
        for (int i = 0; i < skinModels.Length; i++)
        {
            skinModels[i].ChangeCustom(false);
            skin2Models[i].ChangeCustom(false);
        }
        skinModels[onlineIndex].ChangeCustom(true);
        skin2Models[onlineIndex].ChangeCustom(true);
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
        //if (onlineIndex <= oculos2Models.Length)
        //{
        oculosModels[onlineIndex].ChangeCustom(true);
        oculos2Models[onlineIndex].ChangeCustom(true);
        //}
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

        //if (onlineIndex <= mask2Models.Length)
        //{
        maskModels[onlineIndex].ChangeCustom(true);
        mask2Models[onlineIndex].ChangeCustom(true);
        //}
    }

    [PunRPC]
    private void TrocaBone(int onlineIndex)
    {
        for (int i = 0; i < boneModels.Length; i++)
        {
            boneModels[i].ChangeCustom(false);
            bone2Models[i].ChangeCustom(false);
        }

        boneModels[onlineIndex].ChangeCustom(true);
        bone2Models[onlineIndex].ChangeCustom(true);
    }


    [PunRPC]
    private void TrocaPupila(int onlineIndex)
    {
        for (int i = 0; i < pupilaModels.Length; i++)
        {
            pupilaModels[i].ChangeCustom(false);
            pupila2Models[i].ChangeCustom(false);
        }
        pupilaModels[onlineIndex].ChangeCustom(true);
        pupila2Models[onlineIndex].ChangeCustom(true);
    }


    [PunRPC]
    private void TrocaSobrancelha(int onlineIndex)
    {
        for (int i = 0; i < 3; i++)
        {
            sobrancelhaModels[i].ChangeCustom(false);
            sobrancelha2EsqModels[i].ChangeCustom(false);
            sobrancelha2DirModels[i].ChangeCustom(false);
        }

        sobrancelhaModels[onlineIndex].ChangeCustom(true);
        sobrancelha2EsqModels[onlineIndex].ChangeCustom(true);
        sobrancelha2DirModels[onlineIndex].ChangeCustom(true);
    }
}
