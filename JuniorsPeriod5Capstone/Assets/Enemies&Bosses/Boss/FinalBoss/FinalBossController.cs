using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossController : MonoBehaviour
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
    public float StatIncrease;

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
    public bool Phase2;

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
        if(Phase2 == true)
        {
            body.SetFloat("Phase2Speed", StatIncrease);
        }

    }

    public void Movement()
    {
        if (playerDistance > walkAllowance)
        {
           // speed = walkSpeed;
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
            //speed = 0;
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

    public void SetSpeed(float sugar)
    {
        speed = sugar;
    }

    public void SetRange(float rangset)
    {
        attackRange = rangset;
    }

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

    public bool CanTrack = true;

    public void Track()
    {
        CanTrack = true;
    }

    public void StopTracking()
    {
        CanTrack = false;
    }

    //Attack 1 - Rapid Punch
    [Header("Attack1")]
    public GameObject FistBallProjectile;
    public Transform fistShotPoint;
    
    public void Attack1RP()
    {
       GameObject FBPproj = Instantiate(FistBallProjectile, fistShotPoint.position, FistBallProjectile.transform.rotation);
        if(direction < 0)
        {
            FBPproj.GetComponent<TrapProj>().direction = 1;
        }
        else
        {
            FBPproj.GetComponent<TrapProj>().direction = 2;
        }

       
    }

    //Attack 2 - Charge
    [Header("Attack2")]
    public bool IsCharging;

    public void StartCharge()
    {
        IsCharging = true;
        StopTracking();
    }

    public void InCharge()
    {

    }

    public void ChargeHit()
    {

    }

    public void ChargeMiss() //cooldown gets doubled until next attack
    {

    }

    //Attack 3 - Explosion Teleport
    [Header("Attack3")]
    public float difference;
    public int teleportCount;

    public void Teleport()
    {
        Vector3 teleportPoint = new Vector3(player.transform.position.x - difference, gameObject.transform.position.y, gameObject.transform.position.z);
        transform.position = teleportPoint;
    }

    public void Explode()
    {
        if(playerDistance <= attackRange)
        {
            player.GetComponent<PlayerHealth>().Hit(1, gameObject);
        }
    }

    public void EndTeleport()
    {
        if(Phase2 == true)
        {
            teleportCount++;
            if(teleportCount >= 3)
            {
                CoolDown();
            }
        }

        else
        {
            CoolDown();
        }

    }

    //Attack 4 - double punch
    [Header("Attack4")]
    public int punchCount;
    public GameObject firstPunchBall;
    public Transform fpbShotPoint;
    public float punchTeleportDifference = 2;

    public void Punch()
    {
        if(Phase2 == true)
        {
            if(punchCount == 0)
            {
                GameObject FirstPunchProjectile = Instantiate(firstPunchBall, fpbShotPoint.position, firstPunchBall.transform.rotation);
                if (direction < 0)
                {
                    FirstPunchProjectile.GetComponent<TrapProj>().direction = 1;
                }
                else
                {
                    FirstPunchProjectile.GetComponent<TrapProj>().direction = 2;
                }
                punchCount = 1;
            }

            else
            {
                
                punchCount = 0;
            }

        }

        if (playerDistance <= attackRange)
        {
            player.GetComponent<PlayerHealth>().Hit(1, gameObject);
        }

    }

    public void PunchTeleport()
    {
        if (Phase2 == true)
        {
            if (direction >= 0)
            {
                transform.position = new Vector3(player.transform.position.x - punchTeleportDifference, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            else
            {
                transform.position = new Vector3(player.transform.position.x + punchTeleportDifference, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            
        }



    }


    //Attack 5 - Summon
    [Header("Attack5")]
    public GameObject summon;
    public Transform[] summonPoints;

    public void SummonMinions()
    {
        for(int i = 0; i < summonPoints.Length; i++)
        {
            Instantiate(summon, summonPoints[i].position, summon.transform.rotation);
        }
    }



}
