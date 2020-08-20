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

	[Header("Itens Bloqueados")] //Quais itens estarão bloqueados //ainda não foi passado pro PlayerPrefs, deve ser passado
	public bool[] blockedCabelo;
	public bool[] blockedShirt;
	public bool[] blockedShorts;
	public bool[] blockedTenis;
	public bool[] blockedOculos;
	public bool[] blockedDelineado;
	public bool[] blockedMascara;
	public bool[] blockedBone;
	public bool[] blockedSkin;
	public bool[] blockedPupila;


	[Header("Prefab do botão a bloquear e comprar")]
	public GameObject buttonBuy;


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
		for (int k = 0; k < botaoDeModelo.Length; k++)
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
						SwitchIniciaAqui(iniciaAqui, i, spriteCabelo, hairInd);
					}
					break;
				case 2:
					if (iniciaAqui < spriteShirt.Length)
					{
						SwitchIniciaAqui(iniciaAqui, i, spriteShirt, shirtInd);
					}
					break;
				case 3:
					if (iniciaAqui < spriteTenis.Length)
					{
						SwitchIniciaAqui(iniciaAqui, i, spriteTenis, shoesInd);
					}
					break;
				case 4:
					if (iniciaAqui < spriteShorts.Length)
					{
						SwitchIniciaAqui(iniciaAqui, i, spriteShorts, shortsInd);
					}
					break;
				case 5:
					if (iniciaAqui < spriteOculos.Length)
					{
						SwitchIniciaAqui(iniciaAqui, i, spriteOculos, oculosIndex);
					}
					break;
				case 6:
					if (iniciaAqui < spriteDelineado.Length)
					{
						SwitchIniciaAqui(iniciaAqui, i, spriteDelineado, ciliosIndex);
					}
					break;
				case 7:
					if (iniciaAqui < spriteMascara.Length)
					{
						SwitchIniciaAqui(iniciaAqui, i, spriteMascara, maskIndex);
					}
					break;
				case 8:
					if (iniciaAqui < spriteBone.Length)
					{
						SwitchIniciaAqui(iniciaAqui, i, spriteBone, boneIndex);
					}
					break;
				case 9:
					if (iniciaAqui < spriteSkin.Length)
					{
						SwitchIniciaAqui(iniciaAqui, i, spriteSkin, skinInd);
					}
					break;
				case 10:
					if (iniciaAqui < spritePupila.Length)
					{
						SwitchIniciaAqui(iniciaAqui, i, spritePupila, pupilaInd);
					}
					break;
			}
			iniciaAqui++;
		}
		ativaParametro = false;
	}


	

	private void SwitchIniciaAqui(int iniciaAqui, int index, Sprite[] sprite, Prop2D propIndex)
	{
		switch (iniciaAqui)
		{
			case 0:
			case 6:
			case 12:
			case 18:
			case 24:
			case 30:

				buttonManage(iniciaAqui, 0, index, sprite, propIndex);
				break;

			case 1:
			case 7:
			case 13:
			case 19:
			case 25:
			case 31:

				buttonManage(iniciaAqui, 1, index, sprite, propIndex);
				break;

			case 2:
			case 8:
			case 14:
			case 20:
			case 26:
			case 32:

				buttonManage(iniciaAqui, 2, index, sprite, propIndex);
				break;

			case 3:
			case 9:
			case 15:
			case 21:
			case 27:
			case 33:

				buttonManage(iniciaAqui, 3, index, sprite, propIndex);

				break;

			case 4:
			case 10:
			case 16:
			case 22:
			case 28:
			case 34:

				buttonManage(iniciaAqui, 4, index, sprite, propIndex);

				break;

			case 5:
			case 11:
			case 17:
			case 23:
			case 29:
			case 35:

				buttonManage(iniciaAqui, 5, index, sprite, propIndex);

				break;
		}

	}

	private void buttonManage(int iniciaAqui, int numero, int index, Sprite[] sprite, Prop2D propIndex)
	{

		circuloDeSelecaoModelo[index].SetActive(iniciaAqui == propIndex.prop2DInd);

		botaoDeModelo[index].gameObject.SetActive(true);//está sendo ativado aqui porque se não quando passa do limite não funciona
		botaoDeModelo[index].image.sprite = sprite[iniciaAqui];
		botaoDeModelo[index].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, iniciaAqui); });
		botaoDeModelo[index].onClick.AddListener(delegate { AtivaCirculoVerde(iniciaAqui, numero, propIndex); });

	}

	public void AtivaCirculoVerde(int qualCirculo, int qualDosSeis, Prop2D qualProp)
	{
		for (int i = 0; i < circuloDeSelecaoModelo.Length; i++)
		{
			if (qualProp.prop2DInd == qualCirculo && i == qualDosSeis)
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
		if (qualMenu > maximoLoop)
		{
			qualMenu = 1;
		}
		else if (qualMenu < minimoLoop)
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