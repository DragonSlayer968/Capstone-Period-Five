using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBookBossController : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public Animator body;
    public SpriteRenderer sprite;

    [Header("Stats")]
    public float speed;
    public float walkSpeed;
    public float attackDistance;
    public float health;
    public float maxHealth;
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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Mathf.Abs(gameObject.transform.position.x - player.transform.position.x);
        direction = gameObject.transform.position.x - player.transform.position.x;

        playerHeight = player.transform.position.y - transform.position.y;

        AttackCycle();
        Movement();
    }

    public void Movement()
    {
        if (playerDistance > walkAllowance)
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
           
            body.SetBool("Walking", false);
            rb.velocity = new Vector2(0, 0);

            /* if (isWalking == false && jumpStarted == false)
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
             }*/

        }
        if(CanTrack == true)
        {
            if (direction < 0)
            {
                //sprite.flipX = false;
                transform.eulerAngles = new Vector3(0, 180, 0);
            }

            if (direction > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                // sprite.flipX = true;
            }

        }
       
    }

    /*public void SetSpeed(float sugar)
    {
        speed = sugar;
    }*/

    public void Attack()
    {
        body.SetInteger("Attack", attackValue);
    }

    public void CoolDown()
    {
        attackValue = 0;
        body.SetInteger("Attack", attackValue);
        inCoolDown = true;
    }

    public void AttackCycle()
    {
        if (inCoolDown == true)
        {
            coolDownTime -= Time.deltaTime;
            if (coolDownTime <= 0)
            {
                coolDownTime = origCoolDownTime;
                inCoolDown = false;
                attackValue = Random.Range(1, 5);
                Attack();

            }
        }
    }

    //Attack Commands
    public bool CanTrack = true;

    public void Track()
    {
        CanTrack = true;
    }

    public void StopTracking()
    {
        CanTrack = false;
    }

    public void EndAttack()
    {
        body.SetInteger("Attack", 0);
    }

    public void SetSpeed(float sugar)
    {
        speed = sugar;
    }


    //Attack 1 - Bash
    public void Attack1()
    {
        if(playerDistance <= attackRange)
        {
            player.GetComponent<FillerHealth>().Hit();
        }
    }

    //Attack 2 - Get Clapped
    [Header("Attack 2")]
    public GameObject crossProjectile;
    public float crossRotationValue;

    public void Attack2()
    {
        GameObject crosProj = Instantiate(crossProjectile, player.transform.position, crossProjectile.transform.rotation);
        Destroy(crosProj, 2f);
        //crossRotationValue = Random.Range(0, 360);
        //crosProj.transform.eulerAngles = new Vector3(0, 0, crossRotationValue);
    }

    //Attack 3 - Down Five
    [Header("Attack 3")]
    public int fingerCount;
    public GameObject dfProjectile;
    public GameObject ground;
    public float heightIncrease;

    public void Attack3()
    {
        fingerCount++;
        Vector2 projPoint = new Vector2(player.transform.position.x, ground.transform.position.y + heightIncrease);
        GameObject DFproj = Instantiate(dfProjectile, projPoint, dfProjectile.transform.rotation);
       // Destroy(DFproj, 2f);

        if (fingerCount >= 5)
        {
            CoolDown();
            fingerCount = 0;
        }
    }

    //Attack 4 - A little friend
    [Header("Attack 4")]
    public int summonValue;
    public GameObject[] summons;
    public GameObject summonPoint;

    public void Attack4()
    {
        summonValue = Random.Range(0, summons.Length);
        Instantiate(summons[summonValue], summonPoint.transform.position, summons[summonValue].transform.rotation);
    }



}
