using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICustom : MonoBehaviour
{
    public int hairIndex;
    public int shirtIndex;
    public int shortsIndex;
    public int shoesIndex;
    public int oculosIndex;
    public int maskIndex;
    public int skinIndex;

    public BotCustom botCustom;

    public bool isVictory;

    public ChangeMultipleCustom[] hairModels;
    public ChangeMultipleCustom[] shirtModels;
    public ChangeMultipleCustom[] legModels;
    public ChangeMultipleCustom[] shoeModels;
    public ChangeMultipleCustom[] oculosModels;
    public ChangeMultipleCustom[] maskModels;
    public ChangeMultipleCustom[] skinModels;

    public ChangeMultipleCustom[] hair2Models;
    public ChangeMultipleCustom[] shirt2Models;
    public ChangeMultipleCustom[] leg2Models;
    public ChangeMultipleCustom[] shoe2Models;
    public ChangeMultipleCustom[] oculos2Models;
    public ChangeMultipleCustom[] mask2Models;
    public ChangeMultipleCustom[] skin2Models;

    // Start is called before the first frame update
    void Start()
    {

        if (isVictory == false)
        {
            hairIndex = Random.Range(0, 31);
            shirtIndex = Random.Range(0, 11);
            shortsIndex = Random.Range(0, 5);
            shoesIndex = Random.Range(0, 2);
            oculosIndex = Random.Range(0, 2);
            maskIndex = Random.Range(0, 2);
            skinIndex = Random.Range(0, 4);

            botCustom.hairIndex = hairIndex;
            botCustom.shirtIndex = shirtIndex;
            botCustom.shortsIndex = shortsIndex;
            botCustom.shoesIndex = shoesIndex;
            botCustom.oculosIndex = oculosIndex;
            botCustom.maskIndex = maskIndex;
            botCustom.skinIndex = skinIndex;
        }

        else
        {
            hairIndex = botCustom.hairIndex;
            shirtIndex = botCustom.shirtIndex;
            shortsIndex = botCustom.shortsIndex;
            shoesIndex = botCustom.shoesIndex;
            oculosIndex = botCustom.oculosIndex;
            maskIndex = botCustom.maskIndex;
            skinIndex = botCustom.skinIndex;
        }

        TrocaCabelo(hairIndex);
        TrocaCamisa(shirtIndex);
        TrocaCalca(shortsIndex);
        TrocaSapato(shoesIndex);
        TrocaOculos(oculosIndex);
        TrocaMask(maskIndex);
        TrocaSkin(skinIndex);

    }

    private void TrocaSkin(int onlineIndex)
    {
        for (int i = 0; i < skinModels.Length; i++)
        {
            skinModels[i].ChangeCustom(false);
            skin2Models[i].ChangeCustom(false);
        }
        skinModels[onlineIndex].ChangeCustom(true);
        skin2Models[onlineIndex].ChangeCustom(true);
    }

    private void TrocaCabelo(int onlineIndex)
    {
        for (int i = 0; i < hairModels.Length; i++)
        {
            hairModels[i].ChangeCustom(false);
            hair2Models[i].ChangeCustom(false);
        }
        hairModels[onlineIndex].ChangeCustom(true);
        hair2Models[onlineIndex].ChangeCustom(true);
    }

    private void TrocaCamisa(int onlineIndex)
    {
        for (int i = 0; i < shirtModels.Length; i++)
        {
            shirtModels[i].ChangeCustom(false);
            shirt2Models[i].ChangeCustom(false);
        }
        shirtModels[onlineIndex].ChangeCustom(true);
        shirt2Models[onlineIndex].ChangeCustom(true);
    }

    private void TrocaCalca(int onlineIndex)
    {
        for (int i = 0; i < legModels.Length; i++)
        {
            legModels[i].ChangeCustom(false);
            leg2Models[i].ChangeCustom(false);
        }
        legModels[onlineIndex].ChangeCustom(true);
        leg2Models[onlineIndex].ChangeCustom(true);
    }

    private void TrocaSapato(int onlineIndex)
    {
        for (int i = 0; i < shoeModels.Length; i++)
        {
            shoeModels[i].ChangeCustom(false);
            shoe2Models[i].ChangeCustom(false);
        }
        shoeModels[onlineIndex].ChangeCustom(true);
        shoe2Models[onlineIndex].ChangeCustom(true);
    }

    private void TrocaOculos(int onlineIndex)
    {
        for(int i = 0; i < oculosModels.Length; i++)
        {
            oculosModels[i].ChangeCustom(false);
            oculos2Models[i].ChangeCustom(false);
        }

        if(oculosIndex != 2)
        {
            oculosModels[onlineIndex].ChangeCustom(true);
            oculos2Models[onlineIndex].ChangeCustom(true);
        }
    }

    private void TrocaMask(int onlineIndex)
    {
        for (int i = 0; i < maskModels.Length; i++)
        {
            maskModels[i].ChangeCustom(false);
            mask2Models[i].ChangeCustom(false);
        }

        if (oculosIndex != 2)
        {
            maskModels[onlineIndex].ChangeCustom(true);
            mask2Models[onlineIndex].ChangeCustom(true);
        }
    }
}
