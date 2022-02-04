using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
