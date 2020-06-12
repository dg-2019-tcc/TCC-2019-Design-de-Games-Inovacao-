using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivaDesligaMoto : MonoBehaviour
{

    public GameObject desativador;

    void Start()
    {
        StartCoroutine(AtivaDesligaMotoCor());
    }


    IEnumerator AtivaDesligaMotoCor()
    {
        yield return new WaitForSeconds(0.5f);
        desativador.SetActive(true);

    }


}
