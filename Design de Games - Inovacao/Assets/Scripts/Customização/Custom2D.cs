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

    public GameObject[] hairs;
    public GameObject[] shirt;
    public GameObject[] shorts;
    public GameObject[] shoes;

    public GameObject[] hairs2;
    public GameObject[] shirt2;
    public GameObject[] shirt2A;
    public GameObject[] shirt2B;
    public GameObject[] shorts2A;
    public GameObject[] shorts2B;
    public GameObject[] shoes2A;
    public GameObject[] shoes2B;


    void Awake()
    {

        for (int i = 0; i < hairs2.Length; i++)
        {
            hairs2[i].SetActive(false);
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

        for (int i = 0; i < shorts2A.Length; i++)
        {
            shorts2A[i].SetActive(false);
        }

        for (int i = 0; i < shorts2B.Length; i++)
        {
            shorts2B[i].SetActive(false);
        }

        for (int i = 0; i < shoes2A.Length; i++)
        {
            shoes2A[i].SetActive(false);
        }

        for (int i = 0; i < shoes2B.Length; i++)
        {
            shoes2B[i].SetActive(false);
        }

        for (int i = 0; i < hairs.Length; i++)
        {
            hairs[i].SetActive(false);
        }

        for (int i = 0; i < shirt.Length; i++)
        {
            shirt[i].SetActive(false);
        }

        for (int i = 0; i < shorts.Length; i++)
        {
            shorts[i].SetActive(false);
        }

        for (int i = 0; i < shoes.Length; i++)
        {
            shoes[i].SetActive(false);
        }

        hairs[hairInd.prop2DInd].SetActive(true);
        shirt[shirtInd.prop2DInd].SetActive(true);
        shorts[shortsInd.prop2DInd].SetActive(true);
        shoes[shoesInd.prop2DInd].SetActive(true);

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
        hairs2[hairInd.prop2DInd].SetActive(false);

        hairInd.prop2DInd += 1;

        if(hairInd.prop2DInd + 1 > hairs.Length )
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
        shirt2[shirtInd.prop2DInd].SetActive(false);
        shirt2A[shirtInd.prop2DInd].SetActive(false);
        shirt2B[shirtInd.prop2DInd].SetActive(false);

        shirtInd.prop2DInd += 1;

        if(shirtInd.prop2DInd + 1> shirt.Length )
        {
            shirtInd.prop2DInd = 0;
        }

        shirt[shirtInd.prop2DInd].SetActive(true);
        shirt2[shirtInd.prop2DInd].SetActive(true);
        shirt2A[shirtInd.prop2DInd].SetActive(true);
        shirt2B[shirtInd.prop2DInd].SetActive(true);

    }

    [PunRPC]
    public void ChangeShort()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        shorts[shortsInd.prop2DInd].SetActive(false);
        shorts2A[shortsInd.prop2DInd].SetActive(false);
        shorts2B[shortsInd.prop2DInd].SetActive(false);

        shortsInd.prop2DInd += 1;

        if(shortsInd.prop2DInd + 1> shorts.Length)
        {
            shortsInd.prop2DInd = 0;
        }

        shorts[shortsInd.prop2DInd].SetActive(true);
        shorts2A[shortsInd.prop2DInd].SetActive(true);
        shorts2B[shortsInd.prop2DInd].SetActive(true);

    }

    [PunRPC]
    public void ChangeShoes()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        shoes[shoesInd.prop2DInd].SetActive(false);
        shoes2A[shoesInd.prop2DInd].SetActive(false);
        shoes2B[shoesInd.prop2DInd].SetActive(false);

        shoesInd.prop2DInd += 1;

        if(shoesInd.prop2DInd + 1 > shoes.Length)
        {
            shoesInd.prop2DInd = 0;
        }

        shoes[shoesInd.prop2DInd].SetActive(true);
        shoes2A[shoesInd.prop2DInd].SetActive(true);
        shoes2B[shoesInd.prop2DInd].SetActive(true);

    }


}
