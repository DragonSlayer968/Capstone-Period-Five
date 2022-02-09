using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projSpeed;
    public float projDamage;

    public bool IsRight;

    public Rigidbody2D rb;
    public GameObject HitVFX;

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

    public float offsetX;
    public Vector3 offset;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 3)
        {
            if(IsRight == true)
            {
                 offset = new Vector3(transform.position.x + offsetX, transform.position.y, transform.position.z);
            }

            else
            {
                 offset = new Vector3(transform.position.x - offsetX, transform.position.y, transform.position.z);
            }
            
            GameObject vfx = Instantiate(HitVFX, offset, transform.rotation);
            Destroy(vfx, .55f);
            DestroySelf();
        }

        else if(other.gameObject.layer == 7)
        {
            if (IsRight == true)
            {
                 offset = new Vector3(transform.position.x + offsetX, transform.position.y, transform.position.z);
            }

            else
            {
                 offset = new Vector3(transform.position.x - offsetX, transform.position.y, transform.position.z);
            }
            GameObject vfx = Instantiate(HitVFX, offset, transform.rotation);
            Destroy(vfx, .55f);
            other.GetComponent<EnemyHealth>().Hit(projDamage);
            DestroySelf();
        }
    }


}
