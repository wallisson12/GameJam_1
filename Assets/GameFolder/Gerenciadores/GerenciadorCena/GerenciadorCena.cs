using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorCena : MonoBehaviour
{
    [SerializeField] private string nameScene;
    [SerializeField] private AudioClip btnSound;

    void Start()
    {
        nameScene = SceneManager.GetActiveScene().name;
    }

    public void ProximaCena(string name)
    {
        GerenciadorAudio.inst.PlayFX(btnSound);
        SceneManager.LoadScene(name);
    }
    public void Restart()
    {
        GerenciadorAudio.inst.PlayFX(btnSound);
        SceneManager.LoadScene(nameScene);
    }

    public void Sair()
    {
        GerenciadorAudio.inst.PlayFX(btnSound);
        Application.Quit();
    }
}
