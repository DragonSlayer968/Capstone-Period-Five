using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoots : MonoBehaviour
{
    public float SlideDamage;
    public float landDamage;

    public PlayerAbilities abilities;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BossHealth bhCheck = collision.GetComponent<BossHealth>();
        EnemyHealth ehCheck = collision.GetComponent<EnemyHealth>();


        if(abilities.mainPath == 2)
        {
            if(abilities.subPathLevel >= 2)
            {
                if (abilities.subPath == 1)
                {
                    if (bhCheck)
                    {
                        bhCheck.health -= landDamage;
                    }

                    if (ehCheck)
                    {
                        ehCheck.enemyHealth -= landDamage;
                    }
                }

                if (abilities.subPath == 2)
                {
                    if (bhCheck)
                    {
                        bhCheck.health -= SlideDamage;
                    }

                    if (ehCheck)
                    {
                        ehCheck.enemyHealth -= SlideDamage;
                    }
                }
            }
            
        }
    }


}
