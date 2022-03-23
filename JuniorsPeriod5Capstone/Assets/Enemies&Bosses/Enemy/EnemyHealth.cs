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
    public bool IsDead;
    public GameObject popDamageShow;
    public GameObject coin;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0 && IsDead == false)
        {
            anim.SetTrigger("Dead");
            IsDead = true;
        }

        if (invincible == true)
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
            GameObject damageShow = Instantiate(popDamageShow, transform.position, transform.rotation);
            damageShow.GetComponent<PopOut>().popManipulation(damage.ToString());
            invincible = true;
        }
        
    }

    public void DestroySelfThroughHealth(int coinvalue)
    {
        if(coin != null)
        {
            GameObject enemycoin = Instantiate(coin, transform.position, transform.rotation);
            enemycoin.GetComponent<Coin>().coinValue += coinvalue;
        }
        
        Destroy(gameObject);
    }

}
