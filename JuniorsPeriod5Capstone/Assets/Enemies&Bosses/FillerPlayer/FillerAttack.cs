using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillerAttack : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnemyController enemy = FindObjectOfType<EnemyController>();
            if (enemy)
            {
                enemy.health = 0;
            }
        }
    }
}
