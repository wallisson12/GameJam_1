using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliente : MonoBehaviour
{
    //--Cliente--
    [SerializeField] private bool isRight = true;
    [SerializeField] private Transform A, B;
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float time;
    public Transform pontoNasce;

    //--Audios--
    [SerializeField] private AudioClip portaFecha;

    void Start()
    {
        A = GameObject.Find("A").transform;
        B = GameObject.Find("B").transform;
    }

    void Update()
    {
        if (isRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);

            if (Vector2.Distance(transform.position,B.position) < 0.3f)
            {
                TempoAtendimento();

            }

            transform.position = Vector2.MoveTowards(transform.position, B.position, speed);
        }
        else
        {
            transform.localScale = new Vector3(1,1,1);

            if (Vector2.Distance(transform.position,A.position) < 0.3f)
            {
                //Barulho de porta abrindo ou fechando
                GerenciadorAudio.inst.PlayFX(portaFecha);
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
        time -= Time.deltaTime * 0.4f;

        if (time <= 0)
        {
            isRight = false;
            time = 0f;
        }
    }
}
