using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Photon.Pun;

public class AtivaTexto : MonoBehaviour
{

    [Header("EscolheImagem")]

    [SerializeField]
    private Sprite fala;
    [SerializeField]
    private GameObject falaGO;



    [Header("Variaveis")]

    static bool falaAtiva;
    [SerializeField]
    private float tempoAtivo;

    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(falaAtiva == false)
            {
                falaAtiva = true;
                GameObject entry = Instantiate(falaGO);
                //entry.GetComponent<DestroiTexto>().tempoParaDestruir = tempoAtivo;
                entry.transform.SetParent(collision.transform);
                entry.GetComponentInChildren<Image>().sprite = fala;
                StartCoroutine(CooldownFala(5));
            }            
        }
    }



    private IEnumerator CooldownFala(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        falaAtiva = false;
    }
}
