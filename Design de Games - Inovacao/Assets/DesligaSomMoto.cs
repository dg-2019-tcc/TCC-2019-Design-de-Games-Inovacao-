using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;




public class DesligaSomMoto : MonoBehaviour
{

    Bus somMoto;

    void Start()
    {
        somMoto = RuntimeManager.GetBus("bus:/Master/SFX/Moto");
        somMoto.setMute(true);
    }
}
