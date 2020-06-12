using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LojaCustom : MonoBehaviour
{
    [Header("Informação sobre qual círculo deve ser ativado")]

    public Prop2D hairInd;
    public Prop2D shirtInd;
    public Prop2D shortsInd;
    public Prop2D shoesInd;
    public Prop2D oculosIndex;
    public Prop2D ciliosIndex;
    public Prop2D maskIndex;
    public Prop2D boneIndex;
    public Prop2D skinInd;
    public Prop2D pupilaInd;
    public Prop2D sobrancelhaInd;


    [Header("Sobrancelha")]
    public int[] qualSobrancelhaCabelo; //Coloca os números aqui
    public int[] qualSobrancelhaBone; //Coloca os números aqui

    public GameObject[] sobrancelha1Esq;
    public GameObject[] sobrancelha1Dir;
    public GameObject[] sobrancelha2Esq;



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
    public Sprite[] spriteSkin; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19
    public Sprite[] spritePupila; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19



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


    [Header("Armazena valor do círculo verde")]
    public int circuloVerde;


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
                    if (iniciaAqui < spriteCabelo.Length)
                    {
                        if (iniciaAqui == 0 || iniciaAqui == 6 || iniciaAqui == 12 || iniciaAqui == 18 || iniciaAqui == 24 || iniciaAqui == 30)
                        {
                            a = iniciaAqui;
                            if (a == hairInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }

                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteCabelo[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, a); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(a, 0, hairInd); });
                        }
                        else if (iniciaAqui == 1 || iniciaAqui == 7 || iniciaAqui == 13 || iniciaAqui == 19 || iniciaAqui == 25 || iniciaAqui == 31)
                        {
                            b = iniciaAqui;
                            if (b == hairInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteCabelo[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, b); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(b, 1, hairInd); });
                        }
                        else if (iniciaAqui == 2 || iniciaAqui == 8 || iniciaAqui == 14 || iniciaAqui == 20 || iniciaAqui == 26 || iniciaAqui == 32)
                        {
                            c = iniciaAqui;
                            if (c == hairInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteCabelo[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, c); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(c, 2, hairInd); });
                        }
                        else if (iniciaAqui == 3 || iniciaAqui == 9 || iniciaAqui == 15 || iniciaAqui == 21 || iniciaAqui == 27 || iniciaAqui == 33)
                        {
                            d = iniciaAqui;
                            if (d == hairInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteCabelo[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, d); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(d, 3, hairInd); });
                        }
                        else if (iniciaAqui == 4 || iniciaAqui == 10 || iniciaAqui == 16 || iniciaAqui == 22 || iniciaAqui == 28 || iniciaAqui == 34)
                        {
                            e = iniciaAqui;
                            if (e == hairInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteCabelo[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, e); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(e, 4, hairInd); });
                        }
                        else if (iniciaAqui == 5 || iniciaAqui == 11 || iniciaAqui == 17 || iniciaAqui == 23 || iniciaAqui == 29 || iniciaAqui == 35)
                        {
                            f = iniciaAqui;
                            if (f == hairInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteCabelo[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, f); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(f, 5, hairInd); });
                        }
                    }
                    break;
                case 2:
                    if (iniciaAqui < spriteShirt.Length)
                    {
                        if (iniciaAqui == 0 || iniciaAqui == 6 || iniciaAqui == 12 || iniciaAqui == 18 || iniciaAqui == 24 || iniciaAqui == 30)
                        {
                            a = iniciaAqui;
                            if (a == shirtInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShirt[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, a); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(a, 0, shirtInd); });
                        }
                        else if (iniciaAqui == 1 || iniciaAqui == 7 || iniciaAqui == 13 || iniciaAqui == 19 || iniciaAqui == 25 || iniciaAqui == 31)
                        {
                            b = iniciaAqui;
                            if (b == shirtInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShirt[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, b); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(b, 1, shirtInd); });
                        }
                        else if (iniciaAqui == 2 || iniciaAqui == 8 || iniciaAqui == 14 || iniciaAqui == 20 || iniciaAqui == 26 || iniciaAqui == 32)
                        {
                            c = iniciaAqui;
                            if (c == shirtInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShirt[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, c); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(c, 2, shirtInd); });
                        }
                        else if (iniciaAqui == 3 || iniciaAqui == 9 || iniciaAqui == 15 || iniciaAqui == 21 || iniciaAqui == 27 || iniciaAqui == 33)
                        {
                            d = iniciaAqui;
                            if (d == shirtInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShirt[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, d); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(d, 3, shirtInd); });
                        }
                        else if (iniciaAqui == 4 || iniciaAqui == 10 || iniciaAqui == 16 || iniciaAqui == 22 || iniciaAqui == 28 || iniciaAqui == 34)
                        {
                            e = iniciaAqui;
                            if (e == shirtInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShirt[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, e); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(e, 4, shirtInd); });
                        }
                        else if (iniciaAqui == 5 || iniciaAqui == 11 || iniciaAqui == 17 || iniciaAqui == 23 || iniciaAqui == 29 || iniciaAqui == 35)
                        {
                            f = iniciaAqui;
                            if (f == shirtInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShirt[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, f); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(f, 5, shirtInd); });
                        }
                    }
                    break;
                case 3:
                    if (iniciaAqui < spriteTenis.Length)
                    {
                        if (iniciaAqui == 0 || iniciaAqui == 6 || iniciaAqui == 12 || iniciaAqui == 18 || iniciaAqui == 24 || iniciaAqui == 30)
                        {
                            a = iniciaAqui;
                            if (a == shoesInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteTenis[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, a); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(a, 0, shoesInd); });
                        }
                        else if (iniciaAqui == 1 || iniciaAqui == 7 || iniciaAqui == 13 || iniciaAqui == 19 || iniciaAqui == 25 || iniciaAqui == 31)
                        {
                            b = iniciaAqui;
                            if (b == shoesInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteTenis[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, b); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(b, 1, shoesInd); });
                        }
                        else if (iniciaAqui == 2 || iniciaAqui == 8 || iniciaAqui == 14 || iniciaAqui == 20 || iniciaAqui == 26 || iniciaAqui == 32)
                        {
                            c = iniciaAqui;
                            if (c == shoesInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteTenis[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, c); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(c, 2, shoesInd); });
                        }
                        else if (iniciaAqui == 3 || iniciaAqui == 9 || iniciaAqui == 15 || iniciaAqui == 21 || iniciaAqui == 27 || iniciaAqui == 33)
                        {
                            d = iniciaAqui;
                            if (d == shoesInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteTenis[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, d); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(d, 3, shoesInd); });
                        }
                        else if (iniciaAqui == 4 || iniciaAqui == 10 || iniciaAqui == 16 || iniciaAqui == 22 || iniciaAqui == 28 || iniciaAqui == 34)
                        {
                            e = iniciaAqui;
                            if (e == shoesInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteTenis[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, e); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(e, 4, shoesInd); });
                        }
                        else if (iniciaAqui == 5 || iniciaAqui == 11 || iniciaAqui == 17 || iniciaAqui == 23 || iniciaAqui == 29 || iniciaAqui == 35)
                        {
                            f = iniciaAqui;
                            if (f == shoesInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteTenis[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, f); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(f, 5, shoesInd); });
                        }
                    }
                    break;
                case 4:
                    if (iniciaAqui < spriteShorts.Length)
                    {
                        if (iniciaAqui == 0 || iniciaAqui == 6 || iniciaAqui == 12 || iniciaAqui == 18 || iniciaAqui == 24 || iniciaAqui == 30)
                        {
                            a = iniciaAqui;
                            if (a == shortsInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShorts[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, a); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(a, 0, shortsInd); });
                        }
                        else if (iniciaAqui == 1 || iniciaAqui == 7 || iniciaAqui == 13 || iniciaAqui == 19 || iniciaAqui == 25 || iniciaAqui == 31)
                        {
                            b = iniciaAqui;
                            if (b == shortsInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShorts[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, b); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(b, 1, shortsInd); });
                        }
                        else if (iniciaAqui == 2 || iniciaAqui == 8 || iniciaAqui == 14 || iniciaAqui == 20 || iniciaAqui == 26 || iniciaAqui == 32)
                        {
                            c = iniciaAqui;
                            if (c == shortsInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShorts[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, c); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(c, 2, shortsInd); });
                        }
                        else if (iniciaAqui == 3 || iniciaAqui == 9 || iniciaAqui == 15 || iniciaAqui == 21 || iniciaAqui == 27 || iniciaAqui == 33)
                        {
                            d = iniciaAqui;
                            if (d == shortsInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShorts[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, d); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(d, 3, shortsInd); });
                        }
                        else if (iniciaAqui == 4 || iniciaAqui == 10 || iniciaAqui == 16 || iniciaAqui == 22 || iniciaAqui == 28 || iniciaAqui == 34)
                        {
                            e = iniciaAqui;
                            if (e == shortsInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShorts[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, e); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(e, 4, shortsInd); });
                        }
                        else if (iniciaAqui == 5 || iniciaAqui == 11 || iniciaAqui == 17 || iniciaAqui == 23 || iniciaAqui == 29 || iniciaAqui == 35)
                        {
                            f = iniciaAqui;
                            if (f == shortsInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteShorts[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, f); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(f, 5, shortsInd); });
                        }
                    }
                    break;
                case 5:
                    if (iniciaAqui < spriteOculos.Length)
                    {
                        if (iniciaAqui == 0 || iniciaAqui == 6 || iniciaAqui == 12 || iniciaAqui == 18 || iniciaAqui == 24 || iniciaAqui == 30)
                        {
                            a = iniciaAqui;
                            if (a == oculosIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteOculos[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, a); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(a, 0, oculosIndex); });
                        }
                        else if (iniciaAqui == 1 || iniciaAqui == 7 || iniciaAqui == 13 || iniciaAqui == 19 || iniciaAqui == 25 || iniciaAqui == 31)
                        {
                            b = iniciaAqui;
                            if (b == oculosIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteOculos[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, b); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(b, 1, oculosIndex); });
                        }
                        else if (iniciaAqui == 2 || iniciaAqui == 8 || iniciaAqui == 14 || iniciaAqui == 20 || iniciaAqui == 26 || iniciaAqui == 32)
                        {
                            c = iniciaAqui;
                            if (c == oculosIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteOculos[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, c); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(c, 2, oculosIndex); });
                        }
                        else if (iniciaAqui == 3 || iniciaAqui == 9 || iniciaAqui == 15 || iniciaAqui == 21 || iniciaAqui == 27 || iniciaAqui == 33)
                        {
                            d = iniciaAqui;
                            if (d == oculosIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteOculos[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, d); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(d, 3, oculosIndex); });
                        }
                        else if (iniciaAqui == 4 || iniciaAqui == 10 || iniciaAqui == 16 || iniciaAqui == 22 || iniciaAqui == 28 || iniciaAqui == 34)
                        {
                            e = iniciaAqui;
                            if (e == oculosIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteOculos[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, e); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(e, 4, oculosIndex); });
                        }
                        else if (iniciaAqui == 5 || iniciaAqui == 11 || iniciaAqui == 17 || iniciaAqui == 23 || iniciaAqui == 29 || iniciaAqui == 35)
                        {
                            f = iniciaAqui;
                            if (f == oculosIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteOculos[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, f); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(f, 5, oculosIndex); });
                        }
                    }
                    break;
                case 6:
                    if (iniciaAqui < spriteDelineado.Length)
                    {
                        if (iniciaAqui == 0 || iniciaAqui == 6 || iniciaAqui == 12 || iniciaAqui == 18 || iniciaAqui == 24 || iniciaAqui == 30)
                        {
                            a = iniciaAqui;
                            if (a == ciliosIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteDelineado[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, a); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(a, 0, ciliosIndex); });
                        }
                        else if (iniciaAqui == 1 || iniciaAqui == 7 || iniciaAqui == 13 || iniciaAqui == 19 || iniciaAqui == 25 || iniciaAqui == 31)
                        {
                            b = iniciaAqui;
                            if (b == ciliosIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteDelineado[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, b); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(b, 1, ciliosIndex); });
                        }
                        else if (iniciaAqui == 2 || iniciaAqui == 8 || iniciaAqui == 14 || iniciaAqui == 20 || iniciaAqui == 26 || iniciaAqui == 32)
                        {
                            c = iniciaAqui;
                            if (c == ciliosIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteDelineado[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, c); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(c, 2, ciliosIndex); });
                        }
                        else if (iniciaAqui == 3 || iniciaAqui == 9 || iniciaAqui == 15 || iniciaAqui == 21 || iniciaAqui == 27 || iniciaAqui == 33)
                        {
                            d = iniciaAqui;
                            if (d == ciliosIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteDelineado[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, d); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(d, 3, ciliosIndex); });
                        }
                        else if (iniciaAqui == 4 || iniciaAqui == 10 || iniciaAqui == 16 || iniciaAqui == 22 || iniciaAqui == 28 || iniciaAqui == 34)
                        {
                            e = iniciaAqui;
                            if (e == ciliosIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteDelineado[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, e); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(e, 4, ciliosIndex); });
                        }
                        else if (iniciaAqui == 5 || iniciaAqui == 11 || iniciaAqui == 17 || iniciaAqui == 23 || iniciaAqui == 29 || iniciaAqui == 35)
                        {
                            f = iniciaAqui;
                            if (f == ciliosIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteDelineado[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, f); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(f, 5, ciliosIndex); });
                        }
                    }
                    break;
                case 7:
                    if (iniciaAqui < spriteMascara.Length)
                    {
                        if (iniciaAqui == 0 || iniciaAqui == 6 || iniciaAqui == 12 || iniciaAqui == 18 || iniciaAqui == 24 || iniciaAqui == 30)
                        {
                            a = iniciaAqui;
                            if (a == maskIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteMascara[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, a); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(a, 0, maskIndex); });
                        }
                        else if (iniciaAqui == 1 || iniciaAqui == 7 || iniciaAqui == 13 || iniciaAqui == 19 || iniciaAqui == 25 || iniciaAqui == 31)
                        {
                            b = iniciaAqui;
                            if (b == maskIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteMascara[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, b); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(b, 1, maskIndex); });
                        }
                        else if (iniciaAqui == 2 || iniciaAqui == 8 || iniciaAqui == 14 || iniciaAqui == 20 || iniciaAqui == 26 || iniciaAqui == 32)
                        {
                            c = iniciaAqui;
                            if (c == maskIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteMascara[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, c); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(c, 2, maskIndex); });
                        }
                        else if (iniciaAqui == 3 || iniciaAqui == 9 || iniciaAqui == 15 || iniciaAqui == 21 || iniciaAqui == 27 || iniciaAqui == 33)
                        {
                            d = iniciaAqui;
                            if (d == maskIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteMascara[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, d); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(d, 3, maskIndex); });
                        }
                        else if (iniciaAqui == 4 || iniciaAqui == 10 || iniciaAqui == 16 || iniciaAqui == 22 || iniciaAqui == 28 || iniciaAqui == 34)
                        {
                            e = iniciaAqui;
                            if (e == maskIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteMascara[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, e); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(e, 4, maskIndex); });
                        }
                        else if (iniciaAqui == 5 || iniciaAqui == 11 || iniciaAqui == 17 || iniciaAqui == 23 || iniciaAqui == 29 || iniciaAqui == 35)
                        {
                            f = iniciaAqui;
                            if (f == maskIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteMascara[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, f); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(f, 5, maskIndex); });
                        }
                    }
                    break;
                case 8:
                    if (iniciaAqui < spriteBone.Length)
                    {
                        if (iniciaAqui == 0 || iniciaAqui == 6 || iniciaAqui == 12 || iniciaAqui == 18 || iniciaAqui == 24 || iniciaAqui == 30)
                        {
                            a = iniciaAqui;
                            if (a == boneIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteBone[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, a); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(a, 0, boneIndex); });
                        }
                        else if (iniciaAqui == 1 || iniciaAqui == 7 || iniciaAqui == 13 || iniciaAqui == 19 || iniciaAqui == 25 || iniciaAqui == 31)
                        {
                            b = iniciaAqui;
                            if (b == boneIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteBone[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, b); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(b, 1, boneIndex); });
                        }
                        else if (iniciaAqui == 2 || iniciaAqui == 8 || iniciaAqui == 14 || iniciaAqui == 20 || iniciaAqui == 26 || iniciaAqui == 32)
                        {
                            c = iniciaAqui;
                            if (c == boneIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteBone[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, c); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(c, 2, boneIndex); });
                        }
                        else if (iniciaAqui == 3 || iniciaAqui == 9 || iniciaAqui == 15 || iniciaAqui == 21 || iniciaAqui == 27 || iniciaAqui == 33)
                        {
                            d = iniciaAqui;
                            if (d == boneIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteBone[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, d); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(d, 3, boneIndex); });
                        }
                        else if (iniciaAqui == 4 || iniciaAqui == 10 || iniciaAqui == 16 || iniciaAqui == 22 || iniciaAqui == 28 || iniciaAqui == 34)
                        {
                            e = iniciaAqui;
                            if (e == boneIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteBone[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, e); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(e, 4, boneIndex); });
                        }
                        else if (iniciaAqui == 5 || iniciaAqui == 11 || iniciaAqui == 17 || iniciaAqui == 23 || iniciaAqui == 29 || iniciaAqui == 35)
                        {
                            f = iniciaAqui;
                            if (f == boneIndex.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteBone[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, f); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(f, 5, boneIndex); });
                        }
                    }
                    break;
                case 9:
                    if (iniciaAqui < spriteSkin.Length)
                    {
                        if (iniciaAqui == 0 || iniciaAqui == 6 || iniciaAqui == 12 || iniciaAqui == 18 || iniciaAqui == 24 || iniciaAqui == 30)
                        {
                            a = iniciaAqui;
                            if (a == skinInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteSkin[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, a); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(a, 0, skinInd); });
                        }
                        else if (iniciaAqui == 1 || iniciaAqui == 7 || iniciaAqui == 13 || iniciaAqui == 19 || iniciaAqui == 25 || iniciaAqui == 31)
                        {
                            b = iniciaAqui;
                            if (b == skinInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteSkin[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, b); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(b, 1, skinInd); });
                        }
                        else if (iniciaAqui == 2 || iniciaAqui == 8 || iniciaAqui == 14 || iniciaAqui == 20 || iniciaAqui == 26 || iniciaAqui == 32)
                        {
                            c = iniciaAqui;
                            if (c == skinInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteSkin[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, c); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(c, 2, skinInd); });
                        }
                        else if (iniciaAqui == 3 || iniciaAqui == 9 || iniciaAqui == 15 || iniciaAqui == 21 || iniciaAqui == 27 || iniciaAqui == 33)
                        {
                            d = iniciaAqui;
                            if (d == skinInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteSkin[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, d); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(d, 3, skinInd); });
                        }
                        else if (iniciaAqui == 4 || iniciaAqui == 10 || iniciaAqui == 16 || iniciaAqui == 22 || iniciaAqui == 28 || iniciaAqui == 34)
                        {
                            e = iniciaAqui;
                            if (e == skinInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteSkin[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, e); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(e, 4, skinInd); });
                        }
                        else if (iniciaAqui == 5 || iniciaAqui == 11 || iniciaAqui == 17 || iniciaAqui == 23 || iniciaAqui == 29 || iniciaAqui == 35)
                        {
                            f = iniciaAqui;
                            if (f == skinInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spriteSkin[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, f); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(f, 5, skinInd); });
                        }
                    }
                    break;
                case 10:
                    if (iniciaAqui < spritePupila.Length)
                    {
                        if (iniciaAqui == 0 || iniciaAqui == 6 || iniciaAqui == 12 || iniciaAqui == 18 || iniciaAqui == 24 || iniciaAqui == 30)
                        {
                            a = iniciaAqui;
                            if (a == pupilaInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spritePupila[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, a); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(a, 0, pupilaInd); });
                        }
                        else if (iniciaAqui == 1 || iniciaAqui == 7 || iniciaAqui == 13 || iniciaAqui == 19 || iniciaAqui == 25 || iniciaAqui == 31)
                        {
                            b = iniciaAqui;
                            if (b == pupilaInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spritePupila[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, b); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(b, 1, pupilaInd); });
                        }
                        else if (iniciaAqui == 2 || iniciaAqui == 8 || iniciaAqui == 14 || iniciaAqui == 20 || iniciaAqui == 26 || iniciaAqui == 32)
                        {
                            c = iniciaAqui;
                            if (c == pupilaInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spritePupila[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, c); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(c, 2, pupilaInd); });
                        }
                        else if (iniciaAqui == 3 || iniciaAqui == 9 || iniciaAqui == 15 || iniciaAqui == 21 || iniciaAqui == 27 || iniciaAqui == 33)
                        {
                            d = iniciaAqui;
                            if (d == pupilaInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spritePupila[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, d); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(d, 3, pupilaInd); });
                        }
                        else if (iniciaAqui == 4 || iniciaAqui == 10 || iniciaAqui == 16 || iniciaAqui == 22 || iniciaAqui == 28 || iniciaAqui == 34)
                        {
                            e = iniciaAqui;
                            if (e == pupilaInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spritePupila[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, e); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(e, 4, pupilaInd); });
                        }
                        else if (iniciaAqui == 5 || iniciaAqui == 11 || iniciaAqui == 17 || iniciaAqui == 23 || iniciaAqui == 29 || iniciaAqui == 35)
                        {
                            f = iniciaAqui;
                            if (f == pupilaInd.prop2DInd)
                            {
                                circuloDeSelecaoModelo[i].SetActive(true);
                            }
                            else
                            {
                                circuloDeSelecaoModelo[i].SetActive(false);
                            }
                            botaoDeModelo[i].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
                            botaoDeModelo[i].image.sprite = spritePupila[iniciaAqui];
                            botaoDeModelo[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, f); });
                            botaoDeModelo[i].onClick.AddListener(delegate { AtivaCirculoVerde(f, 5, pupilaInd); });
                        }
                    }
                    break;
            }
            iniciaAqui++;
        }
        ativaParametro = false;
    }


    public void AtivaCirculoVerde(int qualCirculo, int qualDosSeis, Prop2D qualProp)
    {
        for (int i = 0; i < circuloDeSelecaoModelo.Length; i++)
        {
            if(qualProp.prop2DInd == qualCirculo && i == qualDosSeis)
            {
                circuloDeSelecaoModelo[i].gameObject.SetActive(true);
            }
            else
            {
                circuloDeSelecaoModelo[i].gameObject.SetActive(false);
            }
        }
    }



    public void SetMinLoop(int min)
    {
        maximoLoop = min;
    }
    public void SetMaxLoop(int max)
    {
        maximoLoop = max;
    }


    public void FrocaModeloLimite(int quantos)
    {
        quantidadeDeModelos = quantos;
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
                customizaScript.ChangeSobrancelha(qualSobrancelhaCabelo[index]);
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
                //customizaScript.ChangeCilios(0);
                break;
            case 6:
                customizaScript.ChangeOculos(0);
                customizaScript.ChangeCilios(index);
                break;
            case 7:
                customizaScript.ChangeMask(index);
                break;
            case 8:
                customizaScript.ChangeSobrancelha(qualSobrancelhaBone[index]);
                customizaScript.ChangeHair(21);
                customizaScript.ChangeBone(index);
                break;
            case 9:
                customizaScript.ChangeSkin(index);
                break;
            case 10:
                customizaScript.ChangePupila(index);
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

