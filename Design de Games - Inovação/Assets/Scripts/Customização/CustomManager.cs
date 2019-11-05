using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomManager : MonoBehaviour
{


    [Header("Modelos")]

    public GameObject[] hairModels;
    public GameObject[] shirtModels;
    public GameObject[] pantModels;



    [Header("Materiais")]

    public SkinnedMeshRenderer[] hairColor;
    public SkinnedMeshRenderer[] legsColor;
    public SkinnedMeshRenderer[] shirtsColor;



    [Header("PropCustom")]

    public PropsCustom hair;
    public PropsCustom shirt;
    public PropsCustom legs;



    [Header("Scripts Externos")]

    public PlayerMovement playerScript;



    [Header("Som")]

    public AudioSource som;



    private void Start()
    {
        /*
        hairIndex = 0;
        shirtIndex = 0;
        legsIndex = 0;
        */

       /*//PlayerPrefs.SetInt("hairIndex", hairIndex);
        hair.propIndex = PlayerPrefs.GetInt("hairIndex");
        PhotonNetwork.LocalPlayer.CustomProperties["hairIndex"] = hair.propIndex;

        //PlayerPrefs.SetInt("shirtIndex", shirtIndex);
        shirt.propIndex = PlayerPrefs.GetInt("shirtIndex");
        PhotonNetwork.LocalPlayer.CustomProperties["shirtIndex"] = shirt.propIndex;

        //PlayerPrefs.SetInt("legsIndex", legsIndex);
        legs.propIndex = PlayerPrefs.GetInt("legsIndex");
        PhotonNetwork.LocalPlayer.CustomProperties["legsIndex"] = legs.propIndex;

		if (GetComponent<PhotonView>() != null && GetComponent<PhotonView>().IsMine)
		{
			gameObject.GetComponent<PhotonView>().RPC("TrocaCabelo", RpcTarget.All, hair.propIndex);
            gameObject.GetComponent<PhotonView>().RPC("TrocaMaterialCabelo", RpcTarget.All, hair.colorIndex);
            gameObject.GetComponent<PhotonView>().RPC("TrocaCamisa", RpcTarget.All, shirt.propIndex);
			gameObject.GetComponent<PhotonView>().RPC("TrocaMaterialCamisa", RpcTarget.All, shirt.colorIndex);
			gameObject.GetComponent<PhotonView>().RPC("TrocaCalca", RpcTarget.All, legs.propIndex);
            gameObject.GetComponent<PhotonView>().RPC("TrocaMaterialCalca", RpcTarget.All, legs.colorIndex);
        }*/
	}

	



	[PunRPC]
	public void ChangeHair()
    {
        som.Play();
		for (int i = 0; i < hairModels.Length; i++)
		{
			hairModels[i].SetActive(false);
		}
        
        hair.propIndex += 1;
        
        if (hair.propIndex >= hairModels.Length)
        {
            hair.propIndex = 0;
            hairModels[hair.propIndex].SetActive(true);
            hairColor[hair.propIndex].material = hair.color.corData[hair.colorIndex];
            Debug.Log("Acabou");
        }
        else
        {            
            hairModels[hair.propIndex].SetActive(true);
            hairColor[hair.propIndex].material = hair.color.corData[hair.colorIndex];
        }


        //PlayerPrefs.SetInt("hairIndex", hair.propIndex);
		PhotonNetwork.LocalPlayer.CustomProperties["hairIndex"] = hair.propIndex;

		//sRHair.sprite = hairSprite[hairIndex];
		//hairAC.ChangeAnimatorController();


	}

    [PunRPC]
	public void ChangeShirt()
    {
        som.Play();
		for (int i = 0; i < shirtModels.Length; i++)
		{
			shirtModels[i].SetActive(false);
		}
        shirt.propIndex += 1;
        if (shirt.propIndex >= shirtModels.Length)
        {
            shirt.propIndex = 0;
            shirtModels[0].SetActive(true);
            shirtsColor[shirt.propIndex].material = shirt.color.corData[shirt.colorIndex];
            Debug.Log("Acabou");
        }
        else
        {            
            shirtModels[shirt.propIndex].SetActive(true);
            shirtsColor[shirt.propIndex].material = shirt.color.corData[shirt.colorIndex];
        }

        //PlayerPrefs.SetInt("shirtIndex", shirt.propIndex);
		PhotonNetwork.LocalPlayer.CustomProperties["shirtIndex"] = shirt.propIndex;

		//sRChest.sprite = chestSprite[chestIndex];
		//chestAC.ChangeAnimatorController();

	}

	[PunRPC]
	public void ChangeLegs()
    {
        som.Play();
		for (int i = 0; i < pantModels.Length; i++)
		{
			pantModels[i].SetActive(false);
		}
        legs.propIndex += 1;
        if (legs.propIndex >= pantModels.Length)
        {


            legs.propIndex = 0;
            pantModels[0].SetActive(true);
            legsColor[legs.propIndex].material = legs.color.corData[legs.colorIndex];
            Debug.Log("Acabou");
        }
        else
        {
            pantModels[legs.propIndex].SetActive(true);
            legsColor[legs.propIndex].material = legs.color.corData[legs.colorIndex];
        }

        //PlayerPrefs.SetInt("legsIndex", legs.propIndex);
		PhotonNetwork.LocalPlayer.CustomProperties["legsIndex"] = legs.propIndex;

		//sRLegs.sprite = legsSprite[legsIndex];
		//legsAC.ChangeAnimatorController();


	}

    [PunRPC]
    public void ChangeHairColor()
    {
        som.Play();
        hair.colorIndex += 1;
        if (hair.colorIndex >= hair.color.corData.Length)
        {
            hair.colorIndex = 0;

            hairColor[hair.propIndex].material = hair.color.corData[hair.colorIndex];
            Debug.Log("Trocou");
        }

       for(int i = 0; i < hairColor.Length; i++)
       {
            hairColor[i].material = hair.color.corData[hair.colorIndex];
       }

        //PlayerPrefs.SetInt("hairColorIndex", hair.colorIndex);
        PhotonNetwork.LocalPlayer.CustomProperties["hairColorIndex"] = hair.colorIndex;


    }

    [PunRPC]
    public void ChangeShirtColor()
    {
        som.Play();
        shirt.colorIndex += 1;
        if (shirt.colorIndex >= shirt.color.corData.Length)
        {
            shirt.colorIndex = 0;

           shirtsColor[shirt.propIndex].material = shirt.color.corData[shirt.colorIndex];
        }

        else
        {
            shirtsColor[shirt.propIndex].material = shirt.color.corData[shirt.colorIndex];
        }

        //PlayerPrefs.SetInt("shirtColorIndex", shirt.colorIndex);
        PhotonNetwork.LocalPlayer.CustomProperties["shirtColorIndex"] = shirt.colorIndex;


    }

    [PunRPC]
    public void ChangeLegsColor()
    {
        som.Play();
        legs.colorIndex += 1;
        if (legs.colorIndex >= legs.color.corData.Length)
        {
            legs.colorIndex = 0;

            legsColor[legs.propIndex].material = legs.color.corData[legs.colorIndex];
        }

        else
        {
            legsColor[legs.propIndex].material = legs.color.corData[legs.colorIndex];
        }

        //PlayerPrefs.SetInt("legsColorIndex", legs.colorIndex);
        PhotonNetwork.LocalPlayer.CustomProperties["legsColorIndex"] = legs.colorIndex;


    }

    //----------------------------------------------------------------Ativando a roupa certa em cada cena
/*
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
        hairColor[hair.propIndex].material = hair.color[0].corData[hair.colorIndex];
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
        shirtsColor[shirt.propIndex].material = shirt.color[shirt.propIndex].corData[onlineIndex];
	}

	[PunRPC]
	private void TrocaCalca(int onlineIndex)
	{
		for (int i = 0; i < pantModels.Length; i++)
		{
			pantModels[i].SetActive(false);
		}
		pantModels[onlineIndex].SetActive(true);
	}

    [PunRPC]
    private void TrocaMaterialCalca(int onlineIndex)
    {
        shirtsColor[shirt.propIndex].material = shirt.color[shirt.propIndex].corData[shirt.colorIndex];
    }*/





    public void Jogar()
    {
        playerScript.enabled = true;
    }
}
