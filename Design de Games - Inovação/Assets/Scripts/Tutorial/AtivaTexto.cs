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



    private void Update()
    {/*

        

        if (falaAtiva)
        {
            StartCoroutine(IniciaFala());
       }*/
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(falaAtiva == false)
            {
                falaAtiva = true;
                GameObject entry = Instantiate(falaGO);
                entry.transform.SetParent(collision.transform);
                entry.GetComponentInChildren<Image>().sprite = fala;
            }            
        }
    }



    private IEnumerator IniciaFala()
    {
        falaAtiva = false;        
        falaGO.SetActive(true);
        yield return new WaitForSeconds(4);
        falaGO.SetActive(false);
    }
}
