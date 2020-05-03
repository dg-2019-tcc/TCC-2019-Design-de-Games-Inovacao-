using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomTeste : MonoBehaviour
{
    public Sprite[] cabelo; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19
    public Button[] botaoDeCor; //Qual o botão que a gente está mexendo

    public Button ContinuaButton;

    public GameObject Menu1;
    public GameObject Menu2;
    public GameObject Menu3;

    public int qualModeloVaiSer;

    public int quantasCoresExistem;

    public void qualBotaoFoiPressionado(int qualMenuAtiva)
    {
        switch (qualMenuAtiva)
        {
            case 1:
                DesativaAsCores();
                Menu1.SetActive(true);
                Menu2.SetActive(false);
                Menu3.SetActive(false);
                break;
            case 2:
                ContinuaButton.interactable = false;
                Menu1.SetActive(false);
                Menu2.SetActive(true);
                Menu3.SetActive(false);
                break;
            case 3:
                Menu1.SetActive(false);
                Menu2.SetActive(false);
                Menu3.SetActive(true);
                break;
        }
    }

    public void SelecionaModelo(int qualModelo)
    {
        qualModeloVaiSer = qualModelo;
        if(ContinuaButton.interactable == false)
        {
            ContinuaButton.interactable = true;
        }
    }

    public void QuantidadeDeCores(int quantasCores)
    {
        quantasCoresExistem = quantasCores;
    }

    public void EscolheACor()
    {
        int q = qualModeloVaiSer * quantasCoresExistem;
        for (int i = 0; i < quantasCoresExistem; i++)
        {
            botaoDeCor[i].gameObject.SetActive(true);
            botaoDeCor[i].image.sprite = cabelo[q]; //Muda a sprite para a imagem de cor certa
            //botaoDeCor[i].CodigoInterno = q; //Vai mandar um valor para um script dentro do botão que vai determinar qual cabelo será ativado caso esse botão seja pressionado
            q++; //Aumenta o valor de "q" para poder mudar quando passar de botão
        }
    }

    public void DesativaAsCores()
    {
        for (int i = 0; i < 5; i++)
        {
            botaoDeCor[i].gameObject.SetActive(false);
        }
    }
}
