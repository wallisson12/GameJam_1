using UnityEngine;

public class RecebePresente : MonoBehaviour
{
    //--Cliente--
    [SerializeField] private Transform seguraPresente;

    //--Pedido--
    [SerializeField] private string descricao;

    //--Acerto/Erro--
    [SerializeField] private GameObject Acerto, Erro;

    //--SoundFx--
    [SerializeField]
    private AudioClip dinheiro_Fx, errou_Fx;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Presente") && other.GetComponent<Presente>().descricao_P == descricao)
        {
            if (other.GetComponent<Rigidbody2D>().simulated)
            {
                other.transform.parent = seguraPresente.transform;
                other.transform.position = seguraPresente.transform.position;

                other.GetComponent<Rigidbody2D>().simulated = false;
                other.GetComponent<BoxCollider2D>().enabled = false;


                Acerto.SetActive(true);

                //Adciona audio
                GerenciadorAudio.inst.PlayFX(dinheiro_Fx);

                //Adiciona dinheiro
                GerenciadorGame.inst.AddDinheiro(other.GetComponent<Presente>().preco);

                //Retira Presente
                GerenciadorGame.inst.PresentesCena--;

            }
        }

        if (other.CompareTag("Presente") && other.GetComponent<Presente>().descricao_P != descricao)
        {
            Erro.SetActive(true);
            GerenciadorAudio.inst.PlayFX(errou_Fx);
        }
    }
}
