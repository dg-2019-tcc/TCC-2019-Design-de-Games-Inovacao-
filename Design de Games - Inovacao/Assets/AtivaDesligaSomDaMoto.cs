using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivaDesligaSomDaMoto : MonoBehaviour
{

    public GameObject motoParador;
    
    void Start()
    {
        StartCoroutine(ParaMoto());
    }




    IEnumerator ParaMoto()
    {
        yield return new WaitForSeconds(0.3f);
        motoParador.SetActive(true);
    }
}
