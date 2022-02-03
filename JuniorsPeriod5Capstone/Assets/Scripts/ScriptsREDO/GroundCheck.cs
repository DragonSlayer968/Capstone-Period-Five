using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerMovement player;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
          

        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
           

        }
    }
}
