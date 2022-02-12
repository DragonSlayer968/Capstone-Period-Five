using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRange : MonoBehaviour
{
    public PlayerAttack attackController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        BossHealth boss = collision.GetComponent<BossHealth>();
        EnemyHealth enemyTarget = collision.GetComponent<EnemyHealth>();

        if (boss)
        {
            attackController.targetBoss = boss;
        }

        if (enemyTarget)
        {
            attackController.targets.Add(enemyTarget);
        }


    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        BossHealth boss = collision.GetComponent<BossHealth>();
        EnemyHealth enemyTarget = collision.GetComponent<EnemyHealth>();

        if (boss)
        {
            attackController.targetBoss = null;
        }

        if (enemyTarget)
        {
            attackController.targets.Remove(enemyTarget);
        }
    }

}
