using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LojaCustom : MonoBehaviour
{

    [Header("Botões da cena")]
    public Button[] botaoDeModelo; //Qual botão dos modelos estamos mexendo


    [Header("Círculo de seleção")]
    public GameObject[] circuloDeSelecaoModelo;


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


    [Header("Em qual dos grupos de menus estamos mexendo")]
    int qualMenu = 1;
    public int maximoLoop;
    public int minimoLoop;


    [Header("Boolean para verificar se precisa ou não do parametro no ativa botoes modelo")]
    bool ativaParametro = false;




    private void Start()
    {
        AtivaBotoesModelo(comecaAqui);
    }


    public void IgnoraParametro()
    {
        ativaParametro = true;
    }    

    public void AtivaBotoesModelo(int iniciaAqui)//EscolheQtdBotoesModelos //Decide quantos botões de modelo serão mostrados
    {
        if (ativaParametro == true)
        {
            iniciaAqui = comecaAqui;
        }

        //Desativa todos os game objects para caso algum deles tenha ficado ativo sem querer
        for(int k = 0; k < botaoDeModelo.Length; k++)
        {
            Debug.Log("rodei");
            botaoDeModelo[k].gameObject.SetActive(false);
        }

        for (int i = 0; i < quantidadeDeModelos; i++)
        {
            switch (qualParteVaiSer)//Decide qual sprite vai aparecer nos botões de Modelo
            {
                case 1:
                    if(iniciaAqui < spriteCabelo.Length)
                    {
                        botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                        botaoDeModelo[i].image.sprite = spriteCabelo[iniciaAqui];
                    }
                    break;
                case 2:
                    if (iniciaAqui < spriteShirt.Length)
                    {
                        botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                        botaoDeModelo[i].image.sprite = spriteShirt[iniciaAqui];
                    }
                    break;
                case 3:
                    if (iniciaAqui < spriteTenis.Length)
                    {
                        botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                        botaoDeModelo[i].image.sprite = spriteTenis[iniciaAqui];
                    }
                    break;
                case 4:
                    if (iniciaAqui < spriteShorts.Length)
                    {
                        botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                        botaoDeModelo[i].image.sprite = spriteShorts[iniciaAqui];
                    }
                    break;
                case 5:
                    if (iniciaAqui < spriteOculos.Length)
                    {
                        botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                        botaoDeModelo[i].image.sprite = spriteOculos[iniciaAqui];
                    }
                    break;
                case 6:
                    if (iniciaAqui < spriteDelineado.Length)
                    {
                        botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                        botaoDeModelo[i].image.sprite = spriteDelineado[iniciaAqui];
                    }
                    break;
                case 7:
                    if (iniciaAqui < spriteMascara.Length)
                    {
                        botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                        botaoDeModelo[i].image.sprite = spriteMascara[iniciaAqui];
                    }
                    break;
            }
            switch (i)
            {
                case 0:
                    botaoModelo1 = i;
                    break;
                case 1:
                    botaoModelo2 = i;
                    break;
                case 2:
                    botaoModelo3 = i;
                    break;
                case 3:
                    botaoModelo4 = i;
                    break;
                case 4:
                    botaoModelo5 = i;
                    break;
                case 5:
                    botaoModelo6 = i;
                    break;
            }
            iniciaAqui++;
        }
        ativaParametro = false;
    }



    public void SetMinLoop(int min)
    {
        maximoLoop = min;
    }
    public void SetMaxLoop(int max)
    {
        maximoLoop = max;
    }


    public void ChangeMenu(int muda)
    {
        qualMenu += muda;
    }

    public void QuaisModelosEstaMostrando()
    {
        //Lembrar de resetar o valor de "qualMenu" quando mudar de menu
        if(qualMenu > maximoLoop)
        {
            qualMenu = 1;
        }else if(qualMenu < minimoLoop)
        {
            qualMenu = maximoLoop;
        }

        switch (qualMenu)
        {
            case 1:
                comecaAqui = 0;
                break;
            case 2:
                comecaAqui = 6;
                break;
            case 3:
                comecaAqui = 12;
                break;
            case 4:
                comecaAqui = 18;
                break;
            case 5:
                comecaAqui = 24;
                break;
            case 6:
                comecaAqui = 30;
                break;
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
