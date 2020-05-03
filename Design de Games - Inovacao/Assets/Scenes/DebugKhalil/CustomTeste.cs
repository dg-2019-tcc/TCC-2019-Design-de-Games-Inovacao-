using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomTeste : MonoBehaviour
{
    public Sprite[] cabelo; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19
    public Sprite[] shirt; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19
    public Sprite[] shoes; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19
    public Sprite[] shorts; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19
    
    public Button[] botaoDeCor; //Qual o botão que a gente está mexendo
    public Button[] botaoDeModelo; //Qual botão dos modelos estamos mexendo

    [Header("Botão para confirmar escolha de modelo")]
    public Button ContinuaButton;

    [Header("Telas De Menu")]
    public GameObject Menu1;
    public GameObject Menu2;
    public GameObject Menu3;

    public int qualParteVaiSer;
    public int qualModeloVaiSer;
    public int quantasCoresExistem;

    public Custom2D customizaScript;

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
            botaoDeCor[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, q); });
            switch (qualParteVaiSer)
            {
                case 1:
                    botaoDeCor[i].image.sprite = cabelo[q]; //Muda a sprite para a imagem de cor certa
                    break;
                case 2:
                    botaoDeCor[i].image.sprite = shirt[q]; //Muda a sprite para a imagem de cor certa
                    break;
                case 3:
                    botaoDeCor[i].image.sprite = shoes[q]; //Muda a sprite para a imagem de cor certa
                    break;
                case 4:
                    botaoDeCor[i].image.sprite = shorts[q]; //Muda a sprite para a imagem de cor certa
                    break;
            }
            q++; //Aumenta o valor de "q" para poder mudar quando passar de botão
        }
    }


    public void EscolheQtdModelo(int qtdDeModelos)
    {
        int q = qtdDeModelos;
        for (int i = 0; i < qtdDeModelos; i++)
        {
            botaoDeModelo[i].gameObject.SetActive(true);
            //LEMBRA DE DESATIVAR OS BOTÕES QUE NÃO SERÃO USADOS PORFAVOR
            switch (qualParteVaiSer)
            {
                case 1:
                    botaoDeModelo[i].image.sprite = cabelo[q * i];
                    break;
                case 2:
                    botaoDeModelo[i].image.sprite = shirt[q * i];
                    break;
                case 3:
                    botaoDeModelo[i].image.sprite = shoes[q * i];
                    break;
                case 4:
                    botaoDeModelo[i].image.sprite = shorts[q * i];
                    break;
            }
        }
    }


    public void DesativaAsCores()
    {
        for (int i = 0; i < 5; i++)
        {
            botaoDeCor[i].onClick.RemoveAllListeners();
            botaoDeCor[i].gameObject.SetActive(false);
        }
    }


    public void QualParte(int selectedPart)
    {
        switch(selectedPart)
        {
            case 1:
                qualParteVaiSer = 1;
                break;
            case 2:
                qualParteVaiSer = 2;
                break;
            case 3:
                qualParteVaiSer = 3;
                break;
            case 4:
                qualParteVaiSer = 4;
                break;
        }
    }

    public void QualParteVaiMudar(int selectedPart, int index)
    {
        switch (selectedPart)
        {
            case 1:
                customizaScript.ChangeHair(index);
                break;
            case 2:
                customizaScript.ChangeShirt(index);
                break;
            case 3:
                customizaScript.ChangeShoes(index);
                break;
            case 4:
                customizaScript.ChangeShort(index);
                break;
        }
    }
}
