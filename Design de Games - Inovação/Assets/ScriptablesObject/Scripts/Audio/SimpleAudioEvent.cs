using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Gerador/AudioEvent/Simple")]
public class SimpleAudioEvent : AudioEvent
{
    public AudioClip clips;

    public float volume;

    public override void Play(AudioSource source)
    {
        source.clip = clips;
        source.Play();
        return;
    }
}
