using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColetavelGerador : MonoBehaviour
{
    public GameObject[] coletaveis;

    public int indexCole;
    
    void Update()
    {
        indexCole = DestroyColetavel2D.index;

        coletaveis[indexCole].SetActive(true);
    }
}
