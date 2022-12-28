using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texto : MonoBehaviour
{
    public GameObject Textoo,Scan;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Textoo.SetActive(true);
            Scan.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D Other)
    {
        if (Other.CompareTag("Player"))
        {
            Textoo.SetActive(false);
            Scan.SetActive(false);
        }
    }
}
