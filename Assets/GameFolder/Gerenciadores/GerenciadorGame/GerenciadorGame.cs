using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorGame : MonoBehaviour
{
    public static GerenciadorGame inst;

    //--Tempo & Dinheiro | Game--
    public float TempoGame;
    public int DinheiroGame;
    public int DinheiroMeta;

    //--Gerenciar Cliente--
    [SerializeField] private GameObject[] clientes;
    [SerializeField] private float tempoSpawn;
    [SerializeField] private float tempoComeca;
    [SerializeField] private int instancias;
    [SerializeField] private AudioClip portaAbre;


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
        //--Atualiza Ui--
        UpdateUI();

        //--Spawn Cliente--
        SpawnCliente();

        //--Tempo Game--
        if (tempoComeca <=0)
        {
            TempoGame -= Time.fixedDeltaTime * 0.4f;

            if (TempoGame <= 0)
            {
                //--Ganhou || Perdeu
                if (DinheiroGame >= DinheiroMeta)
                {
                    GerenciadorUI.inst.Win();
                }
                else
                {
                    GerenciadorUI.inst.GameOver();
                }

                TempoGame = 0f;
            }
        }


    }

    void SpawnCliente()
    {
        if (tempoComeca > 0f)
        {
            tempoComeca -= Time.fixedDeltaTime * 0.4f;
        }
        else
        {
            tempoComeca = 0f;
        }

        if ( tempoComeca <= 0 && instancias == 0)
        {
            //Spawn um cliente
            GameObject obj = clientes[Random.Range(0, clientes.Length)];
            Instantiate(obj, obj.GetComponent<Cliente>().pontoNasce.transform.position, Quaternion.identity);
            GerenciadorAudio.inst.PlayFX(portaAbre);
            instancias = 1;
        }
       
    }

    public void PodeSpawnar()
    {
        StartCoroutine(TempoSpawn());
    }

    IEnumerator TempoSpawn()
    {
        yield return new WaitForSeconds(tempoSpawn);
        instancias = 0;
    }

    void UpdateUI()
    {
        string minutos = ((int)TempoGame/60).ToString("00");
        string segundos = (TempoGame % 60).ToString("00");

        GerenciadorUI.inst.Txt_Tempo.text = minutos + ":" + segundos;
        GerenciadorUI.inst.Txt_Dinheiro.text = DinheiroGame.ToString("F2");
    }

    public void AddDinheiro(int valor)
    {
        DinheiroGame += valor;
    } 
    public void Pause()
    {
        GerenciadorUI.inst.PauseGame();
    }

    public void DesPausa()
    {
        GerenciadorUI.inst.DesPauseGame();
    }
}
