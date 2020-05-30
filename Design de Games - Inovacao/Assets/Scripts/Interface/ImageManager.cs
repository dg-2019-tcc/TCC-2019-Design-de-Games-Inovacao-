using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{


    /// <summary>
    /// Este script faz com que imagens sejam 
    /// passadas entre si após determinado tempo 
    /// (sendo este decidido pela variável 
    /// "tempoParaProxima"), essas imagens são 
    /// colocadas através de um Array (que é 
    /// o "textSprites"), elas devem ser 
    /// inseridas pelo inspector (*IMPORTANTE*
    /// as imagens precisam ser de "TextureType"
    /// "Sprite (2D and UI)" pois assim elas 
    /// estarão no formato de sprite)
    /// 
    /// Deve ser colocado em um objeto que 
    /// tenha um collider para que o jogador 
    /// possa colidir e ativar a sequência 
    /// de imagens
    /// </summary>



    public GameObject textBox;

    bool ativou = false; //Detecta colisão pela primeira vez
    public bool textBoxLigada = false; //Checa se a text box está ligada
    
    //[HideInInspector]
    public int currentImage = 0;
    [HideInInspector]
    public int endAtImage;
    
    public Image theImage;

    public Sprite[] textSprites;

    public float tempoParaProxima;


	private GameObject joystick;

    public BoolVariable aiGanhou;

    private void Start()
    {
        //aiGanhou = Resources.Load<BoolVariable>("AIGanhou");

        textBox.SetActive(false);
        endAtImage = textSprites.Length;
        textBoxLigada = false;

        /*if (aiGanhou.Value == false)
        {
            StartCoroutine(Teste());
        }*/

        /*joystick = FindObjectOfType<Joystick>().gameObject;
		joystick.SetActive(false);*/
    }

    private void Update()
    {
        if (aiGanhou.Value == false && textBoxLigada == false)
        {
            StartCoroutine(Teste());
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !ativou)
        {
            ativou = false;
            StartCoroutine(Teste());
        }
    }
	*/

    IEnumerator Teste()
    {
        textBox.SetActive(true);
        textBoxLigada = true;
        for (int i = 0; i < endAtImage; i++)
        {
            theImage.sprite = textSprites[currentImage];
            /*if (textBoxLigada == false)
            {
                textBox.SetActive(true);
                textBoxLigada = true;
            }*/
            yield return new WaitForSeconds(tempoParaProxima);
            currentImage++;
        }
        textBox.SetActive(false);
        //joystick.SetActive(true);
        Debug.Log("Destroy");
        Destroy(gameObject);
    }
}
