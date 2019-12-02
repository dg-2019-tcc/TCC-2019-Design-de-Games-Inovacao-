using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomManager : MonoBehaviour
{
    /// <summary>
    /// 
    /// Script para pdoer alterar os modelos das 
    /// roupas do jogador
    /// 
    /// O script tem 2 funções principais, a 
    /// primeira é para mudar o modelo
    /// enquanto a segunda serve para mudar 
    /// a cor
    /// 
    /// Além disso ainda tem as funções
    /// que são chamadas pelos botões,
    /// as quais tem "switchs" dentro
    /// delas para que possamos simplesmente
    /// chamar o script e acessar o "case"
    /// que quisermos de forma mais fácil
    /// e rápida
    /// 
    /// </summary>



    [Header("Modelos")]

    public GameObject[] hairModels;
    public GameObject[] shirtModels;
    public GameObject[] legsModels;
    public GameObject[] shoesModels;




    [Header("Materiais")]

    public SkinnedMeshRenderer[] hairColor;
    public SkinnedMeshRenderer[] shirtColor;
    public SkinnedMeshRenderer[] legsColor;
    public SkinnedMeshRenderer[] shoesColor;



    [Header("PropCustom")]

    public PropsCustom hair;
    public PropsCustom shirt;
    public PropsCustom legs;
    public PropsCustom shoe;



    [Header("Scripts Externos")]

    public PlayerMovement playerScript;



    [Header("Som")]

    public AudioSource som;



    #region FUNCTION

    [PunRPC]
    void ChangeModelFunction(GameObject[] arrayModelos, PropsCustom prop, SkinnedMeshRenderer[] arrayCollor, string indexName)
    {
        som.Play();
        for (int i = 0; i < arrayModelos.Length; i++)
        {
            arrayModelos[i].SetActive(false);
        }

        prop.propIndex += 1;

        if (prop.propIndex >= arrayModelos.Length)
        {
            prop.propIndex = 0;
            arrayModelos[prop.propIndex].SetActive(true);
            arrayCollor[prop.propIndex].material = prop.color.corData[prop.colorIndex];
        }
        else
        {
            arrayModelos[prop.propIndex].SetActive(true);
            arrayCollor[prop.propIndex].material = prop.color.corData[prop.colorIndex];
        }
        PhotonNetwork.LocalPlayer.CustomProperties[indexName] = prop.propIndex;
    }
         
    [PunRPC]
    void ChangeColorFunction(PropsCustom prop, SkinnedMeshRenderer[] arrayCollor, string indexName)
    {
        som.Play();
        prop.colorIndex += 1;
        if (prop.colorIndex >= prop.color.corData.Length)
        {
            prop.colorIndex = 0;

            arrayCollor[prop.propIndex].material = prop.color.corData[prop.colorIndex];
        }

        for (int i = 0; i < arrayCollor.Length; i++)
        {
            arrayCollor[i].material = prop.color.corData[prop.colorIndex];
        }

        PhotonNetwork.LocalPlayer.CustomProperties[indexName] = prop.colorIndex;
    }

    #endregion

    #region BUTTON

    [PunRPC]
    public void ChangeModelButton(string efeito)
    {
        switch (efeito)
        {
            case "hair":
                ChangeModelFunction(hairModels, hair, hairColor, "hairIndex");
                break;

            case "shirt":
                ChangeModelFunction(shirtModels, shirt, shirtColor, "shirtIndex");
                break;

            case "legs":
                ChangeModelFunction(legsModels, legs, legsColor, "legsIndex");
                break;

            case "shoe":
                ChangeModelFunction(shoesModels, shoe, shoesColor, "shoeIndex");
                break;
        }
    }

    [PunRPC]
    public void ChangeColorButton(string efeito)
    {
        switch (efeito)
        {
            case "hair":
                ChangeColorFunction(hair, hairColor, "hairColorIndex");
                break;

            case "shirt":
                ChangeColorFunction(shirt, shirtColor, "shirtColorIndex");
                break;

            case "legs":
                ChangeColorFunction(legs, legsColor, "legsColorIndex");
                break;

            case "shoe":
                ChangeColorFunction(shoe, shoesColor, "shoeColorIndex");
                break;
        }
    }

    #endregion
   

    public void Jogar()
    {
        playerScript.enabled = true;
    }
}
