using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public bool patrol;
    public int patrolValue;

    public Vector3[] patrolPoints;

    public float speedPatrol;
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(patrol == true)
        {
            anim.SetBool("Walking", true);
            if (patrolValue == 0) // left
            {
                if(transform.position.x <= patrolPoints[patrolValue].x)
                {
                    patrolValue = 1;
                }

                else
                {
                    sprite.flipX = false;
                    rb.velocity = new Vector2(-speedPatrol, 0);
                }

            }

            else //right
            {
                if (transform.position.x >= patrolPoints[patrolValue].x)
                {
                    patrolValue = 0;
                }

                else
                {
                    sprite.flipX = true;
                    rb.velocity = new Vector2(speedPatrol, 0);
                }
            }
        }


    }
}
