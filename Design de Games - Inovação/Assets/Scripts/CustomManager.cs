using Photon.Pun;
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



    [PunRPC]
	public void ChangeHair()
    {
        if (hairIndex >= hairModels.Length -1)
        {


            hairIndex = 0;
            hairModels[0].SetActive(true);
            Debug.Log("Acabou");
        }
        else
        {

            hairModels[hairIndex].SetActive(false);
            hairIndex += 1;
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


        if (chestIndex >= shirtModels.Length -1)
        {


            chestIndex = 0;
            shirtModels[0].SetActive(true);
            Debug.Log("Acabou");
        }
        else
        {

            shirtModels[chestIndex].SetActive(false);
            chestIndex += 1;
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
        if (legsIndex >= pantModels.Length -1)
        {


            legsIndex = 0;
            pantModels[0].SetActive(true);
            Debug.Log("Acabou");
        }
        else
        {

            pantModels[legsIndex].SetActive(false);
            legsIndex += 1;
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
        if(shirtIndex >= shirtsMat.Length -1)
        {
            shirtIndex = 0;

            shirtColor.material = shirtsMat[shirtIndex];
        }

        else
        {
            shirtIndex += 1;
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
