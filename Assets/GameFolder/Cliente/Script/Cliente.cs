using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliente : MonoBehaviour
{
    //--Cliente--
    [SerializeField] private bool isRight;
    [SerializeField] private Transform A, B;
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float time = 100f;
    [SerializeField] private Transform seguraPresente;

    //--Pedido--
    [SerializeField] private string descricao;

    //--Referencia Player--
    [SerializeField] private Player p;

    //--Acerto/Erro--
    [SerializeField] private GameObject Acerto, Erro;

    void Start()
    {
        p = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (isRight)
        {
            transform.localScale = new Vector3(1, 1, 1);

            if (Vector2.Distance(transform.position,B.position) < 0.3f)
            {
                TempoAtendimento();

            }

            transform.position = Vector2.MoveTowards(transform.position, B.position, speed);
        }
        else
        {
            transform.localScale = new Vector3(-1,1,1);

            if (Vector2.Distance(transform.position,A.position) < 0.3f)
            {
                //Barulho de porta abrindo ou fechando
                Destroy(gameObject,0.2f);
            }

            transform.position = Vector2.MoveTowards(transform.position, A.position, speed);
        }
        
    }

    void TempoAtendimento()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            isRight = false;
        }
    }

   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Presente"))
        {
            if (other.GetComponent<Presente>().descricao_P == descricao)
            {
                p.carregando = false;
                other.transform.parent = null;

                other.transform.parent = seguraPresente.transform;
                other.transform.position = seguraPresente.transform.position;
                other.GetComponent<Rigidbody2D>().simulated = false;
                other.GetComponent<BoxCollider2D>().enabled = false;
                other.GetComponent<CircleCollider2D>().enabled = false;

                Acerto.SetActive(true);
                //Ganha Ponto
                //Adiciona dinheiro
                //Adciona audio
            }

            if (other.GetComponent<Presente>().descricao_P != descricao)
            {
                Erro.SetActive(true);
                //Perde Ponto
            }
         
        }
    }
}
