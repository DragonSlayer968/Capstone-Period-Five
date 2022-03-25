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
        ink = FindObjectOfType<InkDrop>().gameObject;
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

    public GameObject ink;

    public void Hit(float damage)
    {
        
        if(invincible == false)
        {
            if(enemyHealth > 0)
            {
                //anim.SetTrigger("Hit");

                enemyHealth -= damage;
                GameObject damageShow = Instantiate(popDamageShow, transform.position, transform.rotation);
                damageShow.GetComponent<PopOut>().popManipulation(damage.ToString());
                invincible = true;
            }
           
        }
        
    }

    public int maxcoin, mincoin;

    public void DestroySelfThroughHealth(int coinvalue)
    {
        coinvalue = Random.Range(mincoin, maxcoin + 1);
        if(coin != null)
        {
            Vector3 thing = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            GameObject enemycoin = Instantiate(coin, thing, transform.rotation);
            enemycoin.GetComponent<Coin>().coinValue = coinvalue;

            int randomChance = Random.Range(0, 5);
            if(randomChance == 0)
            {
                Instantiate(ink, transform.position, transform.rotation);
            }

        }
        
        Destroy(gameObject);
    }

}
