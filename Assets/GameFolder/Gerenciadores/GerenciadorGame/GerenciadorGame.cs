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

    //--Presentes Cena--
    public int PresentesCena=12;


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

        Time.timeScale = 1f;
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
            TempoGame -= Time.deltaTime * 0.7f;

            if (TempoGame <= 0 || PresentesCena <=0)
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

                if (TempoGame <=0)
                {
                    TempoGame = 0f;
                }

            }
        }


    }

    void SpawnCliente()
    {
        if (tempoComeca > 0f)
        {
            tempoComeca -= Time.deltaTime * 0.5f;
        }
        else
        {
            tempoComeca = 0f;
        }

        if ( tempoComeca <= 0 && instancias == 0)
        {
            //Spawn um cliente
            instancias++;
            GameObject obj = clientes[Random.Range(0, clientes.Length)];
            Instantiate(obj, obj.GetComponent<Cliente>().pontoNasce.transform.position, Quaternion.identity);
            GerenciadorAudio.inst.PlayFX(portaAbre);
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
        string segundos = (TempoGame % 60).ToString("00");
        string minutos = ((int)TempoGame/60).ToString("00");


        if (tempoComeca > 0)
        {
            GerenciadorUI.inst.Txt_Tempo.text = ((int)tempoComeca).ToString();
        }
        else
        {
            GerenciadorUI.inst.Txt_Tempo.text = minutos + ":" + segundos;                
        }

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
