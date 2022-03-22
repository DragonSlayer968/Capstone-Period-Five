using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyLandTraps : MonoBehaviour
{
    public int trapType;
    public float damageValue;

    public float timeBetweenAttacks, TBAOrig;
    public float bulletTime;
    public GameObject trapProj, trapSP;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (trapType == 2)
        {
            timeBetweenAttacks -= Time.deltaTime;
            if (timeBetweenAttacks <= 0)
            {
                Attack();
                timeBetweenAttacks = TBAOrig;
            }
        }
    }

    public int setDirection;

    private void Attack()
    {
        GameObject bullet = Instantiate(trapProj, trapSP.transform.position, trapSP.transform.rotation);
        bullet.GetComponent<TrapProj>().direction = setDirection;
        Destroy(bullet, bulletTime);
    }

   

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (trapType == 1 && other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().Hit(0, gameObject);
        }
    }


}
