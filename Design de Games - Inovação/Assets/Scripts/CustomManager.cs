﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomManager : MonoBehaviour
{

    public GameObject[] hairModels;

    public GameObject[] shirtModels;

    public GameObject[] pantModels;

    public Material[] shirtsMat;

    public MeshRenderer shirtColor;

    public int bodyIndex;

    public int hairIndex;

    public int chestIndex;

    public int legsIndex;

    public int shirtIndex;

    public GameObject custom;

    public Player playerScript;

    public AudioSource som;


    private void Start()
    {
        hairIndex = 0;
        shirtIndex = 0;
        legsIndex = 0;

        PlayerPrefs.SetInt("hairIndex", hairIndex);
        PhotonNetwork.LocalPlayer.CustomProperties["hairIndex"] = hairIndex;

        PlayerPrefs.SetInt("shirtIndex", shirtIndex);
        PhotonNetwork.LocalPlayer.CustomProperties["shirtIndex"] = shirtIndex;

        PlayerPrefs.SetInt("legsIndex", legsIndex);
        PhotonNetwork.LocalPlayer.CustomProperties["legsIndex"] = legsIndex;


    }



    [PunRPC]
	public void ChangeHair()
    {
        som.Play();
        hairModels[hairIndex].SetActive(false);
        hairIndex += 1;
        if (hairIndex >= hairModels.Length)
        {


            hairIndex = 0;
            hairModels[0].SetActive(true);
            Debug.Log("Acabou");
        }
        else
        {            
            hairModels[hairIndex].SetActive(true);
        }


        PlayerPrefs.SetInt("hairIndex", hairIndex);
		PhotonNetwork.LocalPlayer.CustomProperties["hairIndex"] = hairIndex;

		//sRHair.sprite = hairSprite[hairIndex];
		//hairAC.ChangeAnimatorController();


	}

	[PunRPC]
	public void ChangeChest()
    {
        som.Play();
        shirtModels[chestIndex].SetActive(false);
        chestIndex += 1;
        if (chestIndex >= shirtModels.Length)
        {
            chestIndex = 0;
            shirtModels[0].SetActive(true);
            Debug.Log("Acabou");
        }
        else
        {            
            shirtModels[chestIndex].SetActive(true);
        }
        PlayerPrefs.SetInt("chestIndex", chestIndex);
		PhotonNetwork.LocalPlayer.CustomProperties["chestIndex"] = chestIndex;

		//sRChest.sprite = chestSprite[chestIndex];
		//chestAC.ChangeAnimatorController();

	}

	[PunRPC]
	public void ChangeLegs()
    {
        som.Play();
        pantModels[legsIndex].SetActive(false);
        legsIndex += 1;
        if (legsIndex >= pantModels.Length)
        {


            legsIndex = 0;
            pantModels[0].SetActive(true);
            Debug.Log("Acabou");
        }
        else
        {
            pantModels[legsIndex].SetActive(true);
        }

        PlayerPrefs.SetInt("legsIndex", legsIndex);
		PhotonNetwork.LocalPlayer.CustomProperties["legsIndex"] = legsIndex;

		//sRLegs.sprite = legsSprite[legsIndex];
		//legsAC.ChangeAnimatorController();


	}

    [PunRPC]
    public void ChangeShirt()
    {
        som.Play();
        shirtIndex += 1;
        if (shirtIndex >= shirtsMat.Length)
        {
            shirtIndex = 0;

            shirtColor.material = shirtsMat[shirtIndex];
        }

        else
        {
            shirtColor.material = shirtsMat[shirtIndex];
        }

        PlayerPrefs.SetInt("shirtIndex", shirtIndex);
        PhotonNetwork.LocalPlayer.CustomProperties["shirtIndex"] = shirtIndex;


    }

    public void Jogar()
    {
        custom.SetActive(false);
        playerScript.enabled = true;
    }
}
