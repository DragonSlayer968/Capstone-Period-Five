using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [Header("Components")]
    public Animator anim; //player animator
    public Rigidbody2D rb; //player rigidbody

    [Header("Values")]
    //Horizontal Movement
    public float speed; //how fast player is moving
    public float moveInput; //- = left, + = right

    //Vertical Movement
    public float jumpPower; // Amount of force when jumping
    public float groundCheckDist = 8f; // Jump ray length


    [Header("Bools")]
    public bool facingRight = true;
    public bool IsJumping;
    public bool canMove = true;

    [Header("Other")]
    //Jump
    public LayerMask ground; // What layer the player can jump on

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>(); //rb = the rigidbody on the object
        anim = GetComponent<Animator>(); //anim = the animator on the object
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Movement(); //Calls movement
            Jump();
        }
        MovementAnimation(); //calls movementanimation
        JumpCheck();
        SetSafePlace();
    }

    public void Movement() //Horizontal Movement
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (facingRight == false && moveInput > 0) //if facingRight is false but move input is positive turn facingRight to true
        {
           
            Flip(); //calls flip
        }
        else if (facingRight == true && moveInput < 0) //if facingRight is true but move input is negative turn facingRight to false
        {
            
           Flip(); //calls flip
        }
    }

    public void Flip() //Faces player in direction they are moving
    {
        //if (IsInAttack == false)
        //{
        facingRight = !facingRight; //true = false, false = true
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1; //flip
        transform.localScale = Scaler;
        //}
    }

    public void MovementAnimation() //Controls the animation of the movement
    {
        if (moveInput != 0) //If player is moving
        {
            anim.SetFloat("HorizontalValue", 1f); //blendtree: walk

        }
        else //otherwise
        {
            anim.SetFloat("HorizontalValue", 0); //blendtree: idle

        }
    }

    public GameObject jumpSFX;

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsJumping == false)
        {
            //audioSrc.clip = jumpingSound;
            //audioSrc.Play();
            GameObject jumpSound = Instantiate(jumpSFX, transform.position, transform.rotation);
            Destroy(jumpSound, .5f);
            Vector3 trajectory = transform.up * jumpPower; // Where the player will jump to
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDist, ground); // Ground check

            if (hit)
            {
                //audioSrc.clip = jumpingSound;
                IsJumping = true;
                anim.SetTrigger("Leap");
                rb.AddForce(trajectory); // Jump to goal position
            }
        }
    }


    public bool CanJump; 

    void JumpCheck()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDist, ground); // Ground check
        if (hit)
        {
            CanJump = true;
        }

        else
        {
            CanJump = false;
        }

        if(CanJump == true)
        {
            IsJumping = false;
        }

    }


    public Vector3 LastPos;
    public float posTime, posTimeOrig;

    public void SetSafePlace()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDist, ground);
        if (hit)
        {
            posTime -= Time.deltaTime;
            if(posTime <= 0)
            {
                posTime = posTimeOrig;
                LastPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
        }
    }

    public void TeleportToSafePlace()
    {
        transform.position = LastPos;
    }


}
