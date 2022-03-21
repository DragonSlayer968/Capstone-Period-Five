using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttack : MonoBehaviour
{
    [Header("Components")]
    public Animator anim;
    public PlayerMovement movement;
    public Vector3 Rotate;
    public bool right = true;
    public PlayerAbilities abilities;

    [Header("Attacks")]
    public GameObject swordProj;
    public Transform swordPoint;
    public float inkValue, maxInk;
    public float slashCost;
    public float inkGain;
    public float timeBeforeInk, TBIorig;
    public float inkMultiplyer;

    public bool slashPrimed;
    public float meleeDamage;


    [Header("UI")]
    public Slider InkSlider;

    [Header("Target")]
    public BossHealth targetBoss;
    public List<EnemyHealth> targets;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        CheckStateOfMovement();
        InkController();

        if (Input.GetKeyDown(KeyCode.S))
        {
            slashPrimed = true;
        }

        else if (Input.GetKeyUp(KeyCode.S))
        {
            slashPrimed = false;
        }

        if(inkValue > maxInk)
        {
            inkValue = maxInk;
        }

        InkSlider.value = inkValue; InkSlider.maxValue = maxInk;

        StatController();
    }

    public float baseInkCost;
    public float inkCostAfterAbility;

    public float baseDamage;
    public float damageAfterAbility;

    public float baseproDamage;
    public float projdamageafterability;

    public void StatController()
    {
        if(abilities.mainPath == 1)
        {
            if (abilities.subPath == 1)
            {
                if(abilities.subPathLevel <= 1)
                {
                    meleeDamage = baseDamage / 2;
                    slashCost = baseInkCost / 2;
                    projectileDamage = baseproDamage * 1.25f;
                }

                if(abilities.subPathLevel >= 2)
                {
                    meleeDamage = baseDamage / 4;
                    slashCost = baseInkCost / 5;
                }

            }

            else if (abilities.subPath == 2)
            {
                projectileDamage = baseproDamage;
                if (abilities.subPathLevel <= 1)
                {
                    meleeDamage = baseDamage * 1.5f;
                    slashCost = baseInkCost * 1.5f;
                }

                if (abilities.subPathLevel >= 2)
                {
                    meleeDamage = baseDamage * 2;
                    slashCost = baseInkCost * 2;
                }
            }

        }

        else
        {
            meleeDamage = baseDamage;
            slashCost = baseInkCost;
            projectileDamage = baseproDamage;
        }

    }

    public GameObject slashSFX;
    public void Slash()
    {
        GameObject slashSound = Instantiate(slashSFX, swordPoint.position, swordPoint.rotation);
        if (inkValue >= slashCost && slashPrimed == true)
        {

            timeBeforeInk = TBIorig;
            Recharging = false;
            FireProj();
            inkValue -= slashCost;
        }

        else
        {
            if (targetBoss)
            {
                targetBoss.Hit(meleeDamage);
                inkValue += inkGain;
            }

            if(targets.Count > 0)
            {
                for(int i = 0; i < targets.Count; i++)
                {
                    if(abilities.mainPath == 1 && abilities.subPath == 2)
                    {
                        float finalDamage = meleeDamage;
                        int initialCritChance = 0;
                        if (abilities.subPathLevel == 3)
                        {
                            initialCritChance = Random.Range(0, 7);
                        }
                        else
                        {
                            initialCritChance = Random.Range(0, 10);
                        }
                        if(initialCritChance == 0)
                        {
                            finalDamage += meleeDamage * 1.5f;
                        }

                        if(abilities.subPathLevel == 2)
                        {
                            int doublecrit = Random.Range(0, 10);
                            if(doublecrit == 0)
                            {
                                finalDamage += meleeDamage * 1.5f;
                            }
                        }

                        if (abilities.subPathLevel == 3)
                        {
                            int triplecrit = Random.Range(0, 10);
                            if (triplecrit == 0)
                            {
                                finalDamage += meleeDamage * 1.5f;
                            }
                        }

                        targets[i].Hit(finalDamage);


                        if (targets[i].enemyHealth <= targets[i].maxHealth * .12 && abilities.subPathLevel == 1) //Jagged blade excute
                        {
                            targets[i].enemyHealth = 0;
                            inkValue += inkGain * 3;
                        }

                    }
                    else
                    {
                        targets[i].Hit(meleeDamage);
                        if(abilities.mainPath == 1 && abilities.subPath == 1 && abilities.subPathLevel >= 1)
                        {
                            inkValue += inkGain;
                        }
                        
                    }
                    
                }
            }

            
            
        }
        
      
    }

    public bool Recharging;

    public void InkController()
    {
        if(inkValue < slashCost)
        {
            Recharging = true;
        }

        if(Recharging == true)
        {
            timeBeforeInk -= Time.deltaTime;
            if (timeBeforeInk <= 0)
            {
                inkValue += Time.deltaTime * inkMultiplyer;
                if (inkValue >= maxInk)
                {
                    inkValue = maxInk;
                    timeBeforeInk = TBIorig;
                    Recharging = false;
                }
            }
        }

    }

    public void InkRecharge()
    {
        
    }

    public void CheckStateOfMovement()
    {
        if (movement.moveInput != 0)
        {
            anim.SetFloat("StateOfBeing", 1f);
        }
        else
        {
            anim.SetFloat("StateOfBeing", 0f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("Attack");

        }

        if (movement.moveInput < 0)
        {
            Rotate = new Vector3(0, 180, 0);
            right = false;
        }

        else if (movement.moveInput > 0)
        {
            Rotate = new Vector3(0, 0, 0);
            right = true;
        }
    }

    public GameObject swordProj2, swordProj3, swordProj4;
    public float projectileDamage;

    public void FireProj()
    {                      
        if(abilities.mainPath == 1)
        {
            int critchance = 0;
            if (abilities.subPathLevel == 3)
            {
                critchance = Random.Range(0, 7);
            }

            else
            {
                critchance = Random.Range(0, 10);
            }

            if (abilities.subPathLevel != 3)
            {
                GameObject proj = Instantiate(swordProj, swordPoint.position, swordPoint.rotation);
                proj.GetComponent<Projectile>().projDamage = projectileDamage;

                if (critchance == 0)
                {

                    GameObject proj2 = Instantiate(swordProj2, swordPoint.position, swordPoint.rotation);
                    proj2.GetComponent<Projectile>().projDamage = projectileDamage;
                    proj2.transform.eulerAngles = Rotate;
                    if (right == true)
                    {
                        proj2.GetComponent<Projectile>().IsRight = true;
                    }

                    else
                    {
                        proj2.GetComponent<Projectile>().IsRight = false;
                    }

                    if (abilities.subPathLevel == 2)
                    {
                        int secondCrit = Random.Range(0, 4);

                        if (secondCrit == 0)
                        {
                            GameObject proj3 = Instantiate(swordProj3, swordPoint.position, swordPoint.rotation);
                            proj3.GetComponent<Projectile>().projDamage = projectileDamage;
                            proj3.transform.eulerAngles = Rotate;
                            if (right == true)
                            {
                                proj3.GetComponent<Projectile>().IsRight = true;
                            }

                            else
                            {
                                proj3.GetComponent<Projectile>().IsRight = false;
                            }
                        }

                    }

                }

                proj.transform.eulerAngles = Rotate;
                if (right == true)
                {
                    proj.GetComponent<Projectile>().IsRight = true;
                }

                else
                {
                    proj.GetComponent<Projectile>().IsRight = false;
                }
            }

            else
            {
                if(critchance == 0)
                {
                    GameObject proj4 = Instantiate(swordProj4, swordPoint.position, swordPoint.rotation);
                    proj4.transform.eulerAngles = Rotate;
                    proj4.GetComponent<Projectile>().projDamage = projectileDamage * 2.5f;
                    if (right == true)
                    {
                        proj4.GetComponent<Projectile>().IsRight = true;
                    }

                    else
                    {
                        proj4.GetComponent<Projectile>().IsRight = false;
                    }
                }

                else
                {
                    GameObject proj = Instantiate(swordProj, swordPoint.position, swordPoint.rotation);
                    proj.GetComponent<Projectile>().projDamage = projectileDamage;
                    proj.transform.eulerAngles = Rotate;
                    if (right == true)
                    {
                        proj.GetComponent<Projectile>().IsRight = true;
                    }

                    else
                    {
                        proj.GetComponent<Projectile>().IsRight = false;
                    }
                }

            }
           

        }

        else
        {
            GameObject proj = Instantiate(swordProj, swordPoint.position, swordPoint.rotation);
            proj.GetComponent<Projectile>().projDamage = projectileDamage;
            proj.transform.eulerAngles = Rotate;
            if (right == true)
            {
                proj.GetComponent<Projectile>().IsRight = true;
            }

            else
            {
                proj.GetComponent<Projectile>().IsRight = false;
            }
        }
        

       
    }

}
