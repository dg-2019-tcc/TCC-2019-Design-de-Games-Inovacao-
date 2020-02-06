using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Gerador/AudioEvent/Simple")]
public class SimpleAudioEvent : AudioEvent
{
    public AudioClip clips;

    public float volume;
    public float minPitch;
    public float maxPitch;

    public override void Play(AudioSource source)
    {
        source.clip = clips;
        source.pitch = Random.Range(minPitch, maxPitch);
        source.Play();
        return;
    }
}
