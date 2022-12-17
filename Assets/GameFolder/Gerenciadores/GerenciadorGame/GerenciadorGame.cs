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

    //--Gerenciar Cliente--
    [SerializeField] private GameObject[] clientes;
    [SerializeField] private float tempoSpawn;
    [SerializeField] private float tempoComeca;
    [SerializeField] private int instancias;


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
        SpawnCliente();

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

    void SpawnCliente()
    {
        tempoComeca -= Time.deltaTime;

        if ( tempoComeca <= 0 && instancias == 0)
        {
            //Spawn um cliente
            GameObject obj = clientes[Random.Range(0, clientes.Length)];
            Instantiate(obj, obj.GetComponent<Cliente>().pontoNasce.transform.position, Quaternion.identity);
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
        GerenciadorUI.inst.Txt_Tempo.text = TempoGame.ToString("F0");
        GerenciadorUI.inst.Txt_Dinheiro.text = DinheiroGame.ToString("F2");
    }

    public void AddDinheiro(int valor)
    {
        DinheiroGame = DinheiroGame + valor;
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
