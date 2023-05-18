using UnityEngine;
using TMPro;

public class GerenciadorUI : MonoBehaviour
{
    public static GerenciadorUI inst;

    //--UI--
    public TextMeshProUGUI Txt_Tempo;
    public TextMeshProUGUI Txt_Dinheiro;

    //--Telas--
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject pause;
    
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
    }

   public void Win()
   {
        win.GetComponent<CanvasGroup>().alpha = 1;
        win.GetComponent<CanvasGroup>().interactable= true;
        win.GetComponent<CanvasGroup>().blocksRaycasts = true;
        Time.timeScale = 0f;
    }

   public void GameOver()
   {
        gameOver.GetComponent<CanvasGroup>().alpha = 1;
        gameOver.GetComponent<CanvasGroup>().interactable = true;
        gameOver.GetComponent<CanvasGroup>().blocksRaycasts = true;
        Time.timeScale = 0f;
    }

    public void PauseGame()
    {
        pause.GetComponent<CanvasGroup>().alpha = 1;
        pause.GetComponent<CanvasGroup>().interactable = true;
        pause.GetComponent<CanvasGroup>().blocksRaycasts = true;
        Time.timeScale = 0f;
    }

    public void DesPauseGame()
    {
        pause.GetComponent<CanvasGroup>().alpha = 0;
        pause.GetComponent<CanvasGroup>().interactable = false;
        pause.GetComponent<CanvasGroup>().blocksRaycasts = false;
        Time.timeScale = 1f;
    }

}
