using UnityEngine;

public class PegaPresente : MonoBehaviour
{
    //--Interacao--
    [SerializeField] private Transform obj_Segura;
    [SerializeField] private Transform presente;
    [SerializeField] private SpriteRenderer btn_Interacao;
    [SerializeField] private bool interagir = false;
    [SerializeField] private int carregando = 0;


    void Update()
    {
        //Intergir
        if (Input.GetKeyDown(KeyCode.E) && carregando == 0 && interagir)
        {
            presente.parent = transform;
            presente.position = transform.position;
            presente.GetComponent<Rigidbody2D>().simulated = false;
            carregando = 1;
        }

        if (Input.GetKeyDown(KeyCode.E) && carregando == 1 && interagir == false)
        {
            presente.parent = null;
            presente.GetComponent<Rigidbody2D>().simulated = true;
            carregando = 0;
        }

        if (carregando == 1)
        {
            Podeinteragir(true,false);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Presente"))
        {
            if (carregando == 0)
            {
                presente = other.transform;
                Podeinteragir(true,true);
            }
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Presente"))
        {
            if (carregando == 0)
            {
                presente = null;
                Podeinteragir(false,false);
            }
        }
        
    }

    void Podeinteragir(bool btn, bool interagi)
    {
        btn_Interacao.enabled = btn;
        interagir = interagi;
    }
}
