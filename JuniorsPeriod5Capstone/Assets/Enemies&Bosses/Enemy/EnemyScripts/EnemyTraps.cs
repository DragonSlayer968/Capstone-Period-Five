using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTraps : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public Animator body;
    public SpriteRenderer sprite;

    [Header("Stats")]
    public float speed;
    public float attackDamage;
    public float health;
   
    public float enemyRangeType; //1 = melee, 2 = projectile

    [Header("Target")]
    public float walkAllowance;
    public float direction;
    public float lookAtAllowance;
    public float attackAllowance;
    public float attackExtension;
    public float playerDistance;
    public float playerHeight;
    public float heightAllowance;
    public float depthAllowance;
    public GameObject player;

    [Header("FillerAttack")]
    public float attackTime;
    public float origAttackTime;
    public GameObject projectile;
    public Transform shotPoint;
    public float DestroyTime;
    public bool FirstStrike;
    public bool HasAttacked;
    public bool StationaryShooter;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        body = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            RangeController();
        }
      
        if(health == 0)
        {
            body.SetTrigger("Dead");
        }

        if(enemyRangeType == 1)
        {
            health = GetComponent<EnemyHealth>().enemyHealth;
        }


    }

   

    public void RangeController()
    {
        playerDistance = Mathf.Abs(gameObject.transform.position.x - player.transform.position.x);
        playerHeight = gameObject.transform.position.y - player.transform.position.y;
        direction = gameObject.transform.position.x - player.transform.position.x;

        if (playerDistance <= lookAtAllowance)
        {
            if(playerHeight <= heightAllowance && playerHeight >= gameObject.transform.position.y)
            {
                if (enemyRangeType == 1)
                {
                    if (direction < 0)
                    {
                        sprite.flipX = true;
                    }

                    if (direction > 0)
                    {
                        sprite.flipX = false;
                    }
                }
            }
           
           
        }
       


        if (playerDistance <= attackAllowance && HasAttacked == false)
        {
           
            if(playerDistance <= lookAtAllowance)
            {
                
                if (playerHeight <= heightAllowance && playerHeight >= depthAllowance)
                {
                    print("Check3");
                    if (enemyRangeType == 1)
                    {
                        attackTime -= Time.deltaTime;
                        if (attackTime <= 0)
                        {
                            body.SetTrigger("Attack");
                            HasAttacked = true;
                        }
                    }
                }
               
            }
          
           
           
        }

        if(playerDistance <= walkAllowance && enemyRangeType == 2 && HasAttacked == false && playerHeight <= heightAllowance && playerHeight >= gameObject.transform.position.y)
        {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0)
            {

                body.SetTrigger("Attack");
                HasAttacked = true;
            }
        }
       

    }

    public void Attack()
    {
        if(playerDistance <= attackAllowance + attackExtension && playerHeight <= heightAllowance && playerHeight >= gameObject.transform.position.y)
        {
            if (player)
            {
                player.GetComponent<PlayerHealth>().Hit(1, gameObject);
            }
           
        }
      
    }

    public void Shoot()
    {
        GameObject projectileEnemy = Instantiate(projectile, shotPoint.position, shotPoint.rotation);
        projectileEnemy.GetComponent<ManipulatableProjectile>().enemy = gameObject;
        Destroy(projectileEnemy, DestroyTime);
    }

    public void EndAttack()
    {
        attackTime = origAttackTime;
        HasAttacked = false;
        FirstStrike = false;
    }

    public void SetSpeed(float setValue)
    {
        speed = setValue;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
