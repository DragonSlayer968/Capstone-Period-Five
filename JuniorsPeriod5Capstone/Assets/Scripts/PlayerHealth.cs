using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health, maxHealth;
    public int hp;
    public bool IV;
    public float IVTime, IVOrig;

    public Animator anim;
    public Image[] healthPoints;
    public Sprite head, noHead;

    public PlayerAttack paExtend;
    public PlayerAbilities abilities;

    public GameObject healthUI;

    // Start is called before the first frame update
    void Start()
    {
        CheckPointController checkcpc = FindObjectOfType<CheckPointController>();
        if (checkcpc)
        {
            FindObjectOfType<CheckPointController>().player = gameObject;
            checkPointValue = PlayerPrefs.GetInt("Cp");
            FindObjectOfType<CheckPointController>().cpvalue = checkPointValue;
            FindObjectOfType<CheckPointController>().CheckPointLoad();
        }

    }

    public bool Dead;

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && Dead == false)
        {
            anim.SetTrigger("Dead");
            Dead = true;
        }



        Invincibility();
        if(blockNotObtained == false)
        {
            Parry();
        }
        


        if(Invulnerable == true)
        {
            healthUI.SetActive(false);
        }

        else
        {
            healthUI.SetActive(true);
        }

    }

    public bool Parrying;
    public float parryDamage;
    public float parryHealing;

    public bool DefensePath;
    public int path;

    public GameObject ParryProj;
    public GameObject ParrySound;

    public void Parry()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) //make cost ink
        {
            ParryChange();
            anim.SetTrigger("Parry");
        }
    }

    public void ParryChange()
    {
        Parrying = !Parrying;
    }


    public bool Invulnerable;
    public bool blockNotObtained;
    public void Hit(int Attacker, GameObject enemy)
    {
       
            if (GetComponent<PlayerMovement>().rollActive == false)
            {
                if (Parrying == false)
                {
                    if (IV == false && Invulnerable == false)
                    {
                        if (abilities.mainPath != 3)
                        {
                            anim.SetTrigger("Hit");
                            health--;
                            IV = true;
                            healthPoints[hp - 1].sprite = noHead;
                            hp--;
                        }

                        else
                        {
                            if (abilities.subPath == 1)
                            {
                                if (abilities.subPathLevel == 0)
                                {
                                    int dodgeChance = Random.Range(0, 10);
                                    if (dodgeChance != 0)
                                    {
                                        anim.SetTrigger("Hit");
                                        health--;
                                        IV = true;
                                        healthPoints[hp - 1].sprite = noHead;
                                        hp--;
                                    }
                                }

                                else
                                {
                                    int dodgeChance = Random.Range(0, 5);
                                    if (dodgeChance != 0)
                                    {
                                        anim.SetTrigger("Hit");
                                        health--;
                                        IV = true;
                                        healthPoints[hp - 1].sprite = noHead;
                                        hp--;
                                    }
                                }
                            }

                            else
                            {
                                int dodgeChance = Random.Range(0, 10);
                                if (dodgeChance == 0)
                                {
                                    if (abilities.subPathLevel >= 1)
                                    {
                                        EnemyHealth check = enemy.GetComponent<EnemyHealth>();

                                        if (check)
                                        {
                                            enemy.GetComponent<EnemyHealth>().enemyHealth -= 10;
                                        }
                                    }
                                }

                                else
                                {
                                    anim.SetTrigger("Hit");
                                    health--;
                                    IV = true;
                                    healthPoints[hp - 1].sprite = noHead;
                                    hp--;
                                }


                            }
                        }




                    }
                }

                else
                {

                    if (abilities.mainPath != 3)
                    {
                        print(Attacker + "Eh");
                        if (Attacker == 1)
                        {
                            print("parried");
                        }

                        if (Attacker == 2)
                        {
                            print("eh");
                            GameObject slashSound = Instantiate(ParrySound, paExtend.swordPoint.position, paExtend.swordPoint.rotation);
                            GameObject proj = Instantiate(ParryProj, paExtend.swordPoint.position, paExtend.swordPoint.rotation);
                            proj.transform.eulerAngles = paExtend.Rotate;
                            if (paExtend.right == true)
                            {
                                proj.GetComponent<Projectile>().IsRight = true;
                            }

                            else
                            {
                                proj.GetComponent<Projectile>().IsRight = false;
                            }
                        }
                    }

                    else
                    {

                        if (abilities.subPath == 1) //Defense - Harmony 
                        {
                            if (abilities.subPathLevel <= 0) //Base - 5% to heal on parry
                            {
                                int healChance = Random.Range(0, 20);
                                if (healChance == 0)
                                {
                                    health++;
                                    hp++;
                                    healthPoints[hp - 1].sprite = head;

                                }
                            }

                            else if (abilities.subPathLevel == 1) //Increases passive chance to not take damage to 20% //8% to heal on parry
                            {
                                int healChance = Random.Range(0, 100);
                                if (healChance <= 7)
                                {
                                    health++;
                                    hp++;
                                    healthPoints[hp - 1].sprite = head;

                                }
                            }

                            else if (abilities.subPathLevel == 2) //10% to heal on parry
                            {
                                int healChance = Random.Range(0, 10);
                                if (healChance == 0)
                                {
                                    health++;
                                    hp++;
                                    healthPoints[hp - 1].sprite = head;

                                }
                            }

                            else //5% to heal to full //15% to heal on parry *Extreme ability (Might be added later instead of heal to full) - Revive after 1st death at 2 hearts
                            {
                                int healChance = Random.Range(0, 20);
                                if (healChance <= 3)
                                {
                                    health++;
                                    hp++;
                                    healthPoints[hp - 1].sprite = head;

                                }

                                int megaHeal = Random.Range(0, 15);
                                if (megaHeal == 0)
                                {
                                    for (int i = 0; i < healthPoints.Length; i++)
                                    {
                                        health++;
                                        hp++;
                                        healthPoints[i].sprite = head;

                                        if (health > maxHealth)
                                        {
                                            health = maxHealth;
                                        }

                                        if (hp > maxHealth)
                                        {
                                            hp = 5;
                                        }
                                    }


                                }

                            }

                        }

                        if (Attacker == 1)
                        {
                            if (abilities.subPath == 2) //Hate
                            {
                                EnemyHealth check = enemy.GetComponent<EnemyHealth>();

                                if (check)
                                {
                                    if (abilities.subPathLevel <= 0) //Base - low damage
                                    {
                                        enemy.GetComponent<EnemyHealth>().enemyHealth -= 5;
                                    }

                                    else if (abilities.subPathLevel == 1) //Passive chance to not take damage also damages attacked enemy
                                    {
                                        enemy.GetComponent<EnemyHealth>().enemyHealth -= 10;
                                    }

                                    else if (abilities.subPathLevel == 2) //Projectile enemies will be damaged directly
                                    {
                                        enemy.GetComponent<EnemyHealth>().enemyHealth -= 15;
                                    }

                                    else //25% to crit the parry dealing 1.5* damage or if projectile sending out two parry projectiles
                                    {
                                        int critchance = Random.Range(0, 3);
                                        if (critchance == 0)
                                        {
                                            enemy.GetComponent<EnemyHealth>().enemyHealth -= 20 * 1.5f;
                                        }
                                        else
                                        {
                                            enemy.GetComponent<EnemyHealth>().enemyHealth -= 20;
                                        }


                                    }
                                }

                            }
                            print("parried");
                        }

                        if (Attacker == 2)
                        {
                            GameObject slashSound = Instantiate(ParrySound, paExtend.swordPoint.position, paExtend.swordPoint.rotation);
                            GameObject proj = Instantiate(ParryProj, paExtend.swordPoint.position, paExtend.swordPoint.rotation);

                            if (abilities.subPath == 2 && abilities.subPathLevel > 1)
                            {


                                EnemyHealth check = enemy.GetComponent<EnemyHealth>();
                                if (check)
                                {
                                    enemy.GetComponent<EnemyHealth>().enemyHealth -= 8;
                                }

                                if (abilities.subPathLevel == 3)
                                {
                                    int critchance = Random.Range(0, 3);
                                    if (critchance == 0)
                                    {
                                        GameObject secondProj = Instantiate(ParryProj, paExtend.swordPoint.position, paExtend.swordPoint.rotation);
                                        secondProj.transform.eulerAngles = paExtend.Rotate;
                                        if (paExtend.right == true)
                                        {
                                            secondProj.GetComponent<Projectile>().IsRight = true;
                                        }

                                        else
                                        {
                                            secondProj.GetComponent<Projectile>().IsRight = false;
                                        }
                                    }
                                }
                            }
                            proj.transform.eulerAngles = paExtend.Rotate;
                            if (paExtend.right == true)
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

            }
        
       

    }

    public GameObject deathCanvas;
    public GameObject deathSound;

    public void Die(GameObject sound)
    {
        deathSound = sound;

        deathCanvas.SetActive(true);
    }

    //DeathButtons
    public int checkPointValue;
    public void Continue()
    {
        PlayerPrefs.SetInt("Cp", checkPointValue);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void Quit()
    {
        PlayerPrefs.SetInt("Cp", 0);
        SceneManager.LoadScene("LevelSelect");
    }


    public void Invincibility()
    {
        if (IV == true)
        {
            IVTime -= Time.deltaTime;
            if (IVTime <= 0)
            {
                IV = false;
                IVTime = IVOrig;
            }
        }
    }

}
