using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public Animator body;
    public SpriteRenderer sprite;

    [Header("Stats")]
    public float speed;
    public float attackDamage;
    public float health;
    

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

        if (health == 0)
        {
            body.SetTrigger("Dead");
        }
        health = GetComponent<EnemyHealth>().enemyHealth;
    }



    public void RangeController()
    {
        playerDistance = Mathf.Abs(gameObject.transform.position.x - player.transform.position.x);
        playerHeight = gameObject.transform.position.y - player.transform.position.y;
        direction = gameObject.transform.position.x - player.transform.position.x;

       
            if (playerDistance <= walkAllowance)
            {
                if (playerHeight <= gameObject.transform.position.y + heightAllowance || playerHeight >= gameObject.transform.position.y + depthAllowance)
                {
                        body.SetBool("Walking", true);
                        if (FirstStrike == true)
                        {
                            attackTime = origAttackTime;
                        }

                        
                            if (direction < 0)
                            {
                                rb.velocity = new Vector2(-speed, 0);
                            }

                            if (direction > 0)
                            {
                                rb.velocity = new Vector2(speed, 0);
                            }
                        
                    
                }


            }

        

        if (playerDistance > walkAllowance)
        {
            body.SetBool("Walking", false);
            rb.velocity = new Vector2(0, 0);
        }

        if (playerDistance <= lookAtAllowance)
        {
            if (playerHeight <= gameObject.transform.position.y + heightAllowance || playerHeight >= gameObject.transform.position.y + depthAllowance)
            {
               
                    if (direction < 0)
                    {
                        sprite.flipX = true; //possibly make rotation instead
                    }

                    if (direction > 0)
                    {
                        sprite.flipX = false;
                    }
                
            }


        }



        if (playerDistance <= attackAllowance && HasAttacked == false)
        {
            if (playerDistance <= lookAtAllowance)
            {
                if (playerHeight <= gameObject.transform.position.y + heightAllowance || playerHeight >= gameObject.transform.position.y + depthAllowance)
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

    public void Attack()
    {
        if (playerDistance <= attackAllowance + attackExtension)
        {
            if (player)
            {
                player.GetComponent<FillerHealth>().Hit();
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
