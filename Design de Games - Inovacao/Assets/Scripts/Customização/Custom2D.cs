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
    public GameObject[] hairs;
    public GameObject[] eyebrowL;
    public GameObject[] eyebrowR;
    public GameObject[] shirt;
    public GameObject[] shirtA;
    public GameObject[] shirtB;
    public GameObject[] shortsA;
    public GameObject[] shortsB;
    public GameObject[] shortsC;
    public GameObject[] shoesA;
    public GameObject[] shoesB;


    //ARMATURE DE LADO
    public GameObject[] hairs2;
    public GameObject[] shirt2;
    public GameObject[] shirt2A;
    public GameObject[] shirt2B;
    public GameObject[] shorts2A;
    public GameObject[] shorts2B;
    public GameObject[] shoes2A;
    public GameObject[] shoes2B;
          



    void Start()
    {
        //CABELO E SOMBRANCELHAS

        for (int i = 0; i < hairs.Length; i++)
        {
            hairs[i].SetActive(false);
        }

        for (int i = 0; i < eyebrowL.Length; i++)
        {
            eyebrowL[i].SetActive(false);
        }

        for (int i = 0; i < eyebrowR.Length; i++)
        {
            eyebrowR[i].SetActive(false);
        }

        for (int i = 0; i < hairs2.Length; i++)
        {
            hairs2[i].SetActive(false);
        }


        // CAMISAS

        for (int i = 0; i < shirt.Length; i++)
        {
            shirt[i].SetActive(false);
        }

        for (int i = 0; i < shirtA.Length; i++)
        {
            shirtA[i].SetActive(false);
        }

        for (int i = 0; i < shirtB.Length; i++)
        {
            shirtB[i].SetActive(false);
        }

        for (int i = 0; i < shirt2.Length; i++)
        {
            shirt2[i].SetActive(false);
        }

        for (int i = 0; i < shirt2A.Length; i++)
        {
            shirt2A[i].SetActive(false);
        }

        for (int i = 0; i < shirt2B.Length; i++)
        {
            shirt2B[i].SetActive(false);
        }

        //SHORTS

        for (int i = 0; i < shortsA.Length; i++)
        {
            shortsA[i].SetActive(false);
        }

        for (int i = 0; i < shortsB.Length; i++)
        {
            shortsB[i].SetActive(false);
        }

        for (int i = 0; i < shortsC.Length; i++)
        {
            shortsC[i].SetActive(false);
        }

        for (int i = 0; i < shorts2A.Length; i++)
        {
            shorts2A[i].SetActive(false);
        }

        for (int i = 0; i < shorts2B.Length; i++)
        {
            shorts2B[i].SetActive(false);
        }

        //SAPATOS

        for (int i = 0; i < shoesA.Length; i++)
        {
            shoesA[i].SetActive(false);
        }

        for (int i = 0; i < shoesB.Length; i++)
        {
            shoesB[i].SetActive(false);
        }

        for (int i = 0; i < shoes2A.Length; i++)
        {
            shoes2A[i].SetActive(false);
        }

        for (int i = 0; i < shoes2B.Length; i++)
        {
            shoes2B[i].SetActive(false);
        }

        //ATIVANDO OS CORRETOS

        hairs[hairInd.prop2DInd].SetActive(true);
        eyebrowL[hairInd.prop2DInd].SetActive(true);
        eyebrowR[hairInd.prop2DInd].SetActive(true);
        shirt[shirtInd.prop2DInd].SetActive(true);
        shirtA[shirtInd.prop2DInd].SetActive(true);
        shirtB[shirtInd.prop2DInd].SetActive(true);
        shortsA[shortsInd.prop2DInd].SetActive(true);
        shortsB[shortsInd.prop2DInd].SetActive(true);
        shoesA[shoesInd.prop2DInd].SetActive(true);
        shoesB[shoesInd.prop2DInd].SetActive(true);

        hairs2[hairInd.prop2DInd].SetActive(true);
        shirt2[shirtInd.prop2DInd].SetActive(true);
        shirt2A[shirtInd.prop2DInd].SetActive(true);
        shirt2B[shirtInd.prop2DInd].SetActive(true);
        shorts2A[shortsInd.prop2DInd].SetActive(true);
        shorts2B[shortsInd.prop2DInd].SetActive(true);
        shoes2A[shoesInd.prop2DInd].SetActive(true);
        shoes2B[shoesInd.prop2DInd].SetActive(true);
    }

	[PunRPC]
	public void ChangeHair()
    {        
		FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
		hairs[hairInd.prop2DInd].SetActive(false);
        eyebrowL[hairInd.prop2DInd].SetActive(false);
        eyebrowR[hairInd.prop2DInd].SetActive(false);
        hairs2[hairInd.prop2DInd].SetActive(false);

        hairInd.prop2DInd += 1;

		if (hairInd.prop2DInd >= hairs.Length)
		{
			hairInd.prop2DInd = 0;
		}

		hairs[hairInd.prop2DInd].SetActive(true);
        eyebrowL[hairInd.prop2DInd].SetActive(true);
        eyebrowR[hairInd.prop2DInd].SetActive(true);
		hairs2[hairInd.prop2DInd].SetActive(true);
    }

	[PunRPC]
	public void ChangeSpecificHair(int index)
	{
		FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
		hairs[hairInd.prop2DInd].SetActive(false);
		hairs2[hairInd.prop2DInd].SetActive(false);

		hairInd.prop2DInd = index;

		if (hairInd.prop2DInd + 1 > hairs.Length)
		{
			hairInd.prop2DInd = 0;
		}

		hairs[hairInd.prop2DInd].SetActive(true);
		hairs2[hairInd.prop2DInd].SetActive(true);
	}

	[PunRPC]
    public void ChangeShirt()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        shirt[shirtInd.prop2DInd].SetActive(false);
        shirtA[shirtInd.prop2DInd].SetActive(false);
        shirtB[shirtInd.prop2DInd].SetActive(false);
        shirt2[shirtInd.prop2DInd].SetActive(false);
        shirt2A[shirtInd.prop2DInd].SetActive(false);
        shirt2B[shirtInd.prop2DInd].SetActive(false);

        shirtInd.prop2DInd += 1;

        if(shirtInd.prop2DInd >= shirt.Length )
        {
            shirtInd.prop2DInd = 0;
        }

        shirt[shirtInd.prop2DInd].SetActive(true);
        shirtA[shirtInd.prop2DInd].SetActive(true);
        shirtB[shirtInd.prop2DInd].SetActive(true);
        shirt2[shirtInd.prop2DInd].SetActive(true);
        shirt2A[shirtInd.prop2DInd].SetActive(true);
        shirt2B[shirtInd.prop2DInd].SetActive(true);

    }

    [PunRPC]
    public void ChangeShort()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        shortsA[shortsInd.prop2DInd].SetActive(false);
        shortsB[shortsInd.prop2DInd].SetActive(false);
        shorts2A[shortsInd.prop2DInd].SetActive(false);
        shorts2B[shortsInd.prop2DInd].SetActive(false);

        shortsInd.prop2DInd += 1;

        if(shortsInd.prop2DInd >= shortsA.Length)
        {
            shortsInd.prop2DInd = 0;
        }

        shortsA[shortsInd.prop2DInd].SetActive(true);
        shortsB[shortsInd.prop2DInd].SetActive(true);
        shorts2A[shortsInd.prop2DInd].SetActive(true);
        shorts2B[shortsInd.prop2DInd].SetActive(true);

    }

    [PunRPC]
    public void ChangeShoes()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        shoesA[shoesInd.prop2DInd].SetActive(false);
        shoesB[shoesInd.prop2DInd].SetActive(false);
        shoes2A[shoesInd.prop2DInd].SetActive(false);
        shoes2B[shoesInd.prop2DInd].SetActive(false);

        shoesInd.prop2DInd += 1;

        if(shoesInd.prop2DInd >= shoesA.Length)
        {
            shoesInd.prop2DInd = 0;
        }

        shoesA[shoesInd.prop2DInd].SetActive(true);
        shoesB[shoesInd.prop2DInd].SetActive(true);
        shoes2A[shoesInd.prop2DInd].SetActive(true);
        shoes2B[shoesInd.prop2DInd].SetActive(true);

    }


}
