using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer musicMixer;

    public FloatVariable musicVol;

    public Slider musicSlider;

    public AudioMixer SFXMixer;

    public FloatVariable sfxVol;

    public Slider sfxSlider;

    private void Awake()
    {
        musicSlider.value = musicVol.Value;
        sfxSlider.value = sfxVol.Value;
    }

    public void SetLevelMusic (float sliderValue)
    {
        musicMixer.SetFloat("MusicVol", Mathf.Log10 (sliderValue) * 20);
        musicVol.Value = sliderValue;
    }

    public void SetLevelSFX(float sliderValue)
    {
        SFXMixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
        sfxVol.Value = sliderValue;
    }
}
