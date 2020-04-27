﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.UI;
using TMPro;

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

    public TextMeshProUGUI masterText;
    public TextMeshProUGUI musicsText;
    public TextMeshProUGUI sfxText;

    

    void Awake()
    {
        master = RuntimeManager.GetBus("bus:/Master");
        musics = RuntimeManager.GetBus("bus:/Master/Musics");
        sfx = RuntimeManager.GetBus("bus:/Master/SFX");
        ButtonModeText("Master", isMasterOn, masterText);
        ButtonModeText("Musics", isMusicsOn, musicsText);
        ButtonModeText("SFX", isSFXOn, sfxText);
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
                if(isMasterOn == false)
                {
                    isMusicsOn = false;
                    musics.setMute(true);
                    isSFXOn = false;
                    sfx.setMute(true);
                }
                else
                {
                    
                    isMusicsOn = false;
                    isSFXOn = false;
                    SoundMode(musics, ref isMusicsOn);
                    SoundMode(sfx, ref isSFXOn);
                    
                }
                ButtonModeText("Master", isMasterOn, masterText);
                ButtonModeText("Musics", isMusicsOn, musicsText);
                ButtonModeText("SFX", isSFXOn, sfxText);
                break;


            case "Musics":
                SoundMode(musics, ref isMusicsOn);
                if (isMusicsOn == true && isMasterOn == false)
                {
                    SoundMode(master, ref isMasterOn);
                    ButtonModeText("Master", isMasterOn, masterText);
                }
                ButtonModeText("Musics", isMusicsOn, musicsText);
                break;


            case "SFX":
                SoundMode(sfx, ref isSFXOn); 
                if (isSFXOn == true && isMasterOn == false)
                {
                    SoundMode(master, ref isMasterOn);
                    ButtonModeText("Master", isMasterOn, masterText);
                }
                ButtonModeText("SFX", isSFXOn, sfxText);
                break;
        }
    }


    public void ButtonModeText(string buttonText, bool isOn, TextMeshProUGUI textSpace)
    {
        if(isOn == true)
        {
            textSpace.text = buttonText + ": On";
            textSpace.color = Color.green;
        }
        else
        {
            textSpace.text = buttonText + ": Off";
            textSpace.color = Color.red;
        }
    }
}
