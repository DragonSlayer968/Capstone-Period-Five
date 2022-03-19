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
                    targets[i].Hit(meleeDamage);
                    inkValue += inkGain;
                }
            }

            print("normalAttack");
            
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

    public void FireProj()
    {
        GameObject proj = Instantiate(swordProj, swordPoint.position, swordPoint.rotation);
       

        proj.transform.eulerAngles = Rotate;
        if(right == true)
        {
            proj.GetComponent<Projectile>().IsRight = true;
        }

        else
        {
            proj.GetComponent<Projectile>().IsRight = false;
        }
        

       
    }

}
