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

    Bus musics;
    Bus sfx;

    static bool isMusicsOn;
    static bool isSFXOn;

    bool isMenuOpen = false;

    public GameObject menu;
    public GameObject butonOnline;


    public Button m;
    public Button s;

	public Sprite[] musicSprite;
	public Sprite[] soundSprite;


    void Awake()
    {
        musics = RuntimeManager.GetBus("bus:/Master/Musics");
        sfx = RuntimeManager.GetBus("bus:/Master/SFX");
    }
    /*
    private void OnEnable()
    {
        changeButtonCollor(isMusicsOn, m, musicSprite);
        changeButtonCollor(isSFXOn, s, soundSprite);
    }
    */
    /*
	private void Start()
	{
		changeButtonCollor(isMusicsOn, m, musicSprite);
		changeButtonCollor(isSFXOn, s, soundSprite);
	}*/

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
            case "Musics":
                SoundMode(musics, ref isMusicsOn);
                changeButtonCollor(isMusicsOn, m, musicSprite);
                break;
            case "SFX":
                SoundMode(sfx, ref isSFXOn);
                changeButtonCollor(isSFXOn, s, soundSprite);
                break;
        }
    }



    public void OpenOrCloseMenu()
    {
        if (isMenuOpen == true)
        {
            menu.SetActive(false);
            butonOnline.SetActive(false);
            isMenuOpen = false;
        }
        else
        {
            menu.SetActive(true);
            butonOnline.SetActive(true);
            isMenuOpen = true;
        }
    }


    public void changeButtonCollor(bool active, Button b, Sprite[] sprite)
    {
        if(active == true)
        {
            b.image.sprite = sprite[0];
        }
        else
        {
			b.image.sprite = sprite[1];
		}
    }
}

