using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulatableProjectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public int Direction; //1 = at player(moving enemy), 2 = up or down (non moving enemy)
    public float projSpeed;

    public GameObject enemy, player;
    public bool destroysOnImpact;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Direction == 1)
        {
            if (enemy)
            {
                Vector2 myPos = enemy.transform.position; //enemy position
                Vector2 targetPos = player.transform.position; //setting player position as a variable
                Vector2 direction = (targetPos - myPos).normalized; //finding which direction to fire projectile
                rb.velocity = direction * projSpeed; //this launches projectile
            }
            else
            {
                Destroy(gameObject);
            }
           
        }

        if(Direction == 2)
        {
            rb.velocity = transform.up * projSpeed;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().Hit(2, enemy);
            if(destroysOnImpact == true)
            {
                Destroy(gameObject);
            }
        }
    }


}
