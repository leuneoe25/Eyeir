using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{

    public Slider SFXSource;
    public Slider MusicSource;

    public AudioSource Musicsource;
    public AudioSource SFXsource;

    public void SetMusicVolume(float volume)
    {
        Musicsource.volume = MusicSource.value;
    }

    public void SetSFXVolume(float volume)
    {
        SFXsource.volume = SFXSource.value;
    }
}
