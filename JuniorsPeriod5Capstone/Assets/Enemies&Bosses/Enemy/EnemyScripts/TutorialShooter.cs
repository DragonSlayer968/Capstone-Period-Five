using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShooter : MonoBehaviour
{
    public GameObject Player;
    public float playerDist, ShootDist;

    public float bulletTime;
    public GameObject trapProj, trapSP;
    public int setDirection;

    public Animator RisingWater;

    public float TimeTilNextShot;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerDist = Mathf.Abs(transform.position.x - Player.transform.position.x);

        if(TimeTilNextShot <= 0)
        {
            if (playerDist <= ShootDist)
            {
                GetComponent<Animator>().SetBool("Shoot", true);
                TimeTilNextShot = 8f;

            }

            else
            {
                GetComponent<Animator>().SetBool("Shoot", false);
            }
        }

        else
        {
            TimeTilNextShot -= Time.deltaTime;
            GetComponent<Animator>().SetBool("Shoot", false);
        }
       
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(trapProj, trapSP.transform.position, trapSP.transform.rotation);
        bullet.GetComponent<TrapProj>().direction = setDirection;
        bullet.GetComponent<TrapProj>().shooter = gameObject;
        Destroy(bullet, bulletTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Parry")
        {
            RisingWater.SetTrigger("InkRise");
            Destroy(gameObject);
        }
    }

}
