using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public bool CanMove;

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
        health = GetComponent<EnemyHealth>().enemyHealth;
        if (player)
        {
            
            RangeController();
               
            

            if(CanMove == false)
            {
                body.SetBool("Walking", false);
            }
            
        }

       
    }

   

    public void RangeController()
    {
        playerDistance = Mathf.Abs(gameObject.transform.position.x - player.transform.position.x);
       
        playerHeight = Mathf.Abs(gameObject.transform.position.y - player.transform.position.y);
        direction = gameObject.transform.position.x - player.transform.position.x;

        if (playerHeight <= heightAllowance && playerHeight >= depthAllowance)
        {
            if (CanMove == true)
            {
                if (playerDistance <= walkAllowance)
                {


                    if (playerDistance > attackAllowance)
                    {
                        body.SetBool("Walking", true);
                        if (FirstStrike == true)
                        {
                            attackTime = origAttackTime;
                        }


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

                else
                {
                    body.SetBool("Walking", false);
                    rb.velocity = new Vector2(0, 0);
                }

                if (playerDistance <= lookAtAllowance)
                {

                    if (direction < 0)
                    {
                        transform.eulerAngles = new Vector3(0, 180, 0);
                        //sprite.flipX = true;
                    }

                    if (direction > 0)
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        //sprite.flipX = false;
                    }

                }
            }
           

           
            if (playerDistance <= attackAllowance && HasAttacked == false && playerHeight <= 1)
            {
                body.SetBool("Walking", false);
                rb.velocity = new Vector2(0, 0);

                attackTime -= Time.deltaTime;
                if (attackTime <= 0)
                {

                    body.SetTrigger("Attack");
                    HasAttacked = true;
                }
                
            }
        }

        else
        {
            body.SetBool("Walking", false);
            rb.velocity = new Vector2(0, 0);
        }

    }

    public void Attack()
    {
        if (playerDistance <= attackAllowance + attackExtension)
        {
            if (player)
            {
                player.GetComponent<PlayerHealth>().Hit(1, gameObject);
            }

        }

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
