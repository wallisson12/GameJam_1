using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //--Player--
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float force;
    private Animator anim;

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

    //--Audio--
    [SerializeField] private AudioClip pulo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        checkGround = transform.GetChild(0).GetComponent<GroundCheck>();
        btn_Interacao = transform.GetChild(2).GetComponent<SpriteRenderer>();
        obj_Segura = transform.GetChild(3).transform;
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);    
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


        //Flip
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            anim.SetBool("IsWalking", true);

            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);

            }
            else
            {
                transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }

    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && checkGround.numJump > 0)
        {
            //animacao e audio
            GerenciadorAudio.inst.PlayFX(pulo);
            checkGround.numJump--;
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(rb.velocity.x, force));
        }
    }

    void Interacao()
    {
        if (Input.GetKeyDown(KeyCode.E) && interagir && carregando == false)
        {
            presente.GetComponent<Rigidbody2D>().simulated = false;
            presente.parent = obj_Segura.transform;
            presente.transform.position = obj_Segura.transform.position;
            carregando = true;
        }

        if (Input.GetKeyDown(KeyCode.Q) && carregando)
        {
            presente.GetComponent<Rigidbody2D>().simulated = true;
            presente.parent = null;
            carregando = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Presente"))
        {
            if (carregando == false)
            {
                cor.a = 1;
                btn_Interacao.color = cor;
                interagir = true;

                presente = other.transform;
            }
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
