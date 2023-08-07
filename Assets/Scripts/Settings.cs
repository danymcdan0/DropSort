//--------------------------------------------------------------------------------------------------------------------\\
//Settings.cs
//--------------------------------------------------------------------------------------------------------------------\\
//PURPOSE: Script to handle the settings set by the player:
//--------------------------------------------------------------------------------------------------------------------\\
//-->Settings was made into a Singleton to ensure that there are no duplicates of the object with a combination
//of DontDestroyOnLoad(this.gameObject) to ensure when going between scenes that the volume stays the same and music
//plays through the different scenes.
//-->Further sets the game in fullscreen or not with a toggle.
//--------------------------------------------------------------------------------------------------------------------\\
//AUTHOR: DANYAL SALEH ds18635
//Registration number: 1806262
//--------------------------------------------------------------------------------------------------------------------\\
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    
    public AudioMixer audioMixer; //To control sound volume

    private static Settings _instance;

    public static Settings Singleton
    {
        get => _instance;
        private set
        {
            if (_instance == null)
            {
                _instance = value;
            }
            else if (_instance != value)
            {
                Debug.Log($"{nameof(Settings)} instance already exists, destroying object!");
                Destroy(value.gameObject);
            }
        }
    }
    public void Awake()
    {
        Singleton = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetVolume(float volume) //To be able to change the mixers volume as the slider in settings changes
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void Fullscreen(bool isFullscreen) //To be able to set the game in fullscreen or not 
    {
        Screen.fullScreen = isFullscreen;
    }
}