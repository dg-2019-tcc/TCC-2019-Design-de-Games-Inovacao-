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
    public GameObject[] shorts2;
    public GameObject[] shoes2;


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

        for (int i = 0; i < shorts2.Length; i++)
        {
            shorts2[i].SetActive(false);
        }

        for (int i = 0; i < shoes2.Length; i++)
        {
            shoes2[i].SetActive(false);
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
        shorts2[shortsInd.prop2DInd].SetActive(true);
        shoes2[shoesInd.prop2DInd].SetActive(true);
    }

    [PunRPC]
    public void ChangeHair()
    {
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
        shirt[shirtInd.prop2DInd].SetActive(false);
        shirt2[shirtInd.prop2DInd].SetActive(false);

        shirtInd.prop2DInd += 1;

        if(shirtInd.prop2DInd + 1> shirt.Length )
        {
            shirtInd.prop2DInd = 0;
        }

        shirt[shirtInd.prop2DInd].SetActive(true);
        shirt2[shirtInd.prop2DInd].SetActive(true);

    }

    [PunRPC]
    public void ChangeShort()
    {
        shorts[shortsInd.prop2DInd].SetActive(false);

        shortsInd.prop2DInd += 1;

        if(shortsInd.prop2DInd + 1> shorts.Length)
        {
            shortsInd.prop2DInd = 0;
        }

        shorts[shortsInd.prop2DInd].SetActive(true);
        shorts2[shortsInd.prop2DInd].SetActive(true);

    }

    [PunRPC]
    public void ChangeShoes()
    {
        shoes[shoesInd.prop2DInd].SetActive(false);

        shoesInd.prop2DInd += 1;

        if(shoesInd.prop2DInd + 1 > shoes.Length)
        {
            shoesInd.prop2DInd = 0;
        }

        shoes[shoesInd.prop2DInd].SetActive(true);
        shoes2[shoesInd.prop2DInd].SetActive(true);

    }


}
