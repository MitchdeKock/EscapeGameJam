using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioClip testSFX;
    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume",level);
    }
    public void SetSFXVolume(float level)
    { 
        audioMixer.SetFloat("SoundFXVolume", level);
        SFXManager.instance.PlaySoundFXClip(testSFX, transform, 1f);
    }
    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", level);
    }
}
