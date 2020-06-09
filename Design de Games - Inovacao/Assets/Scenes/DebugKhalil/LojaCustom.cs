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
    public Sprite[] spriteBone; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19



    [Header("Número de botões que irão spawnar")]
    public int quantidadeDeModelos;


    [Header("Custom2D Script")]
    public Custom2D customizaScript;



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

        int a, b, c, d, e, f;


        if (ativaParametro == true)
        {
            iniciaAqui = comecaAqui;
        }

        //Desativa todos os game objects para caso algum deles tenha ficado ativo sem querer
        for(int k = 0; k < botaoDeModelo.Length; k++)
        {
            botaoDeModelo[k].gameObject.SetActive(false);
            botaoDeModelo[k].onClick.RemoveAllListeners();
        }

        for (int i = 0; i < quantidadeDeModelos; i++)
        {
            switch (qualParteVaiSer)//Decide qual sprite vai aparecer nos botões de Modelo
            {
                case 1:
                    if(iniciaAqui < spriteCabelo.Length)
                    {
                        if (iniciaAqui == 0 || iniciaAqui == 6 || iniciaAqui == 12 || iniciaAqui == 18 || iniciaAqui == 24 || iniciaAqui == 30)
                        {
                            a = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteCabelo[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, a); });
                        }
                        else if (iniciaAqui == 1 || iniciaAqui == 7 || iniciaAqui == 13 || iniciaAqui == 19 || iniciaAqui == 25 || iniciaAqui == 31)
                        {
                            b = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteCabelo[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, b); });
                        }
                        else if (iniciaAqui == 2 || iniciaAqui == 8 || iniciaAqui == 14 || iniciaAqui == 20 || iniciaAqui == 26 || iniciaAqui == 32)
                        {
                            c = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteCabelo[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, c); });
                        }
                        else if (iniciaAqui == 3 || iniciaAqui == 9 || iniciaAqui == 15 || iniciaAqui == 21 || iniciaAqui == 27 || iniciaAqui == 33)
                        {
                            d = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteCabelo[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, d); });
                        }
                        else if (iniciaAqui == 4 || iniciaAqui == 10 || iniciaAqui == 16 || iniciaAqui == 22 || iniciaAqui == 28 || iniciaAqui == 34)
                        {
                            e = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteCabelo[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, e); });
                        }
                        else if (iniciaAqui == 5 || iniciaAqui == 11 || iniciaAqui == 17 || iniciaAqui == 23 || iniciaAqui == 29 || iniciaAqui == 35)
                        {
                            f = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteCabelo[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, f); });
                        }
                    }
                    break;
                case 2:
                    if (iniciaAqui < spriteShirt.Length)
                    {
                        if (iniciaAqui == 0 || iniciaAqui == 6 || iniciaAqui == 12 || iniciaAqui == 18 || iniciaAqui == 24 || iniciaAqui == 30)
                        {
                            a = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShirt[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, a); });
                        }
                        else if (iniciaAqui == 1 || iniciaAqui == 7 || iniciaAqui == 13 || iniciaAqui == 19 || iniciaAqui == 25 || iniciaAqui == 31)
                        {
                            b = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShirt[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, b); });
                        }
                        else if (iniciaAqui == 2 || iniciaAqui == 8 || iniciaAqui == 14 || iniciaAqui == 20 || iniciaAqui == 26 || iniciaAqui == 32)
                        {
                            c = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShirt[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, c); });
                        }
                        else if (iniciaAqui == 3 || iniciaAqui == 9 || iniciaAqui == 15 || iniciaAqui == 21 || iniciaAqui == 27 || iniciaAqui == 33)
                        {
                            d = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShirt[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, d); });
                        }
                        else if (iniciaAqui == 4 || iniciaAqui == 10 || iniciaAqui == 16 || iniciaAqui == 22 || iniciaAqui == 28 || iniciaAqui == 34)
                        {
                            e = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShirt[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, e); });
                        }
                        else if (iniciaAqui == 5 || iniciaAqui == 11 || iniciaAqui == 17 || iniciaAqui == 23 || iniciaAqui == 29 || iniciaAqui == 35)
                        {
                            f = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShirt[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, f); });
                        }
                    }
                    break;
                case 3:
                    if (iniciaAqui < spriteTenis.Length)
                    {
                        if (iniciaAqui == 0 || iniciaAqui == 6 || iniciaAqui == 12 || iniciaAqui == 18 || iniciaAqui == 24 || iniciaAqui == 30)
                        {
                            a = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteTenis[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, a); });
                        }
                        else if (iniciaAqui == 1 || iniciaAqui == 7 || iniciaAqui == 13 || iniciaAqui == 19 || iniciaAqui == 25 || iniciaAqui == 31)
                        {
                            b = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteTenis[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, b); });
                        }
                        else if (iniciaAqui == 2 || iniciaAqui == 8 || iniciaAqui == 14 || iniciaAqui == 20 || iniciaAqui == 26 || iniciaAqui == 32)
                        {
                            c = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteTenis[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, c); });
                        }
                        else if (iniciaAqui == 3 || iniciaAqui == 9 || iniciaAqui == 15 || iniciaAqui == 21 || iniciaAqui == 27 || iniciaAqui == 33)
                        {
                            d = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteTenis[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, d); });
                        }
                        else if (iniciaAqui == 4 || iniciaAqui == 10 || iniciaAqui == 16 || iniciaAqui == 22 || iniciaAqui == 28 || iniciaAqui == 34)
                        {
                            e = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteTenis[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, e); });
                        }
                        else if (iniciaAqui == 5 || iniciaAqui == 11 || iniciaAqui == 17 || iniciaAqui == 23 || iniciaAqui == 29 || iniciaAqui == 35)
                        {
                            f = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteTenis[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, f); });
                        }
                    }
                    break;
                case 4:
                    if (iniciaAqui < spriteShorts.Length)
                    {
                        if (iniciaAqui == 0 || iniciaAqui == 6 || iniciaAqui == 12 || iniciaAqui == 18 || iniciaAqui == 24 || iniciaAqui == 30)
                        {
                            a = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShorts[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, a); });
                        }
                        else if (iniciaAqui == 1 || iniciaAqui == 7 || iniciaAqui == 13 || iniciaAqui == 19 || iniciaAqui == 25 || iniciaAqui == 31)
                        {
                            b = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShorts[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, b); });
                        }
                        else if (iniciaAqui == 2 || iniciaAqui == 8 || iniciaAqui == 14 || iniciaAqui == 20 || iniciaAqui == 26 || iniciaAqui == 32)
                        {
                            c = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShorts[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, c); });
                        }
                        else if (iniciaAqui == 3 || iniciaAqui == 9 || iniciaAqui == 15 || iniciaAqui == 21 || iniciaAqui == 27 || iniciaAqui == 33)
                        {
                            d = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShorts[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, d); });
                        }
                        else if (iniciaAqui == 4 || iniciaAqui == 10 || iniciaAqui == 16 || iniciaAqui == 22 || iniciaAqui == 28 || iniciaAqui == 34)
                        {
                            e = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShorts[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, e); });
                        }
                        else if (iniciaAqui == 5 || iniciaAqui == 11 || iniciaAqui == 17 || iniciaAqui == 23 || iniciaAqui == 29 || iniciaAqui == 35)
                        {
                            f = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShorts[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, f); });
                        }
                    }
                    break;
                case 5:
                    if (iniciaAqui < spriteOculos.Length)
                    {
                        if (iniciaAqui == 0 || iniciaAqui == 6 || iniciaAqui == 12 || iniciaAqui == 18 || iniciaAqui == 24 || iniciaAqui == 30)
                        {
                            a = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteOculos[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, a); });
                        }
                        else if (iniciaAqui == 1 || iniciaAqui == 7 || iniciaAqui == 13 || iniciaAqui == 19 || iniciaAqui == 25 || iniciaAqui == 31)
                        {
                            b = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteOculos[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, b); });
                        }
                        else if (iniciaAqui == 2 || iniciaAqui == 8 || iniciaAqui == 14 || iniciaAqui == 20 || iniciaAqui == 26 || iniciaAqui == 32)
                        {
                            c = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteOculos[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, c); });
                        }
                        else if (iniciaAqui == 3 || iniciaAqui == 9 || iniciaAqui == 15 || iniciaAqui == 21 || iniciaAqui == 27 || iniciaAqui == 33)
                        {
                            d = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteOculos[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, d); });
                        }
                        else if (iniciaAqui == 4 || iniciaAqui == 10 || iniciaAqui == 16 || iniciaAqui == 22 || iniciaAqui == 28 || iniciaAqui == 34)
                        {
                            e = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteOculos[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, e); });
                        }
                        else if (iniciaAqui == 5 || iniciaAqui == 11 || iniciaAqui == 17 || iniciaAqui == 23 || iniciaAqui == 29 || iniciaAqui == 35)
                        {
                            f = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteOculos[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, f); });
                        }
                    }
                    break;
                case 6:
                    if (iniciaAqui < spriteDelineado.Length)
                    {
                        if (iniciaAqui == 0 || iniciaAqui == 6 || iniciaAqui == 12 || iniciaAqui == 18 || iniciaAqui == 24 || iniciaAqui == 30)
                        {
                            a = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteDelineado[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, a); });
                        }
                        else if (iniciaAqui == 1 || iniciaAqui == 7 || iniciaAqui == 13 || iniciaAqui == 19 || iniciaAqui == 25 || iniciaAqui == 31)
                        {
                            b = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteDelineado[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, b); });
                        }
                        else if (iniciaAqui == 2 || iniciaAqui == 8 || iniciaAqui == 14 || iniciaAqui == 20 || iniciaAqui == 26 || iniciaAqui == 32)
                        {
                            c = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteDelineado[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, c); });
                        }
                        else if (iniciaAqui == 3 || iniciaAqui == 9 || iniciaAqui == 15 || iniciaAqui == 21 || iniciaAqui == 27 || iniciaAqui == 33)
                        {
                            d = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteDelineado[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, d); });
                        }
                        else if (iniciaAqui == 4 || iniciaAqui == 10 || iniciaAqui == 16 || iniciaAqui == 22 || iniciaAqui == 28 || iniciaAqui == 34)
                        {
                            e = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteDelineado[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, e); });
                        }
                        else if (iniciaAqui == 5 || iniciaAqui == 11 || iniciaAqui == 17 || iniciaAqui == 23 || iniciaAqui == 29 || iniciaAqui == 35)
                        {
                            f = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteDelineado[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, f); });
                        }
                    }
                    break;
                case 7:
                    if (iniciaAqui < spriteMascara.Length)
                    {
                        if (iniciaAqui == 0 || iniciaAqui == 6 || iniciaAqui == 12 || iniciaAqui == 18 || iniciaAqui == 24 || iniciaAqui == 30)
                        {
                            a = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteMascara[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, a); });
                        }
                        else if (iniciaAqui == 1 || iniciaAqui == 7 || iniciaAqui == 13 || iniciaAqui == 19 || iniciaAqui == 25 || iniciaAqui == 31)
                        {
                            b = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteMascara[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, b); });
                        }
                        else if (iniciaAqui == 2 || iniciaAqui == 8 || iniciaAqui == 14 || iniciaAqui == 20 || iniciaAqui == 26 || iniciaAqui == 32)
                        {
                            c = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteMascara[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, c); });
                        }
                        else if (iniciaAqui == 3 || iniciaAqui == 9 || iniciaAqui == 15 || iniciaAqui == 21 || iniciaAqui == 27 || iniciaAqui == 33)
                        {
                            d = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteMascara[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, d); });
                        }
                        else if (iniciaAqui == 4 || iniciaAqui == 10 || iniciaAqui == 16 || iniciaAqui == 22 || iniciaAqui == 28 || iniciaAqui == 34)
                        {
                            e = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteMascara[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, e); });
                        }
                        else if (iniciaAqui == 5 || iniciaAqui == 11 || iniciaAqui == 17 || iniciaAqui == 23 || iniciaAqui == 29 || iniciaAqui == 35)
                        {
                            f = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteMascara[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, f); });
                        }
                    }
                    break;
                case 8:
                    if (iniciaAqui < spriteBone.Length)
                    {
                        if (iniciaAqui == 0 || iniciaAqui == 6 || iniciaAqui == 12 || iniciaAqui == 18 || iniciaAqui == 24 || iniciaAqui == 30)
                        {
                            a = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteBone[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, a); });
                        }
                        else if (iniciaAqui == 1 || iniciaAqui == 7 || iniciaAqui == 13 || iniciaAqui == 19 || iniciaAqui == 25 || iniciaAqui == 31)
                        {
                            b = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteBone[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, b); });
                        }
                        else if (iniciaAqui == 2 || iniciaAqui == 8 || iniciaAqui == 14 || iniciaAqui == 20 || iniciaAqui == 26 || iniciaAqui == 32)
                        {
                            c = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteBone[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, c); });
                        }
                        else if (iniciaAqui == 3 || iniciaAqui == 9 || iniciaAqui == 15 || iniciaAqui == 21 || iniciaAqui == 27 || iniciaAqui == 33)
                        {
                            d = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteBone[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, d); });
                        }
                        else if (iniciaAqui == 4 || iniciaAqui == 10 || iniciaAqui == 16 || iniciaAqui == 22 || iniciaAqui == 28 || iniciaAqui == 34)
                        {
                            e = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteBone[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, e); });
                        }
                        else if (iniciaAqui == 5 || iniciaAqui == 11 || iniciaAqui == 17 || iniciaAqui == 23 || iniciaAqui == 29 || iniciaAqui == 35)
                        {
                            f = iniciaAqui;
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteBone[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, f); });
                        }
                    }
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












    public void QualParteVaiMudar(int selectedPart, int index)//Envia a escolha de forma definitiva do resultado da combinação (Parte, Modelo e Cor) e efetiva no personagem
    {
        switch (selectedPart)
        {
            case 1:
                customizaScript.ChangeHair(index);
                customizaScript.ChangeBone(10);
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
            case 5:
                customizaScript.ChangeOculos(index);
                customizaScript.ChangeCilios(3);
                break;
            case 6:
                customizaScript.ChangeCilios(index);
                break;
            case 7:
                customizaScript.ChangeMask(index);
                break;
            case 8:
                customizaScript.ChangeHair(21);
                customizaScript.ChangeBone(index);
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
