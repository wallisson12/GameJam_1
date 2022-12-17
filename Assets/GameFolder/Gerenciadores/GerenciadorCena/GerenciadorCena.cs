using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorCena : MonoBehaviour
{
    [SerializeField] private string nameCena;
    [SerializeField] private string btn_Retry;

    void Update()
    {
        btn_Retry = SceneManager.GetActiveScene().name;    
    }
    public void ProximaCena(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void Restart()
    {
        SceneManager.LoadScene(btn_Retry);
    }
}
