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

    //ARMATURE DE FRENTE
    public ChangeMultipleCustom[] hairs;
    public ChangeMultipleCustom[] shirt;
    public ChangeMultipleCustom[] shorts;
    public ChangeMultipleCustom[] shoes;


    //ARMATURE DE LADO
    public ChangeMultipleCustom[] hairs2;
    public ChangeMultipleCustom[] shirt2;
    public ChangeMultipleCustom[] shorts2;
    public ChangeMultipleCustom[] shoes2;
          



    void Start()
    {

        /*//CABELO E SOMBRANCELHAS

        for (int i = 0; i < hairs.Length; i++)
        {
            hairs[i].ChangeCustom(false);
        }


        for (int i = 0; i < hairs2.Length; i++)
        {
            hairs2[i].ChangeCustom(false);
        }


        // CAMISAS

        for (int i = 0; i < shirt.Length; i++)
        {
            shirt[i].ChangeCustom(false);
        }

        for (int i = 0; i < shirt2.Length; i++)
        {
            shirt2[i].ChangeCustom(false);
        }

        //SHORTS

        for (int i = 0; i < shorts.Length; i++)
        {
            shorts[i].ChangeCustom(false);
        }

        for (int i = 0; i < shorts2.Length; i++)
        {
            shorts2[i].ChangeCustom(false);
        }

        //SAPATOS

        for (int i = 0; i < shoes.Length; i++)
        {
            shoes[i].ChangeCustom(false);
        }

        for (int i = 0; i < shoes2.Length; i++)
        {
            shoes2[i].ChangeCustom(false);
        }


        //ATIVANDO OS CORRETOS

        hairs[hairInd.prop2DInd].ChangeCustom(true);
        shirt[shirtInd.prop2DInd].ChangeCustom(true);
        shirt[shirtInd.prop2DInd].ChangeCustom(true);
        shorts[shortsInd.prop2DInd].ChangeCustom(true);
        shoes[shoesInd.prop2DInd].ChangeCustom(true);

        hairs2[hairInd.prop2DInd].ChangeCustom(true);
        shirt2[shirtInd.prop2DInd].ChangeCustom(true);
        shirt2[shirtInd.prop2DInd].ChangeCustom(true);
        shorts2[shortsInd.prop2DInd].ChangeCustom(true);
        shoes2[shoesInd.prop2DInd].ChangeCustom(true);*/
    }

	[PunRPC]
	public void ChangeHair(int index)
	{
		FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
		hairs[hairInd.prop2DInd].ChangeCustom(false);
		hairs2[hairInd.prop2DInd].ChangeCustom(false);

		hairInd.prop2DInd = index;

		if (hairInd.prop2DInd + 1 > hairs.Length)
		{
			hairInd.prop2DInd = 0;
		}
        Debug.Log(hairInd.prop2DInd);
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
}
