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

    //public Material[] shirtsMat;

    public SkinnedMeshRenderer[] hairColor;

    public SkinnedMeshRenderer[] legsColor;

    public SkinnedMeshRenderer[] shirtsColor;

    /* public int bodyIndex;

     public int hairIndex;

     public int chestIndex;

     public int legsIndex;

     public int shirtIndex;*/

    public PropsCustom hair;

    public PropsCustom shirt;

    public PropsCustom legs;

    public GameObject custom;

    public PlayerMovement playerScript;

    public AudioSource som;


    private void Start()
    {
        /*
        hairIndex = 0;
        shirtIndex = 0;
        legsIndex = 0;
        */

       //PlayerPrefs.SetInt("hairIndex", hairIndex);
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
			gameObject.GetComponent<PhotonView>().RPC("TrocaCamisa", RpcTarget.All, shirt.propIndex);
			gameObject.GetComponent<PhotonView>().RPC("TrocaMaterialCamisa", RpcTarget.All, shirt.colorIndex);
			gameObject.GetComponent<PhotonView>().RPC("TrocaCalca", RpcTarget.All, legs.propIndex);
		}
	}

	



	[PunRPC]
	public void ChangeHair()
    {
        som.Play();
        hairModels[hair.propIndex].SetActive(false);
        hair.propIndex += 1;
        if (hair.propIndex > hairModels.Length)
        {

            hairModels[0].SetActive(true);
            hairColor[hair.propIndex].material = hair.color[0].corData[hair.colorIndex];
            Debug.Log("Acabou");
        }
        else if (hair.propIndex == hairModels.Length)
        {
            hair.propIndex = 0;
            Debug.Log("Acabou");
        }
        else
        {            
            hairModels[hair.propIndex].SetActive(true);
            hairColor[hair.propIndex].material = hair.color[0].corData[hair.colorIndex];
        }


        PlayerPrefs.SetInt("hairIndex", hair.propIndex);
		PhotonNetwork.LocalPlayer.CustomProperties["hairIndex"] = hair.propIndex;

		//sRHair.sprite = hairSprite[hairIndex];
		//hairAC.ChangeAnimatorController();


	}

    [PunRPC]
	public void ChangeChest()
    {
        som.Play();
        shirtModels[shirt.propIndex].SetActive(false);
        shirt.propIndex += 1;
        if (shirt.propIndex >= shirtModels.Length)
        {
            shirt.propIndex = 0;
            shirtModels[0].SetActive(true);
            shirtsColor[shirt.propIndex].material = shirt.color[shirt.propIndex].corData[shirt.colorIndex];
            Debug.Log("Acabou");
        }
        else
        {            
            shirtModels[shirt.propIndex].SetActive(true);
            shirtsColor[shirt.propIndex].material = shirt.color[shirt.propIndex].corData[shirt.colorIndex];
        }
        PlayerPrefs.SetInt("chestIndex", shirt.propIndex);
		PhotonNetwork.LocalPlayer.CustomProperties["chestIndex"] = shirt.propIndex;

		//sRChest.sprite = chestSprite[chestIndex];
		//chestAC.ChangeAnimatorController();

	}

	[PunRPC]
	public void ChangeLegs()
    {
        som.Play();
        pantModels[legs.propIndex].SetActive(false);
        legs.propIndex += 1;
        if (legs.propIndex >= pantModels.Length)
        {


            legs.propIndex = 0;
            pantModels[0].SetActive(true);
            legsColor[legs.propIndex].material = legs.color[legs.propIndex].corData[legs.colorIndex];
            Debug.Log("Acabou");
        }
        else
        {
            pantModels[legs.propIndex].SetActive(true);
            legsColor[legs.propIndex].material = legs.color[legs.propIndex].corData[legs.colorIndex];
        }

        PlayerPrefs.SetInt("legsIndex", legs.propIndex);
		PhotonNetwork.LocalPlayer.CustomProperties["legsIndex"] = legs.propIndex;

		//sRLegs.sprite = legsSprite[legsIndex];
		//legsAC.ChangeAnimatorController();


	}

    [PunRPC]
    public void ChangeHairColor()
    {
        som.Play();
        hair.colorIndex += 1;
        if (hair.colorIndex >= hair.color[0].corData.Length)
        {
            hair.colorIndex = 0;

            hairColor[hair.propIndex].material = hair.color[0].corData[hair.colorIndex];
        }

        else
        {
            hairColor[hair.propIndex].material = hair.color[0].corData[hair.colorIndex];
        }

        /*PlayerPrefs.SetInt("hairIndex", shirt.colorIndex);
        PhotonNetwork.LocalPlayer.CustomProperties["shirtIndex"] = shirt.colorIndex;*/


    }

    [PunRPC]
    public void ChangeShirtColor()
    {
        som.Play();
        shirt.colorIndex += 1;
        if (shirt.colorIndex >= shirt.color[shirt.propIndex].corData.Length)
        {
            shirt.colorIndex = 0;

           shirtsColor[shirt.propIndex].material = shirt.color[shirt.propIndex].corData[shirt.colorIndex];
        }

        else
        {
            shirtsColor[shirt.propIndex].material = shirt.color[shirt.propIndex].corData[shirt.colorIndex];
        }

        PlayerPrefs.SetInt("shirtIndex", shirt.colorIndex);
        PhotonNetwork.LocalPlayer.CustomProperties["shirtIndex"] = shirt.colorIndex;


    }

    [PunRPC]
    public void ChangeLegsColor()
    {
        som.Play();
        legs.colorIndex += 1;
        if (legs.colorIndex >= legs.color[legs.propIndex].corData.Length)
        {
            legs.colorIndex = 0;

            legsColor[legs.propIndex].material = legs.color[legs.propIndex].corData[legs.colorIndex];
        }

        else
        {
            legsColor[legs.propIndex].material = legs.color[legs.propIndex].corData[legs.colorIndex];
        }

        PlayerPrefs.SetInt("shirtIndex", legs.colorIndex);
        PhotonNetwork.LocalPlayer.CustomProperties["shirtIndex"] = legs.colorIndex;


    }

    //----------------------------------------------------------------Ativando a roupa certa em cada cena

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





	public void Jogar()
    {
        custom.SetActive(false);
        playerScript.enabled = true;
    }
}
