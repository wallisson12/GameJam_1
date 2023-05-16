using UnityEngine;

public class GerenciadorAudio : MonoBehaviour
{
    public static GerenciadorAudio inst;

    [SerializeField] private AudioSource AudioS;
    [SerializeField] private float Volume;

    void Awake()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(inst);
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
