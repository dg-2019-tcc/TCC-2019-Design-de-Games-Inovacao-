using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomManager : MonoBehaviour
{
    /*public SpriteRenderer sRBody;

    public SpriteRenderer sRHair;

    public SpriteRenderer sRChest;

    public SpriteRenderer sRLegs;*/

    public Image bodyImage;

    public Image hairImage;

    public Image chestImage;

    public Image legsImage;

    public Sprite[] bodySprite;

    public Sprite[] hairSprite;

    public Sprite[] chestSprite;

    public Sprite[] legsSprite;

    public int bodyIndex;

    public int hairIndex;

    public int chestIndex;

    public int legsIndex;

    /*public BodyManager bodyAC;

    public HairManager hairAC;

    public ChestManager chestAC;

    public LegsManager legsAC;*/

    public GameObject custom;

    public Player playerScript;





    public void ChangeBody()
    {
        bodyIndex += 1;
        if(bodyIndex >= bodySprite.Length)
        {
            bodyIndex = 0;
        }

        bodyImage.sprite = bodySprite[bodyIndex];

        PlayerPrefs.SetInt("bodyIndex", bodyIndex);

        //sRBody.sprite = bodySprite[bodyIndex];
        //bodyAC.ChangeAnimatorController();
    }

    public void ChangeHair()
    {
        hairIndex += 1;
        if(hairIndex >= hairSprite.Length)
        {
            hairIndex = 0;
        }

        hairImage.sprite = hairSprite[hairIndex];

        PlayerPrefs.SetInt("hairIndex", hairIndex);

        //sRHair.sprite = hairSprite[hairIndex];
        //hairAC.ChangeAnimatorController();


    }

    public void ChangeChest()
    {
        chestIndex += 1;
        if(chestIndex >= chestSprite.Length)
        {
            chestIndex = 0;
        }

        chestImage.sprite = chestSprite[chestIndex];

        PlayerPrefs.SetInt("chestIndex", chestIndex);

        //sRChest.sprite = chestSprite[chestIndex];
        //chestAC.ChangeAnimatorController();

    }

    public void ChangeLegs()
    {
        legsIndex += 1;
        if(legsIndex >= legsSprite.Length)
        {
            legsIndex = 0;
        }

        legsImage.sprite = legsSprite[legsIndex];

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
