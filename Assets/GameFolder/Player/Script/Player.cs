using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //--Player--
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float force;

    //--CheckGround--
    [SerializeField] private GroundCheck checkGround;

    //--Interacao--
    [SerializeField] private SpriteRenderer btn_Interacao;
    [SerializeField] private Color cor;
    [SerializeField] private bool interagir;
    [SerializeField] private Transform obj_Segura;
    [SerializeField] private Transform presente;
                     public bool carregando = false;

    //--Pause--
    [SerializeField] private bool pause = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        checkGround = transform.GetChild(0).GetComponent<GroundCheck>();
        btn_Interacao = transform.GetChild(2).GetComponent<SpriteRenderer>();
        obj_Segura = transform.GetChild(3).transform;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        
        //Animacao andar
        //Flip
    }
    void Update()
    {
        //Pulo
        Jump();

        //Interacao
        Interacao();

        //Pause menu
        if (Input.GetKeyDown(KeyCode.Escape) && pause == false)
        {
            GerenciadorGame.inst.Pause();
            pause = true;

        }else if (Input.GetKeyDown(KeyCode.Escape) && pause)
        {
            GerenciadorGame.inst.DesPausa();
            pause = false;
        }

    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && checkGround.numJump > 0)
        {
            //animacao e audio
            checkGround.numJump--;
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(rb.velocity.x, force));
        }
    }

    void Interacao()
    {
        if (Input.GetKeyDown(KeyCode.E) && interagir && carregando == false)
        {
            presente.parent = obj_Segura.transform;
            presente.transform.position = obj_Segura.transform.position;
            presente.GetComponent<Rigidbody2D>().simulated = false;
            carregando = true;
        }

        if (Input.GetKeyDown(KeyCode.Q) && carregando == true)
        {
            presente.parent = null;
            carregando = false;
            presente.GetComponent<Rigidbody2D>().simulated = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Presente"))
        {
            cor.a = 1;
            btn_Interacao.color = cor;
            interagir = true;

            presente = other.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Presente"))
        {
            cor.a = 0;
            btn_Interacao.color = cor;
            interagir = false;
        }
    }

}
