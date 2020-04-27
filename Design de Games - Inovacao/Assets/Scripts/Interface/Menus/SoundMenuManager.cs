using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.UI;

public class SoundMenuManager : MonoBehaviour
{
    ///<summary>
    /// 
    /// Este código tem a função de cuidar do menu de som
    /// ativando e desativando o menu e também lidando 
    /// com os botões, ativando a música e os SFX
    /// 
    /// posteriormente a intenção é unir esse script 
    /// com outros que cuidem do menu de pausa em geral
    /// 
    /// </summary>

    Bus master;
    Bus musics;
    Bus sfx;

    bool isMasterOn = true;
    bool isMusicsOn = true;
    bool isSFXOn = true;

    void Awake()
    {
        master = RuntimeManager.GetBus("bus:/Master");
        musics = RuntimeManager.GetBus("bus:/Master/Musics");
        sfx = RuntimeManager.GetBus("bus:/Master/SFX");
    }

    public void SoundMode(Bus myBus, ref bool isSoundOn)
    {
        if (isSoundOn)
        {
            myBus.setMute(true);
            isSoundOn = false;
        }
        else
        {
            myBus.setMute(false);
            isSoundOn = true;
        }
    }

    public void ButtonSoundConfig(string qualBus)
    {
        switch (qualBus)
        {
            case "Master":
                SoundMode(master, ref isMasterOn);
                break;
            case "Musics":
                SoundMode(musics, ref isMusicsOn);
                break;
            case "SFX":
                SoundMode(sfx, ref isSFXOn);
                break;
        }
    }
}
