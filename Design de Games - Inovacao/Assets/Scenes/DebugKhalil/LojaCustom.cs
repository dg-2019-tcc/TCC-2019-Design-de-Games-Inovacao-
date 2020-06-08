using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LojaCustom : MonoBehaviour
{

    [Header("Botões da cena")]
    public Button[] botaoDeModelo; //Qual botão dos modelos estamos mexendo
    public Button[] botaoDeCor; //Qual o botão que a gente está mexendo


    [Header("Círculo de seleção")]
    public GameObject[] circuloDeSelecaoModelo;
    public GameObject[] circuloDeSelecaoCor;


    [Header("Sprites da HUD de botões")]
    public Sprite[] spriteCabelo; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19
    public Sprite[] spriteShirt; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19   
    public Sprite[] spriteShorts; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19
    public Sprite[] spriteTenis; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19
    public Sprite[] spriteOculos; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19
    public Sprite[] spriteDelineado; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19
    public Sprite[] spriteMascara; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19


    [Header("Quantidade de cores que cada modelo tem")]
    public int[] coresCabelo;
    public int[] coresShirt;
    public int[] coresShort;
    public int[] coresTenis;
    public int[] coresOculos;
    public int[] coresDelineado;
    public int[] coresMascara;


    [Header("Telas De Menu")]
    public GameObject[] menu;


    [Header("Número de botões que irão spawnar")]
    public int quantidadeDeModelos;
    public int maiorQuantitadeDeCoresDessaParte;


    [Header("Custom2D Script")]
    public Custom2D customizaScript;


    [Header("Botao Next")]
    public GameObject nextButton;


    [Header("Armazena o valor dos botões e efetiva a escolha do modelo")]
    int botaoModelo1;
    int botaoModelo2;
    int botaoModelo3;
    int botaoModelo4;
    int botaoModelo5;
    int botaoModelo6;


    [Header("Com qual parte estamos mexendo agora?")]
    public int qualParteVaiSer;


    [Header("Começa os sprites a partir desse número")]
    public int comecaAqui;
    //Utilizado para determinar a partir de qual sprite será iniciada associação dos botões


    private void Start()
    {
        AtivaBotoesModelo(comecaAqui);
    }

    // Serve para trocar entre as telas dos menus da customização
    public void TrocaDeMenu(int qualMenuAtiva)
    {
        for (int i = 0; i < menu.Length; i++)
        {
            if (menu[i] != null)
            {
                menu[i].SetActive(false);
            }
        }
        menu[qualMenuAtiva].SetActive(true);        
    }
    
    public void AlteraQtdDeBotoesModelo(int qtd)
    {
        quantidadeDeModelos = qtd;
    }

    public void AtivaBotoesModelo(int iniciaAqui)//EscolheQtdBotoesModelos //Decide quantos botões de modelo serão mostrados
    {
        int q = 5;

        //Desativa todos os game objects para caso algum deles tenha ficado ativo sem querer
        for(int k = 0; k < 6; k++)
        {
            botaoDeModelo[k].gameObject.SetActive(false);
        }

        for (int i = 0; i < quantidadeDeModelos; i++)
        {
            botaoDeModelo[i].gameObject.SetActive(true);//Ativa o botão

            switch (qualParteVaiSer)//Decide qual sprite vai aparecer nos botões de Modelo
            {
                case 1:
                    botaoDeModelo[i].image.sprite = spriteCabelo[q * iniciaAqui];
                    break;
                case 2:
                    botaoDeModelo[i].image.sprite = spriteShirt[q * iniciaAqui];
                    break;
                case 3:
                    botaoDeModelo[i].image.sprite = spriteTenis[q * iniciaAqui];
                    break;
                case 4:
                    botaoDeModelo[i].image.sprite = spriteShorts[q * iniciaAqui];
                    break;
                case 5:
                    botaoDeModelo[i].image.sprite = spriteOculos[q * iniciaAqui];
                    break;
                case 6:
                    botaoDeModelo[i].image.sprite = spriteDelineado[q * iniciaAqui];
                    break;
                case 7:
                    botaoDeModelo[i].image.sprite = spriteMascara[q * iniciaAqui];
                    break;
            }
            switch (i)
            {
                case 0:
                    botaoModelo1 = q * i;
                    break;
                case 1:
                    botaoModelo2 = q * i;
                    break;
                case 2:
                    botaoModelo3 = q * i;
                    break;
                case 3:
                    botaoModelo4 = q * i;
                    break;
                case 4:
                    botaoModelo5 = q * i;
                    break;
                case 5:
                    botaoModelo6 = q * i;
                    break;
            }
            iniciaAqui++;
        }
    }




    /// <QualParte>
    /// 
    /// Indica para os botões de Modelo (2 Level) qual das partes foi selecionada (Cabelo, Camisa, Calça,
    /// Sapato) através da variavel "selectedPart" que pode ser alterada no inspector de cada botão (sendo
    /// esses botões os botões do menu de Partes (1 Level)).
    /// 
    /// Além de também enviar a informação sobre quantas cores diferentes existem no modelo com mais 
    /// variedade dessa parte (Função "QuantidadeDeCores")
    /// 
    /// </QualParte>
    public void QualParte(int selectedPart) // Vai ter que informar através da porta, talvez separe em código
    {
        qualParteVaiSer = selectedPart;
    }
}
