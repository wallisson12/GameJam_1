using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorGame : MonoBehaviour
{
    public static GerenciadorGame inst;

    //--Tempo & Dinheiro--
    public float TempoGame;
    public int DinheiroGame;
    public int DinheiroMeta;

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

        Time.timeScale = 1;
    }
    
    void Update()
    {
        UpdateUI();

        TempoGame -= Time.deltaTime;

        if (TempoGame <= 0)
        {
            TempoGame = 0;

            if (DinheiroGame >= DinheiroMeta)
            {
                GerenciadorUI.inst.Win();
                Time.timeScale = 0;
            }
            else
            {
                GerenciadorUI.inst.GameOver();
                Time.timeScale = 0;
            }


        }
    }

    void UpdateUI()
    {
        GerenciadorUI.inst.Txt_Tempo.text = TempoGame.ToString("F2");
        GerenciadorUI.inst.Txt_Dinheiro.text = DinheiroGame.ToString("F2");
    }
    public void Pause()
    {
        Time.timeScale = 0;
        GerenciadorUI.inst.PauseGame();
    }

    public void DesPausa()
    {
        Time.timeScale = 1;
        GerenciadorUI.inst.DesPauseGame();
    }
}
