using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    ///Deve ser colocado o seguinte no qualSom - "event:/HUD/Start" (event:/pasta/nomeDoEvento)

    public string qualSom;


    public void TocaEsseSom()
    {
        FMODUnity.RuntimeManager.PlayOneShot(qualSom);
    }
}
