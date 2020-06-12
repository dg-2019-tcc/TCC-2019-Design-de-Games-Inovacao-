using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;


public class DesligaSomDaMoto : MonoBehaviour
{

    Bus motocaBus;

    void Start()
    {
        motocaBus = RuntimeManager.GetBus("bus:/Master/Musics/Moto");
        motocaBus.setMute(true);
    }
}
