using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillerHealth : MonoBehaviour
{
    public float health;
    public float immunityTime, imOrig;
    public bool isImmune;

    public Animator body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health > 0)
        {
            if (isImmune == true)
            {
                body.SetBool("Immune", true);
                immunityTime -= Time.deltaTime;
                if (immunityTime <= 0)
                {
                    immunityTime = imOrig;
                    isImmune = false;
                    body.SetBool("Immune", false);
                }
            }
        }
       
        if(health <= 0)
        {
            Destroy(gameObject);
        }

        
    }

    public void Hit()
    {
        if(isImmune == false)
        {
            health--;
            isImmune = true;
        }
        
    }

}
