using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projSpeed;
    public float projDamage;

    public bool IsRight;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsRight == true)
        {
            rb.velocity = new Vector2(projSpeed, 0);
        }
       
        else
        {
            rb.velocity = new Vector2(-projSpeed, 0);
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
