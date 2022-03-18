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

        if (Input.GetKeyDown(KeyCode.H))
        {
            Hit();
        }

        Invincibility();

        

        

    }

    public void Hit()
    {
        if(IV == false)
        {
            anim.SetTrigger("Hit");
            health--;
            IV = true;
            healthPoints[hp - 1].sprite = noHead;
            hp--;
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
