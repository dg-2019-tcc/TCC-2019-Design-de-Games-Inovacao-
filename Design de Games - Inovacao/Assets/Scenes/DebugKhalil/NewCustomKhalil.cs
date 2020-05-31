using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewCustomKhalil : MonoBehaviour
{
    public bool mudaSpriteCor;


    [Header("Sprites da HUD de botões")]
    public Sprite[] spriteCabelo; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19
    public Sprite[] spriteShirt; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19   
    public Sprite[] spriteShorts; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19
    public Sprite[] spriteTenis; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19

    [Header("Botões da cena")]
    public Button[] botaoDeModelo; //Qual botão dos modelos estamos mexendo
    public Button[] botaoDeCor; //Qual o botão que a gente está mexendo

    [Header("Quantidade de cores que cada modelo tem")]
    public int[] coresCabelo;
    public int[] coresShirt;
    public int[] coresShort;
    public int[] coresTenis;
    
    [Header("Telas De Menu")]
    public GameObject Menu1;
    public GameObject Menu2;

    [Header("Círculo de seleção")]
    public GameObject[] circuloDeSelecaoModelo;
    public GameObject[] circuloDeSelecaoCor;

    public int qualParteVaiSer;
    int qualModeloVaiSer;

    public Custom2D customizaScript;

    public int maiorQuantitadeDeCoresDessaParte;

    int quantasCoresTem; // Fala para dos botões Modelo(2 Level) para os botões Cor (3 Level) quantas cores diferentes existem

    public int quantidadeDeModelos;

    [Header("Botao Next")]
    public GameObject nextButton;

    [Header("Armazena o valor dos botões e efetiva a escolha do modelo")]
    int botaoModelo1;
    int botaoModelo2;
    int botaoModelo3;
    int botaoModelo4;
    int botaoModelo5;

    private void Start()
    {
        EscolheQtdDeBotoesModelo(quantidadeDeModelos);
        nextButton.SetActive(false);
    }


    // Serve para trocar entre as telas dos menus da customização
    public void TrocaDeMenu(int qualMenuAtiva)
    {
        switch (qualMenuAtiva)
        {
            case 1:
                //DesativaAsCores();
                Menu1.SetActive(true);
                Menu2.SetActive(false);
                break;
            case 2:
                Menu1.SetActive(false);
                Menu2.SetActive(true);
                break;
        }
    }



    /// <summary>
    /// 
    /// Recebe a informação das partes (quando é chamado, recebendo a informação através do seu parâmetro
    /// "maiorQuantidadeDeCores" que devem ser associadas uma das variaveis pertencentes ao grupo
    /// "Quantas variações (Cores) o modelo com mais variações tem") sobre qual o modelo 
    /// que tem a maior variedade de cores, e repassa esse valor para o cálculo de inicio do Array
    /// (Para saber aonde o "EscolheQtdCor" irá começar a contar para pegar os sprites corretos) 
    /// 
    /// </summary>
    public void QuantidadeDeCores(int maiorQuantidadeDeCores)
    {
        maiorQuantitadeDeCoresDessaParte = maiorQuantidadeDeCores;
    }



    /// <summary>
    /// 
    /// Indica para os botões de Modelo (2 Level) qual das partes foi selecionada (Cabelo, Camisa, Calça,
    /// Sapato) através da variavel "selectedPart" que pode ser alterada no inspector de cada botão (sendo
    /// esses botões os botões do menu de Partes (1 Level)).
    /// 
    /// Além de também enviar a informação sobre quantas cores diferentes existem no modelo com mais 
    /// variedade dessa parte (Função "QuantidadeDeCores")
    /// 
    /// </summary>

    public void QualParte(int selectedPart) // Vai ter que informar através da porta, talvez separe em código
    {
        switch (selectedPart)
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




    /// <summary>
    /// 
    /// Muda a quantidade de botões que serão mostrados no menu de modelos (2 Level) para mostrar a quantidade
    /// correta (qtdDeModelos) que é escolhida no inspector de cada botão
    /// 
    /// Além disso também muda o sprite que será exibido em cada botão do menu de modelos (2 Level)
    /// 
    /// </summary>
    public void EscolheQtdDeBotoesModelo(int qtdDeModelos)//EscolheQtdBotoesModelos //Decide quantos botões de modelo serão mostrados
    {
        int q = 5;
        for (int i = 0; i < qtdDeModelos; i++)
        {
            botaoDeModelo[i].gameObject.SetActive(true);

            switch (qualParteVaiSer)//Decide qual sprite vai aparecer nos botões de Modelo(2 nível)
            {
                case 1:
                    botaoDeModelo[i].image.sprite = spriteCabelo[q * i];
                    break;
                case 2:
                    botaoDeModelo[i].image.sprite = spriteShirt[q * i];
                    break;
                case 3:
                    botaoDeModelo[i].image.sprite = spriteTenis[q * i];
                    break;
                case 4:
                    botaoDeModelo[i].image.sprite = spriteShorts[q * i];
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
            }
        }
    }



    /// <summary>
    /// 
    /// "Ativa" a escolha do jogador com relação aos modelos, informando o sistema de qual parte está 
    /// selecionada, permitindo que o jogador possa, posteriormente, clicar no botão de continuar para 
    /// poder seguir para a parte de esolha de cores
    /// 
    /// É associado nos botões de escolha 
    /// 
    /// </summary>

    public void SelecionaModelo(int qualModelo)
    {
        nextButton.SetActive(true);
        qualModeloVaiSer = qualModelo;

        switch (qualParteVaiSer)
        {
            case 1:
                switch (qualModelo)
                {
                    case 0:
                        quantasCoresTem = coresCabelo[0];
                        break;
                    case 1:
                        quantasCoresTem = coresCabelo[1];
                        break;
                    case 2:
                        quantasCoresTem = coresCabelo[2];
                        break;
                    case 3:
                        quantasCoresTem = coresCabelo[3];
                        break;
                }
                break;

            case 2:
                switch (qualModelo)
                {
                    case 0:
                        quantasCoresTem = coresShirt[0];
                        break;
                    case 1:
                        quantasCoresTem = coresShirt[1];
                        break;
                    case 2:
                        quantasCoresTem = coresShirt[2];
                        break;
                    case 3:
                        quantasCoresTem = coresShirt[3];
                        break;
                }
                break;

            case 3:
                switch (qualModelo)
                {
                    case 0:
                        quantasCoresTem = coresShort[0];
                        break;
                    case 1:
                        quantasCoresTem = coresShort[1];
                        break;
                    case 2:
                        quantasCoresTem = coresShort[2];
                        break;
                    case 3:
                        quantasCoresTem = coresShort[3];
                        break;
                }
                break;

            case 4:
                switch (qualModelo)
                {
                    case 0:
                        quantasCoresTem = coresTenis[0];
                        break;
                    case 1:
                        quantasCoresTem = coresTenis[1];
                        break;
                    case 2:
                        quantasCoresTem = coresTenis[2];
                        break;
                    case 3:
                        quantasCoresTem = coresTenis[3];
                        break;
                }
                break;
        }
    }




    /// <summary>
    /// 
    /// Botão de confirmação sobre escolher qual modelo vai ser aplicado, e que serão mostradas
    /// as cores no menu de cores (3 Level).
    /// É associado no menu de modelos (2 Level) no botão "ContinuaButton", pois quando pressionado
    /// ele irá iniciar a abertura do novo menu de cores (3 Level) ajustando as funções dos novos botões
    /// dizendo qual botão irá associar a qual sprite
    ///
    /// </summary>
    public void EscolheQtdDeBotoesCor()//EscolheQtdBotoesCor //Decide quantos botões de cor serão ativados
    {
        int startArrayFromThisPoint = qualModeloVaiSer * maiorQuantitadeDeCoresDessaParte;
        int a, b, c, d, e;
        for (int i = 0; i < quantasCoresTem; i++)
        {
            botaoDeCor[i].gameObject.SetActive(true);
            switch (i)//Foi necessário criar essas 5 variaveis pois o delegate leva o valor consigo e não somente quando ele é associado, ou seja, se eu associar quando a variavel for "1" e posteriormente mudar essa variavel para "2" ele será considerado commo "2"
            {
                case 0:
                    a = startArrayFromThisPoint;
                    botaoDeCor[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, a); });
                    break;
                case 1:
                    b = startArrayFromThisPoint;
                    botaoDeCor[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, b); });
                    break;
                case 2:
                    c = startArrayFromThisPoint;
                    botaoDeCor[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, c); });
                    break;
                case 3:
                    d = startArrayFromThisPoint;
                    botaoDeCor[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, d); });
                    break;
                case 4:
                    e = startArrayFromThisPoint;
                    botaoDeCor[i].onClick.AddListener(delegate { QualParteVaiMudar(qualParteVaiSer, e); });
                    break;
            }

            if (mudaSpriteCor)// AQUI QUE FAZ A LATA DE SPRAY OU O SPRITE APARECER
            {
                switch (qualParteVaiSer)
                {
                    case 1:
                        botaoDeCor[i].image.sprite = spriteCabelo[startArrayFromThisPoint]; //Muda a sprite para a imagem de cor certa
                        break;
                    case 2:
                        botaoDeCor[i].image.sprite = spriteShirt[startArrayFromThisPoint]; //Muda a sprite para a imagem de cor certa
                        break;
                    case 3:
                        botaoDeCor[i].image.sprite = spriteTenis[startArrayFromThisPoint]; //Muda a sprite para a imagem de cor certa
                        break;
                    case 4:
                        botaoDeCor[i].image.sprite = spriteShorts[startArrayFromThisPoint]; //Muda a sprite para a imagem de cor certa
                        break;
                }
            }
            startArrayFromThisPoint++; //Aumenta o valor de "q" para poder mudar quando passar de botão
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

    public void DesativaOsModelos()
    {
        for (int i = 0; i < 5; i++)
        {
            botaoDeModelo[i].gameObject.SetActive(false);
        }
    }




    public void QualParteVaiMudar(int selectedPart, int index)//Envia a escolha de forma definitiva do resultado da combinação (Parte, Modelo e Cor) e efetiva no personagem
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


    public void EfetivaMudancaModelo(int qualBotao)
    {
        switch (qualParteVaiSer)
        {
            case 1:
                switch (qualBotao)
                {
                    case 1:
                        customizaScript.ChangeHair(botaoModelo1);
                        break;
                    case 2:
                        customizaScript.ChangeHair(botaoModelo2);
                        break;
                    case 3:
                        customizaScript.ChangeHair(botaoModelo3);
                        break;
                    case 4:
                        customizaScript.ChangeHair(botaoModelo4);
                        break;
                    case 5:
                        customizaScript.ChangeHair(botaoModelo5);
                        break;
                }
                break;
            case 2:
                switch (qualBotao)
                {
                    case 1:
                        customizaScript.ChangeShirt(botaoModelo1);
                        break;
                    case 2:
                        customizaScript.ChangeShirt(botaoModelo2);
                        break;
                    case 3:
                        customizaScript.ChangeShirt(botaoModelo3);
                        break;
                    case 4:
                        customizaScript.ChangeShirt(botaoModelo4);
                        break;
                    case 5:
                        customizaScript.ChangeShirt(botaoModelo5);
                        break;
                }
                break;
            case 3:
                switch (qualBotao)
                {
                    case 1:
                        customizaScript.ChangeShoes(botaoModelo1);
                        break;
                    case 2:
                        customizaScript.ChangeShoes(botaoModelo2);
                        break;
                    case 3:
                        customizaScript.ChangeShoes(botaoModelo3);
                        break;
                    case 4:
                        customizaScript.ChangeShoes(botaoModelo4);
                        break;
                    case 5:
                        customizaScript.ChangeShoes(botaoModelo5);
                        break;
                }
                break;
            case 4:
                switch (qualBotao)
                {
                    case 1:
                        customizaScript.ChangeShort(botaoModelo1);
                        break;
                    case 2:
                        customizaScript.ChangeShort(botaoModelo2);
                        break;
                    case 3:
                        customizaScript.ChangeShort(botaoModelo3);
                        break;
                    case 4:
                        customizaScript.ChangeShort(botaoModelo4);
                        break;
                    case 5:
                        customizaScript.ChangeShort(botaoModelo5);
                        break;
                }
                break;
        }
    }

    public void AtivaCirculoModel(int index)
    {
        for(int i = 0; i < circuloDeSelecaoModelo.Length; i++)
        {
            if(i == index)
            {
                circuloDeSelecaoModelo[i].SetActive(true);
            }
            else
            {
                circuloDeSelecaoModelo[i].SetActive(false);
            }
        }
    }

    public void AtivaCirculoCor(int index)
    {
        for (int i = 0; i < circuloDeSelecaoCor.Length; i++)
        {
            if (i == index)
            {
                circuloDeSelecaoCor[i].SetActive(true);
            }
            else
            {
                circuloDeSelecaoCor[i].SetActive(false);
            }
        }
    }

    public void DesativaCoresButton()
    {
        for(int i = 0; i < botaoDeCor.Length; i++)
        {
            botaoDeCor[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < circuloDeSelecaoCor.Length; i++)
        {
            circuloDeSelecaoCor[i].SetActive(false);
        }
    }
}
