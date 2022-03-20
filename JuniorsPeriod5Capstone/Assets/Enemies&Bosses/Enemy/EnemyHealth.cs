using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth;
    public float maxHealth;
    public bool invincible;
    public float IV, IVOrig;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincible == true)
        {
            IV -= Time.deltaTime;
            if(IV <= 0)
            {
                IV = IVOrig;
                invincible = false;
            }
        }
    }

    public void Hit(float damage)
    {
        
        if(invincible == false)
        {
            anim.SetTrigger("Hit");

            enemyHealth -= damage;
            invincible = true;
        }
        
    }

}
