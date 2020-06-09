using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Custom2D : MonoBehaviour
{
    public Prop2D hairInd;
    public Prop2D shirtInd;
    public Prop2D shortsInd;
    public Prop2D shoesInd;
    public Prop2D oculosIndex;
    public Prop2D ciliosIndex;
    public Prop2D maskIndex;
    public Prop2D boneIndex;

    //ARMATURE DE FRENTE
    public ChangeMultipleCustom[] hairs;
    public ChangeMultipleCustom[] shirt;
    public ChangeMultipleCustom[] shorts;
    public ChangeMultipleCustom[] shoes;
    public ChangeMultipleCustom[] cilios;
    public ChangeMultipleCustom[] oculos;
    public ChangeMultipleCustom[] mask;
    public ChangeMultipleCustom[] bone;


    //ARMATURE DE LADO
    public ChangeMultipleCustom[] hairs2;
    public ChangeMultipleCustom[] shirt2;
    public ChangeMultipleCustom[] shorts2;
    public ChangeMultipleCustom[] shoes2;
    public ChangeMultipleCustom[] cilios2;
    public ChangeMultipleCustom[] oculos2;
    public ChangeMultipleCustom[] mask2;
    public ChangeMultipleCustom[] bone2;

    public BoolVariable boneOn;




    [PunRPC]
	public void ChangeHair(int index)
	{
		FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        if (boneOn == false)
        {
            hairs[hairInd.prop2DInd].ChangeCustom(false);
            hairs2[hairInd.prop2DInd].ChangeCustom(false);
            Debug.Log("Desativa cabelo");
        }
        else
        {
            bone[boneIndex.prop2DInd].ChangeCustom(false);
            bone2[boneIndex.prop2DInd].ChangeCustom(false);
        }
        boneOn.Value = false;

        hairInd.prop2DInd = index;

		if (hairInd.prop2DInd + 1 > hairs.Length)
		{
			hairInd.prop2DInd = 0;
		}
		hairs[hairInd.prop2DInd].ChangeCustom(true);
		hairs2[hairInd.prop2DInd].ChangeCustom(true);

		PhotonNetwork.LocalPlayer.CustomProperties["hairIndex"] = hairInd.prop2DInd;
    }

	

	[PunRPC]
    public void ChangeShirt(int index)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        shirt[shirtInd.prop2DInd].ChangeCustom(false);
        shirt2[shirtInd.prop2DInd].ChangeCustom(false);

        shirtInd.prop2DInd = index;

        if(shirtInd.prop2DInd >= shirt.Length )
        {
            shirtInd.prop2DInd = 0;
        }

        shirt[shirtInd.prop2DInd].ChangeCustom(true);
        shirt2[shirtInd.prop2DInd].ChangeCustom(true);

        PhotonNetwork.LocalPlayer.CustomProperties["shirtIndex"] = shirtInd.prop2DInd;

    }

    [PunRPC]
    public void ChangeShort(int index)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        shorts[shortsInd.prop2DInd].ChangeCustom(false);
        shorts2[shortsInd.prop2DInd].ChangeCustom(false);

        shortsInd.prop2DInd = index;

        if(shortsInd.prop2DInd >= shorts.Length)
        {
            shortsInd.prop2DInd = 0;
        }

        shorts[shortsInd.prop2DInd].ChangeCustom(true);
        shorts2[shortsInd.prop2DInd].ChangeCustom(true);

        PhotonNetwork.LocalPlayer.CustomProperties["legsIndex"] = shortsInd.prop2DInd;
    }

    [PunRPC]
    public void ChangeShoes(int index)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        shoes[shoesInd.prop2DInd].ChangeCustom(false);
        shoes2[shoesInd.prop2DInd].ChangeCustom(false);

        shoesInd.prop2DInd = index;

        if(shoesInd.prop2DInd >= shoes.Length)
        {
            shoesInd.prop2DInd = 0;
        }

        shoes[shoesInd.prop2DInd].ChangeCustom(true);
        shoes2[shoesInd.prop2DInd].ChangeCustom(true);

        PhotonNetwork.LocalPlayer.CustomProperties["shoeIndex"] = shoesInd.prop2DInd;

    }

    [PunRPC]
    public void ChangeOculos(int index)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        oculos[oculosIndex.prop2DInd].ChangeCustom(false);
        oculos2[oculosIndex.prop2DInd].ChangeCustom(false);

        oculosIndex.prop2DInd = index;

        if(oculosIndex.prop2DInd >= oculos.Length)
        {
            oculosIndex.prop2DInd = 0;
        }

        oculos[oculosIndex.prop2DInd].ChangeCustom(true);
        oculos2[oculosIndex.prop2DInd].ChangeCustom(true);

        PhotonNetwork.LocalPlayer.CustomProperties["oculosIndex"] = oculosIndex.prop2DInd;
    }


    [PunRPC]
    public void ChangeCilios(int index)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        cilios[ciliosIndex.prop2DInd].ChangeCustom(false);
        cilios2[ciliosIndex.prop2DInd].ChangeCustom(false);

        ciliosIndex.prop2DInd = index;

        if (ciliosIndex.prop2DInd >= cilios.Length)
        {
            ciliosIndex.prop2DInd = 0;
        }

        cilios[ciliosIndex.prop2DInd].ChangeCustom(true);
        cilios2[ciliosIndex.prop2DInd].ChangeCustom(true);

        PhotonNetwork.LocalPlayer.CustomProperties["ciliosIndex"] = ciliosIndex.prop2DInd;
    }


    [PunRPC]
    public void ChangeMask(int index)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        mask[maskIndex.prop2DInd].ChangeCustom(false);
        mask2[maskIndex.prop2DInd].ChangeCustom(false);

        maskIndex.prop2DInd = index;

        if (maskIndex.prop2DInd >= mask.Length)
        {
            maskIndex.prop2DInd = 0;
        }

        mask[maskIndex.prop2DInd].ChangeCustom(true);
        mask2[maskIndex.prop2DInd].ChangeCustom(true);

        PhotonNetwork.LocalPlayer.CustomProperties["maskIndex"] = maskIndex.prop2DInd;
    }


    [PunRPC]
    public void ChangeBone(int index)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        if (boneOn == false)
        {
            hairs[hairInd.prop2DInd].ChangeCustom(false);
            hairs2[hairInd.prop2DInd].ChangeCustom(false);
        }
        else
        {
            bone[boneIndex.prop2DInd].ChangeCustom(false);
            bone2[boneIndex.prop2DInd].ChangeCustom(false);
        }
        boneOn.Value = true;
        boneIndex.prop2DInd = index;

        if (boneIndex.prop2DInd >= bone.Length)
        {
            boneIndex.prop2DInd = 0;
        }

        bone[boneIndex.prop2DInd].ChangeCustom(true);
        bone2[boneIndex.prop2DInd].ChangeCustom(true);

        PhotonNetwork.LocalPlayer.CustomProperties["boneIndex"] = boneIndex.prop2DInd;
    }

}
