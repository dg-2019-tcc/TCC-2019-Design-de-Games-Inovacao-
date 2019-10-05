using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomManager : MonoBehaviour
{

    public GameObject[] hairModels;

    public GameObject[] shirtModels;

    public GameObject[] pantModels;

    public int bodyIndex;

    public int hairIndex;

    public int chestIndex;

    public int legsIndex;

    public GameObject custom;

    public Player playerScript;



    public void ChangeHair()
    {
        if (hairIndex >= hairModels.Length)
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

        //sRHair.sprite = hairSprite[hairIndex];
        //hairAC.ChangeAnimatorController();


    }

    public void ChangeChest()
    {


        if (chestIndex >= shirtModels.Length)
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

        //sRChest.sprite = chestSprite[chestIndex];
        //chestAC.ChangeAnimatorController();

    }

    public void ChangeLegs()
    {
        if (legsIndex >= pantModels.Length)
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

        //sRLegs.sprite = legsSprite[legsIndex];
        //legsAC.ChangeAnimatorController();


    }

    public void Jogar()
    {
        custom.SetActive(false);
        playerScript.enabled = true;
    }
}
