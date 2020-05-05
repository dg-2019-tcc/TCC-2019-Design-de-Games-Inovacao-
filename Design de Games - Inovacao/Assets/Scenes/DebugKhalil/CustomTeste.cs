using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomTeste : MonoBehaviour
{
    public Sprite[] cabelo; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19
    public Sprite[] shirt; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19   
    public Sprite[] shorts; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19
    public Sprite[] tenis; //Tem todos os sprites de cabelo, os modelos de cabelo vão de 0 - 4 / 5 - 9 / 10 - 14 / 15 / 19

    public Button[] botaoDeModelo; //Qual botão dos modelos estamos mexendo
    public Button[] botaoDeCor; //Qual o botão que a gente está mexendo

    [Header("Quantidade de cores que cada modelo tem")]
    public int[] coresCabelo;
    public int[] coresShirt;
    public int[] coresShort;
    public int[] coresTenis;

    [Header("Quantas variações (Cores) o modelo com mais variações tem")]
    public int maiorVariedadeCorCabelo;
    public int maiorVariedadeCorShirt;
    public int maiorVariedadeCorShort;
    public int maiorVariedadeCorTenis;

    [Header("Botão para confirmar escolha de modelo")]
    public Button ContinuaButton;

    [Header("Telas De Menu")]
    public GameObject Menu1;
    public GameObject Menu2;
    public GameObject Menu3;

    int qualParteVaiSer;
    int qualModeloVaiSer;

    public Custom2D customizaScript;

    int maiorQuantitadeDeCoresDessaParte;

    int quantasCoresTem; // Fala para dos botões Modelo(2 Level) para os botões Cor (3 Level) quantas cores diferentes existem



    // Serve para trocar entre as telas dos menus da customização
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

    public void QualParte(int selectedPart)
    {
        switch (selectedPart)
        {
            case 1:
                qualParteVaiSer = 1;
                QuantidadeDeCores(maiorVariedadeCorCabelo);
                break;
            case 2:
                qualParteVaiSer = 2;
                QuantidadeDeCores(maiorVariedadeCorShirt);
                break;
            case 3:
                qualParteVaiSer = 3;
                QuantidadeDeCores(maiorVariedadeCorShort);
                break;
            case 4:
                qualParteVaiSer = 4;
                QuantidadeDeCores(maiorVariedadeCorTenis);
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
    public void EscolheQtdModelo(int qtdDeModelos)//EscolheQtdBotoesModelos //Decide quantos botões de modelo serão mostrados
    {
        int q = 5;
        for (int i = 0; i < qtdDeModelos; i++)
        {
            botaoDeModelo[i].gameObject.SetActive(true);

            switch (qualParteVaiSer)//Decide qual sprite vai aparecer nos botões de Modelo(2 nível)
            {
                case 1:
                    botaoDeModelo[i].image.sprite = cabelo[q * i];
                    break;
                case 2:
                    botaoDeModelo[i].image.sprite = shirt[q * i];
                    break;
                case 3:
                    botaoDeModelo[i].image.sprite = tenis[q * i];
                    break;
                case 4:
                    botaoDeModelo[i].image.sprite = shorts[q * i];
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
        
        
        if (ContinuaButton.interactable == false)
        {
            ContinuaButton.interactable = true;
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
    public void EscolheQtdCor()//EscolheQtdBotoesCor //Decide quantos botões de cor serão ativados
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
            
            switch (qualParteVaiSer)
            {
                case 1:
                    botaoDeCor[i].image.sprite = cabelo[startArrayFromThisPoint]; //Muda a sprite para a imagem de cor certa
                    break;
                case 2:
                    botaoDeCor[i].image.sprite = shirt[startArrayFromThisPoint]; //Muda a sprite para a imagem de cor certa
                    break;
                case 3:
                    botaoDeCor[i].image.sprite = tenis[startArrayFromThisPoint]; //Muda a sprite para a imagem de cor certa
                    break;
                case 4:
                    botaoDeCor[i].image.sprite = shorts[startArrayFromThisPoint]; //Muda a sprite para a imagem de cor certa
                    break;                    
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
}
