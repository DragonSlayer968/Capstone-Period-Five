using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolController : MonoBehaviour
{
    public GameObject Enemy;
    public int enemyValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MeleeEnemy parent = Enemy.GetComponent<MeleeEnemy>();
        if (parent)
        {
            enemyValue = 1;
        }

       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyValue == 1 && collision.gameObject.layer == 3)
        {
            Enemy.GetComponent<MeleeEnemy>().CanMove = true;
        }

        if (enemyValue == 2 && collision.gameObject.layer == 3)
        {
            Enemy.GetComponent<ShootingEnemy>().CanMove = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(enemyValue == 1 && collision.gameObject.layer == 3)
        {
            Enemy.GetComponent<MeleeEnemy>().CanMove = false;
        }

        if (enemyValue == 2 && collision.gameObject.layer == 3)
        {
            Enemy.GetComponent<ShootingEnemy>().CanMove = true;
        }
    }



}
