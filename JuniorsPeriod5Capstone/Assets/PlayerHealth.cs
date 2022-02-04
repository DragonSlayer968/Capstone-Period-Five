using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health, maxHealth;
    public bool IV;
    public float IVTime, IVOrig;

    public Animator anim;

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
