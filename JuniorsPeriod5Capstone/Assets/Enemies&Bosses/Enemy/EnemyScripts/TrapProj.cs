using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapProj : MonoBehaviour
{
    public Rigidbody2D rb;
    public float projSpeed;

    public int direction;
    public bool destroysOnImpact;

    // Start is called before the first frame update
    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       if(direction == 1)
       {
            rb.velocity = transform.right * projSpeed;
       }

        if (direction == 2)
        {
            rb.velocity = transform.right * -projSpeed;
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<FillerHealth>().Hit();
            if (destroysOnImpact == true)
            {
                Destroy(gameObject);
            }
        }
    }
}
