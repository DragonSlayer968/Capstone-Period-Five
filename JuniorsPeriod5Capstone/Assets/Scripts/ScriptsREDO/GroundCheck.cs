using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject landSFX;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 3)
        {
            player.anim.SetBool("IsFalling", false);
            if(player.IsJumping == true)
            {
                GameObject landSound = Instantiate(landSFX, transform.position, transform.rotation);
                Destroy(landSound, .5f);
            }
            player.IsJumping = false;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 3)
        {
            player.anim.SetBool("IsFalling", true);
            
        }
    }
}
