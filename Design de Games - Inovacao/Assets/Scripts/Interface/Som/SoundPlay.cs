using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    ///Deve ser colocado o seguinte no qualSom - "event:/HUD/Start" (event:/pasta/nomeDoEvento)

    public string qualSom;

    public bool espera = false;

    bool possoTocar = true;
    public void TocaEsseSom()
    {
        if(espera == true)
        {
            if (possoTocar == true)
            {
                StartCoroutine(EsperaPraTocar());
                FMODUnity.RuntimeManager.PlayOneShot(qualSom);
            }
        }
        else
        {
            StartCoroutine(EsperaPraTocar());
        }
    }


    IEnumerator EsperaPraTocar()
    {
        possoTocar = false;
        yield return new WaitForSeconds(3);
        possoTocar = true;
    }

}
