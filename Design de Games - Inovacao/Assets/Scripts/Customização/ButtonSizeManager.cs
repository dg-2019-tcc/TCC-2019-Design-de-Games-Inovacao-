using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSizeManager : MonoBehaviour
{
    [Header("Buttons")]

    public Button modelButton;
    public Button[] colorButton;


    [Header("Size")]
    public float width;
    public float height;


    private void Awake()
    {
        SizeManagerModel();
    }

    public void SizeManagerModel()
    {
        modelButton.transform.localScale = new Vector3(width,height);
    }


    //Esse codigo está aqui para caso eu precise mudar o tamanho dos botões de cor também,
    //se esse momento chegar, coloque todos os botões da lista de cores no array e chame isto
    //quando o botão for apertado, ELE NÃO É CHAMADO SOZINHO, VOCÊ PRECISA ASSOCIAR NO BOTÃO
    public void SizeManagerColor()
    {
        for(int i = 0; i < colorButton.Length; i++)
        {
            colorButton[i].transform.localScale = new Vector3(width, height);
        }
    }




}
