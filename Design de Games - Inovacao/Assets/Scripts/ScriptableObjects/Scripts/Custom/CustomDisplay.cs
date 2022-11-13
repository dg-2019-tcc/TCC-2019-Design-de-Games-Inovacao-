using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDisplay : MonoBehaviour
{
    public static CustomDisplay current;

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
    public CustomController customController;

    #region Unity Function

    private void Awake()
    {
        current = this;
        pv = GetComponent<PhotonView>();
        customController = GetComponent<CustomController>();
    }

    #endregion

    #region Public Functions

    public void AtivaRoupas()
    {
        pv = GetComponent<PhotonView>();
        customController = GetComponent<CustomController>();

        PlayerPrefsManager.Instance.LoadPlayerPref("Customização");

        PhotonNetwork.LocalPlayer.CustomProperties["hairIndex"] = PlayerPrefsManager.Instance.prefsVariables.hairIndex;
        PhotonNetwork.LocalPlayer.CustomProperties["shirtIndex"] = PlayerPrefsManager.Instance.prefsVariables.shirtIndex;
        PhotonNetwork.LocalPlayer.CustomProperties["legsIndex"] = PlayerPrefsManager.Instance.prefsVariables.legsIndex;
        PhotonNetwork.LocalPlayer.CustomProperties["shoeIndex"] = PlayerPrefsManager.Instance.prefsVariables.shoeIndex;
        PhotonNetwork.LocalPlayer.CustomProperties["oculosIndex"] = PlayerPrefsManager.Instance.prefsVariables.oculosIndex;
        PhotonNetwork.LocalPlayer.CustomProperties["ciliosIndex"] = PlayerPrefsManager.Instance.prefsVariables.ciliosIndex;
        PhotonNetwork.LocalPlayer.CustomProperties["maskIndex"] = PlayerPrefsManager.Instance.prefsVariables.maskIndex;
        PhotonNetwork.LocalPlayer.CustomProperties["boneIndex"] = PlayerPrefsManager.Instance.prefsVariables.boneIndex;
        PhotonNetwork.LocalPlayer.CustomProperties["skinIndex"] = PlayerPrefsManager.Instance.prefsVariables.skinIndex;
        PhotonNetwork.LocalPlayer.CustomProperties["pupilaIndex"] = PlayerPrefsManager.Instance.prefsVariables.pupilaIndex;
        PhotonNetwork.LocalPlayer.CustomProperties["sobrancelhaIndex"] = PlayerPrefsManager.Instance.prefsVariables.sombrancelhaIndex;


        //hair.prop2DInd = 1;//utilizei pra resetar os valores do cabelo
        if (GameManager.inRoom == false)
        {
            TrocaOffline();
        }

        else
        {
            if (pv.IsMine)
            {
                TrocaOffline();
            }

            else
            {
                TrocaOtherPv();
            }
        }

        customController.TiraCustomDesativada();
    }

    public void AtivaTudo()
    {
        skinModels[PlayerPrefsManager.Instance.prefsVariables.skinIndex].ChangeCustom(true);
        skin2Models[PlayerPrefsManager.Instance.prefsVariables.skinIndex].ChangeCustom(true);
        hairModels[PlayerPrefsManager.Instance.prefsVariables.hairIndex].ChangeCustom(true);
        hair2Models[PlayerPrefsManager.Instance.prefsVariables.hairIndex].ChangeCustom(true);
        shirtModels[PlayerPrefsManager.Instance.prefsVariables.shirtIndex].ChangeCustom(true);
        shirt2Models[PlayerPrefsManager.Instance.prefsVariables.shirtIndex].ChangeCustom(true);
        legModels[PlayerPrefsManager.Instance.prefsVariables.legsIndex].ChangeCustom(true);
        leg2Models[PlayerPrefsManager.Instance.prefsVariables.legsIndex].ChangeCustom(true);
        shoeModels[PlayerPrefsManager.Instance.prefsVariables.shoeIndex].ChangeCustom(true);
        shoe2Models[PlayerPrefsManager.Instance.prefsVariables.shoeIndex].ChangeCustom(true);
        oculosModels[PlayerPrefsManager.Instance.prefsVariables.oculosIndex].ChangeCustom(true);
        oculos2Models[PlayerPrefsManager.Instance.prefsVariables.oculosIndex].ChangeCustom(true);
        maskModels[PlayerPrefsManager.Instance.prefsVariables.maskIndex].ChangeCustom(true);
        mask2Models[PlayerPrefsManager.Instance.prefsVariables.maskIndex].ChangeCustom(true);
        boneModels[PlayerPrefsManager.Instance.prefsVariables.boneIndex].ChangeCustom(true);
        bone2Models[PlayerPrefsManager.Instance.prefsVariables.boneIndex].ChangeCustom(true);
        pupilaModels[PlayerPrefsManager.Instance.prefsVariables.sombrancelhaIndex].ChangeCustom(true);
        pupila2Models[PlayerPrefsManager.Instance.prefsVariables.sombrancelhaIndex].ChangeCustom(true);
        sobrancelhaModels[PlayerPrefsManager.Instance.prefsVariables.sombrancelhaIndex].ChangeCustom(true);
        sobrancelha2EsqModels[PlayerPrefsManager.Instance.prefsVariables.sombrancelhaIndex].ChangeCustom(true);
        sobrancelha2DirModels[PlayerPrefsManager.Instance.prefsVariables.sombrancelhaIndex].ChangeCustom(true);

        customController = GetComponent<CustomController>();
        customController.TiraCustomDesativada();

        Debug.Log("[CustomDisplay] AtivaTudo()");
    }

    public void DesativaTudo()
    {

        for (int i = 0; i < skinModels.Length; i++)
        {
            if (i != PlayerPrefsManager.Instance.prefsVariables.skinIndex)
            {
                skinModels[i].ChangeCustom(false);
                skin2Models[i].ChangeCustom(false);
            }
        }

        for (int i = 0; i < hairModels.Length; i++)
        {
            hairModels[i].ChangeCustom(false);
            hair2Models[i].ChangeCustom(false);
        }

        hairModels[PlayerPrefsManager.Instance.prefsVariables.hairIndex].ChangeCustom(true);
        hair2Models[PlayerPrefsManager.Instance.prefsVariables.hairIndex].ChangeCustom(true);

        for (int i = 0; i < shirtModels.Length; i++)
        {
            if (i != PlayerPrefsManager.Instance.prefsVariables.shirtIndex)
            {
                shirtModels[i].ChangeCustom(false);
                shirt2Models[i].ChangeCustom(false);
            }
        }

        for (int i = 0; i < legModels.Length; i++)
        {
            if (i != PlayerPrefsManager.Instance.prefsVariables.legsIndex)
            {
                legModels[i].ChangeCustom(false);
                leg2Models[i].ChangeCustom(false);
            }
        }

        for (int i = 0; i < shoeModels.Length; i++)
        {
            if (i != PlayerPrefsManager.Instance.prefsVariables.shoeIndex)
            {
                shoeModels[i].ChangeCustom(false);
                shoe2Models[i].ChangeCustom(false);
            }
        }

        for (int i = 0; i < oculosModels.Length; i++)
        {
            if (i != PlayerPrefsManager.Instance.prefsVariables.oculosIndex)
            {
                oculosModels[i].ChangeCustom(false);
                oculos2Models[i].ChangeCustom(false);
            }
        }

        for (int i = 0; i < ciliosModels.Length; i++)
        {
            if (i != PlayerPrefsManager.Instance.prefsVariables.ciliosIndex)
            {
                ciliosModels[i].ChangeCustom(false);
                cilios2Models[i].ChangeCustom(false);
            }
        }

        for (int i = 0; i < maskModels.Length; i++)
        {
            if (i != PlayerPrefsManager.Instance.prefsVariables.maskIndex)
            {
                maskModels[i].ChangeCustom(false);
                mask2Models[i].ChangeCustom(false);
            }
        }

        for (int i = 0; i < boneModels.Length; i++)
        {
            if (i != PlayerPrefsManager.Instance.prefsVariables.boneIndex)
            {
                boneModels[i].ChangeCustom(false);
                bone2Models[i].ChangeCustom(false);
            }
        }

        for (int i = 0; i < pupilaModels.Length; i++)
        {
            if (i != PlayerPrefsManager.Instance.prefsVariables.pupilaIndex)
            {
                pupilaModels[i].ChangeCustom(false);
                pupila2Models[i].ChangeCustom(false);
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (i != PlayerPrefsManager.Instance.prefsVariables.sombrancelhaIndex)
            {
                sobrancelhaModels[i].ChangeCustom(false);
                sobrancelha2EsqModels[i].ChangeCustom(false);
                sobrancelha2DirModels[i].ChangeCustom(false);
            }
        }

        if (customController == null) { customController = GetComponent<CustomController>(); }

        customController.TiraCustomDesativada();
        
        //AtivaTudo();
    }

    #endregion

    #region Private Functions

    private void TrocaOtherPv()
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

    private void TrocaOffline()
    {
        TrocaCabelo(PlayerPrefsManager.Instance.prefsVariables.hairIndex);
        TrocaCamisa(PlayerPrefsManager.Instance.prefsVariables.shirtIndex);
        TrocaCalca(PlayerPrefsManager.Instance.prefsVariables.legsIndex);
        TrocaSapato(PlayerPrefsManager.Instance.prefsVariables.shoeIndex);
        TrocaOculos(PlayerPrefsManager.Instance.prefsVariables.oculosIndex);
        TrocaCilios(PlayerPrefsManager.Instance.prefsVariables.ciliosIndex);
        TrocaMask(PlayerPrefsManager.Instance.prefsVariables.maskIndex);
        TrocaBone(PlayerPrefsManager.Instance.prefsVariables.boneIndex);
        TrocaSkin(PlayerPrefsManager.Instance.prefsVariables.skinIndex);
        TrocaPupila(PlayerPrefsManager.Instance.prefsVariables.pupilaIndex);
        TrocaSobrancelha(PlayerPrefsManager.Instance.prefsVariables.sombrancelhaIndex);
        Debug.Log("[CustomDisplay] TrocaOffline");
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

    #endregion
}
