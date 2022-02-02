using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    /*float dirX;
    [SerializeField]
    Rigidbody2D rb;
    AudioSource audioSrc;
    bool isMoving = false;


    void Start ()
    {
        rb = GetComponent<Rigidbody2D> ();
        audioSrc = GetComponent<AudioSource>();
    }

    void Update ()
    {
        dirX = Input.GetAxis("Horizontal") * moveSpeed;

        if (rb.velocity.x != 0)
            isMoving = true;
        else
            isMoving = false;

        if (isMoving)
        {
            if (!audioSrc.isPlaying)
                audioSrc.Play();
        }
        else
            audioSrc.Stop ();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, rb.velocity.y);
    }
    */
}
