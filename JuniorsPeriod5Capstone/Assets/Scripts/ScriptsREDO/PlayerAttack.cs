using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Components")]
    public Animator anim;
    public PlayerMovement movement;

    [Header("Attacks")]
    public GameObject swordProj;
    public Transform swordPoint;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
    }

    public void FireProj()
    {
        Instantiate(swordProj, swordPoint.position, swordPoint.rotation);
    }

}
