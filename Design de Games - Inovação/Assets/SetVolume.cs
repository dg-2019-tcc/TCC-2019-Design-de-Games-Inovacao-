using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer musicMixer;

    public AudioMixer SFXMixer;

    public void SetLevelMusic (float sliderValue)
    {
        musicMixer.SetFloat("MusicVol", Mathf.Log10 (sliderValue) * 20);
    }

    public void SetLevelSFX(float sliderValue)
    {
        SFXMixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
    }
}
