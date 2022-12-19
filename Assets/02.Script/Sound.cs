using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{

    public Slider SFXSource;
    public Slider MasterSource;
    public Slider MusicSource;

    public AudioSource Musicsource;
    public AudioSource SFXsource;

    public void SetMusicVolume(float volume)
    {
        Musicsource.volume = volume * MasterSource.value;
    }


    public void SetSFXVolume(float volume)
    {
        SFXsource.volume = volume * MasterSource.value;
    }

    public void SetMasterVolume(float volume)
    {
        SFXsource.volume = SFXSource.value * volume;
        Musicsource.volume = MusicSource.value * volume;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
