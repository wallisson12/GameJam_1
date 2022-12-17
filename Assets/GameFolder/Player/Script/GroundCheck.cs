using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public int numJump;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.IsTouchingLayers(9))
        {
            numJump = 2;
        }
    }

}
