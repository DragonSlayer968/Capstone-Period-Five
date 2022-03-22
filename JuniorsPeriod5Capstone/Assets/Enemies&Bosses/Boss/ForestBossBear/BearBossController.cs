using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BearBossController : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public Animator body;
    public SpriteRenderer sprite;

    [Header("Stats")]
    public float speed;
    public float attackDistance;
    //public float health;
    //public float maxHealth;
    public bool Started;

    [Header("Target")]
    public float direction;    
    public float attackAllowance;
    public float attackRange;
    public float heightAllowance;
    public float playerDistance;
    public float playerHeight;

    public float DepthAllowance;
    public GameObject player;
    public float walkAllowance;

    [Header("FillerAttack")]
    public float coolDownTime;
    public float origCoolDownTime;
    public GameObject projectile;
    public Transform shotPoint;
    public float DestroyTime;
    public bool inCoolDown;
    public int attackValue;

    public Slider healthSlider;
    public Text healthText;
    public EnemyHealth enemyHealth;

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
        healthSlider.maxValue = enemyHealth.maxHealth;
        healthSlider.value = enemyHealth.enemyHealth;
        healthText.text = enemyHealth.enemyHealth + "/" + enemyHealth.maxHealth;

        if (Started == true)
        {
          if(isWalking == true || jumpStarted == true)
            {
                playerDistance = Mathf.Abs(gameObject.transform.position.x - player.transform.position.x) + 2;
               
            }
            else
            {
                playerDistance = Mathf.Abs(gameObject.transform.position.x - player.transform.position.x);
                direction = gameObject.transform.position.x - player.transform.position.x;

            }
            
            playerHeight = player.transform.position.y;

           
            
        }
        Movement();
        CycleAttacks();
        if(jumpStarted == true)
        {
            Attack2Jumping();
        }
        if(isWalking == true)
        {
            Walking();
        }

      
    }

    public void Movement()
    {
        if(playerDistance > walkAllowance)
        {
            body.SetBool("Walking", true);
           // rb.velocity = new Vector2(speed, 0);
            if (direction < 0)
            {
                rb.velocity = new Vector2(speed, 0);
            }

            if (direction > 0)
            {
                rb.velocity = new Vector2(-speed, 0);
            }

        }

        else if (playerDistance <= walkAllowance)
        {
            if(isWalking == false && jumpStarted == false)
            {
                body.SetBool("Walking", false);
                rb.velocity = new Vector2(0, 0);
            }
          
            else if (isWalking == true || jumpStarted == true)
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

        if (direction < 0)
        {
            //sprite.flipX = false;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (direction > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            // sprite.flipX = true;
        }

        

    }

    public void SetSpeed(float sugar)
    {
        speed = sugar;
    }

    public void Attack()
    {
        body.SetInteger("Attack", attackValue);
    }

    public void SetRange(float protein)
    {
        attackRange = protein;
    }

    public void CoolDown()
    {
        attackValue = 0;
        body.SetInteger("Attack", attackValue);
        inCoolDown = true;
    }

    public void CycleAttacks()
    {
       
        if(inCoolDown == true)
        {
            coolDownTime -= Time.deltaTime;
            if(coolDownTime <= 0)
            {
                coolDownTime = origCoolDownTime;
                inCoolDown = false;
                attackValue = Random.Range(1, 5);
                Attack();
                
            }
        }

      

    }

    //Attacks

    

    public void Attack1Swipe()
    {
        if(playerDistance <= attackRange  && playerHeight <= heightAllowance && playerHeight >= DepthAllowance)
        {
            player.GetComponent<PlayerHealth>().Hit(1, gameObject);
        }
    }

    [Header("Attack2")]
    public float jumpRange;
    public bool jumpStarted;

    public void Attack2Jump()
    {
        if(jumpStarted == true)
        {
            jumpStarted = false;
            
        }

        else
        {
            jumpStarted = true;
           
        }
    }

    public void Attack2Jumping()
    {
        print("working");
        if(playerDistance <= jumpRange && playerHeight <= heightAllowance && playerHeight >= DepthAllowance)
        {
            player.GetComponent<PlayerHealth>().Hit(1, gameObject);
        }
    }

    [Header("Attack3")]
    public Transform summonPoint;
    public GameObject summon;
    public GameObject[] summons;
   

    public void Attack3Summon()
    {
       

        summon = summons[Random.Range(0, summons.Length)];
       
        GameObject summonObject = Instantiate(summon, summonPoint.position, summon.gameObject.transform.rotation);


    }


    [Header("Attack4")]
    public float walkTime; public float origWalkTime;
    public bool isWalking;
    public float chargeExtension;
    public bool isStunned;
    public float stunTime; public float origStun;

    public void Walk()
    {
        isWalking = true;
        body.SetBool("IsCharging", true);
    }

    public void Walking()
    {
        if(walkTime > 0)
        {
           // playerDistance = Mathf.Abs(gameObject.transform.position.x - player.transform.position.x) + chargeExtension;
            walkTime -= Time.deltaTime;
            if (playerDistance <= attackRange + 2 && playerHeight <= heightAllowance && playerHeight >= DepthAllowance)
            {

                player.GetComponent<PlayerHealth>().Hit(1, gameObject);
                isWalking = false;
                body.SetBool("IsCharging", false);
                walkTime = origWalkTime;
                CoolDown();
            }



        }
        else
        {
            isWalking = false;
            body.SetBool("IsCharging", false);
            walkTime = origWalkTime;
            CoolDown();
        }

    }

    public void Stunned()
    {
        stunTime -= Time.deltaTime;
        if(stunTime <= 0)
        {
            stunTime = origStun;
            body.SetBool("Stunned", false);
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Wall")
        {
            if(isWalking == true)
            {
                isWalking = false;
                body.SetBool("IsCharging", false);
                walkTime = origWalkTime;
                body.SetBool("Stunned", true);
                isStunned = true;
            }
        }
    }

   

}
