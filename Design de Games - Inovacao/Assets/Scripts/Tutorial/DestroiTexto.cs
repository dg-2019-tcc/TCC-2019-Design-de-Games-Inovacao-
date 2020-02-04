using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroiTexto : MonoBehaviour
{


    [HideInInspector]
    public float tempoParaDestruir;



    private void OnEnable()
    {
        StartCoroutine(DestruirFala(5));
    }



    private IEnumerator DestruirFala(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        Destroy(gameObject);
    }
}
