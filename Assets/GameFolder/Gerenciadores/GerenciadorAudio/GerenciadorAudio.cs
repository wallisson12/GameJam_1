using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorAudio : MonoBehaviour
{
    public static GerenciadorAudio inst;
    public AudioSource AudioS;
    public float Volume;

    void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(gameObject);
        }  
    }


    public void PlayFX(AudioClip clip)
    {
        AudioS.PlayOneShot(clip, Volume);
    }
}
