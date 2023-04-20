using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public bool isThisAudioSourceMusic;
    private AudioSource audioSource;
    private int controlSound;
    private int controlMusic;
    void Start()
    {
        controlMusic = PlayerPrefs.GetInt("music");
        controlSound = PlayerPrefs.GetInt("sound");
        audioSource = this.gameObject.GetComponent<AudioSource>();

        if (isThisAudioSourceMusic)
        {
            if (controlMusic == 0) audioSource.mute = true;
            else audioSource.mute = false;
        }
        else
        {
            if (controlSound == 0) audioSource.mute = true;
            else audioSource.mute = false;
        }
    }

    void Update()
    {
        if (isThisAudioSourceMusic)
        {
            if (controlMusic != PlayerPrefs.GetInt("music"))
            {
                controlMusic = PlayerPrefs.GetInt("music");
                audioSource.mute = !audioSource.mute;
                
            }
        }
        else
        {
            if (controlSound != PlayerPrefs.GetInt("sound"))
            {
                controlSound = PlayerPrefs.GetInt("sound");
                audioSource.mute = !audioSource.mute;

            }
        }
    }
}
