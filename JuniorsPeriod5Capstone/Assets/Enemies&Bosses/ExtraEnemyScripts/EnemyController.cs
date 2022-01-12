using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public Animator body;
    public SpriteRenderer sprite;

    [Header("Stats")]
    public float speed;
    public float attackDamage;
    public float health;
    public float enemyType; //1 = moving, 2 = stationary
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
        if(enemyRangeType == 2 && enemyType == 2)
        {
            StationaryShooter = true;
        }
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

    }

   

    public void RangeController()
    {
        playerDistance = Mathf.Abs(gameObject.transform.position.x - player.transform.position.x);
        playerHeight = gameObject.transform.position.y - player.transform.position.y;
        direction = gameObject.transform.position.x - player.transform.position.x;

        if(enemyType == 1)
        {
            if (playerDistance <= walkAllowance)
            {
                if(playerHeight <= gameObject.transform.position.y + heightAllowance || playerHeight >= gameObject.transform.position.y + depthAllowance)
                {
                    if (enemyRangeType == 1 && playerDistance > attackAllowance)
                    {
                        body.SetBool("Walking", true);
                        if (FirstStrike == true)
                        {
                            attackTime = origAttackTime;
                        }

                        if (enemyRangeType == 1)
                        {
                            if (direction < 0)
                            {
                                rb.velocity = new Vector2(speed, 0);
                            }

                            if (direction > 0)
                            {
                                rb.velocity = new Vector2(-speed, 0);
                            }
                        }

                    }

                    if (enemyRangeType == 2)
                    {
                        body.SetBool("Walking", true);
                        if (FirstStrike == true)
                        {
                            attackTime = origAttackTime;
                        }

                        if (enemyRangeType == 2)
                        {
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
                }
              

            }
               
        }
       
        if (playerDistance > walkAllowance && enemyType == 1)
        {
            body.SetBool("Walking", false);
            rb.velocity = new Vector2(0, 0);
        }

        if (playerDistance <= lookAtAllowance)
        {
            if(playerHeight <= gameObject.transform.position.y + heightAllowance || playerHeight >= gameObject.transform.position.y + depthAllowance)
            {
                if (StationaryShooter == false)
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
                if (playerHeight <= gameObject.transform.position.y + heightAllowance || playerHeight >= gameObject.transform.position.y + depthAllowance)
                {
                    if (enemyType == 1 && enemyRangeType == 1)
                    {
                        body.SetBool("Walking", false);
                        rb.velocity = new Vector2(0, 0);
                    }

                    if (StationaryShooter == false)
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

        if(playerDistance <= walkAllowance && enemyType == 2 && enemyRangeType == 2 && HasAttacked == false)
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
        if(playerDistance <= attackAllowance + attackExtension)
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
