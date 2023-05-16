using UnityEngine;

public class BtnOver : MonoBehaviour
{
    [SerializeField] private AudioClip btnOver;
    public void PlayOver()
    {
        GerenciadorAudio.inst.PlayFX(btnOver);
    }
}
