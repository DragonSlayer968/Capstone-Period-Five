using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            anim.SetTrigger("Dead");
        }

       

        Invincibility();
        Parry();
        

        

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

    public void Hit(int Attacker)
    {
        if(Parrying == false)
        {
            if (IV == false)
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
           

            if (Attacker == 1)
            {
                print("parried");
            }

            if(Attacker == 2)
            {
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
       
       
    }

    public void Invincibility()
    {
        if(IV == true)
        {
            IVTime -= Time.deltaTime;
            if(IVTime <= 0)
            {
                IV = false;
                IVTime = IVOrig;
            }
        }
    }

}
