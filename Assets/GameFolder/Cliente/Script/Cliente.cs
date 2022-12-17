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
    public Transform pontoNasce;

    //--Pedido--
    [SerializeField] private string descricao;

    //--Referencia Player--
    [SerializeField] private Player p;

    //--Acerto/Erro--
    [SerializeField] private GameObject Acerto, Erro;

    public int pe;

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
                //isRight = true;
                gameObject.SetActive(false);

                //Pode instanciar outro cliente
                GerenciadorGame.inst.PodeSpawnar();
                Destroy(gameObject, 0.2f);

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
        if (other.CompareTag("Presente") && other.GetComponent<Presente>().descricao_P == descricao)
        {
            if (other.GetComponent<Rigidbody2D>().simulated)
            {
                Debug.Log("Entrou");
                other.GetComponent<Rigidbody2D>().simulated = false;
                other.GetComponent<BoxCollider2D>().enabled = false;
                other.GetComponent<CircleCollider2D>().enabled = false;

                p.carregando = false;
                other.transform.parent = null;

                other.transform.parent = seguraPresente.transform;
                other.transform.position = seguraPresente.transform.position;

                Acerto.SetActive(true);

                //Adiciona dinheiro
                GerenciadorGame.inst.AddDinheiro(other.GetComponent<Presente>().preco);
                //Adciona audio
            }
        }

        if (other.CompareTag("Presente") && other.GetComponent<Presente>().descricao_P != descricao)
        {
            Erro.SetActive(true);
        }
    }
}
