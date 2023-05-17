using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //--Player--
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float forceJump;
    [SerializeField] private Animator anim;

    //--CheckGround--
    [SerializeField] private GroundCheck checkGround;   

    //--Pause--
    [SerializeField] private bool pause = false;

    //--Audio--
    [SerializeField] private AudioClip pulo;

    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);    
    }

    void Update()
    {
        //Pulo
        Jump();

        //Pause menu
        if (Input.GetKeyDown(KeyCode.Escape) && pause == false)
        {
            GerenciadorGame.inst.Pause();
            pause = !pause;

        }else if (Input.GetKeyDown(KeyCode.Escape) && pause)
        {
            GerenciadorGame.inst.DesPausa();
            pause = !pause;
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
            checkGround.numJump--;
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(rb.velocity.x, forceJump));
            GerenciadorAudio.inst.PlayFX(pulo);
        }
    }
}
