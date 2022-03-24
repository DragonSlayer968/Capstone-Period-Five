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
    public float origSpeed;
    public float SprintMultiplyer;
    public float moveInput; //- = left, + = right

    //Vertical Movement
    public float jumpPower; // Amount of force when jumping
    public float groundCheckDist = 8f; // Jump ray length

    //Roll
    public float rollInkCost;

    [Header("Bools")]
    public bool facingRight = true;
    public bool IsJumping;
    public bool canMove = true;

    //Unlocks
    public bool rollUnlocked; public int rollunlockCheck;
    public bool doubleJumpUnlocked;

    //Tutorial
    public bool jtDone;

    public PlayerAbilities abilities;


    [Header("Other")]
    //Jump
    public LayerMask ground; // What layer the player can jump on
    AnimatorClipInfo[] m_CurrentClipInfo;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>(); //rb = the rigidbody on the object
        anim = GetComponent<Animator>(); //anim = the animator on the object
        m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(rollActive == false)
        {
            if (canMove)
            {
                Movement(); //Calls movement
                JumpEew();
            }
            MovementAnimation(); //calls movementanimation
            JumpCheck();
            SetSafePlace();
           
            if(rollUnlocked == true)
            {
                RollActivate();
            }
           
           



        }

        else
        {
            Roll();
        }

        if(abilities.mainPath == 2 && abilities.subPath == 2 && abilities.subPathLevel >= 3)
        {
            if (Input.GetKeyDown(KeyCode.Space) && rollActive == true)
            {
                RollDeactive();
            }
        }

        AbilityStats();
        if(PlayerPrefs.GetInt("HasRoll") == 1)
        {
            rollUnlocked = true;
            rollunlockCheck = 1;
        }
        else
        {
            rollUnlocked = false;
            rollunlockCheck = 0;
        }
       
    }

    public float baseJumpPower, baseRollSpeed, baseRollCost;

    public void AbilityStats()
    {
        if(abilities.mainPath != 2)
        {
            jumpPower = baseJumpPower;
            rollSpeed = baseRollSpeed;
            rollInkCost = baseRollCost;
            anim.SetFloat("RollSpeed", 1);
        }

        else
        {
            if(abilities.subPath == 1)
            {
                jumpPower = baseJumpPower * 1.15f;
                rollSpeed = baseRollSpeed * .8f;
                rollInkCost = baseRollCost;
                anim.SetFloat("RollSpeed", 1);
            }

            if (abilities.subPath == 2)
            {
                jumpPower = baseJumpPower * 1f;
                
                rollInkCost = 0;

                if(abilities.subPathLevel >= 3)
                {
                    anim.SetFloat("RollSpeed", .5f);
                    rollSpeed = baseRollSpeed * 1.55f;
                }

                else
                {
                    rollSpeed = baseRollSpeed * 1.25f;
                }

            }
        }

    }

    public void RollActivate()
    {
        if (m_CurrentClipInfo[0].clip.name == "Run" || m_CurrentClipInfo[0].clip.name == "Idle")
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDist, ground); // Ground check

            if (hit)
            {
                if (Input.GetKeyDown(KeyCode.V) && ink.inkValue >= rollInkCost)
                {
                    rollControl();
                    anim.SetTrigger("Roll");
                    ink.inkValue -= rollInkCost;
                }
            }
        }

        
    }

    public void RollDeactive()
    {
        anim.SetTrigger("RollStop");
        rollActive = false;
    }

    public void Movement() //Horizontal Movement
    {
        moveInput = Input.GetAxis("Horizontal");
        if(abilities.mainPath == 2)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = origSpeed * SprintMultiplyer;

            }

            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = origSpeed;
            }
        }

        else
        {
            speed = origSpeed;
        }
        


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
        if(facingRight == true)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        else
        {
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        //Vector3 Scaler = transform.localScale;
        //Scaler.x *= -1; //flip
        //transform.localScale = Scaler;
        //}
    }

    public void MovementAnimation() //Controls the animation of the movement
    {
        if (moveInput != 0) //If player is moving
        {
            if(speed == origSpeed * SprintMultiplyer)
            {
                anim.SetFloat("HorizontalValue", 1f); //blendtree: sprint
            }

            else
            {
                anim.SetFloat("HorizontalValue", .5f);
            }
           

        }
        else //otherwise
        {
            anim.SetFloat("HorizontalValue", 0); //blendtree: idle

        }
    }

    public GameObject jumpSFX;
    public GameObject doubleJumpVFX;
    public GameObject DJpoint;
    public int jumpValue;
    public int djInKCost;

    public PlayerAttack ink;

    /*void Jump()
    {
        ink = GetComponent<PlayerAttack>();

        if (Input.GetButtonDown("Jump") && IsJumping == false)
        {
            //audioSrc.clip = jumpingSound;
            //audioSrc.Play();

            Vector3 trajectory = transform.up * jumpPower; // Where the player will jump to
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDist, ground); // Ground check

            if (hit)
            {
                //audioSrc.clip = jumpingSound;
                GameObject jumpSound = Instantiate(jumpSFX, transform.position, transform.rotation);
                Destroy(jumpSound, .5f);
                //IsJumping = true;
                anim.SetTrigger("Leap");
                rb.AddForce(trajectory); // Jump to goal position
            }

        }

        else if (Input.GetButtonDown("Jump") && jumpValue != 1 && IsJumping == true && ink.inkValue >= djInKCost)
        {
            ink.inkValue -= djInKCost;

            anim.SetTrigger("Leap");
            jumpValue = 1;

            Vector3 trajectory = transform.up * jumpPower;
            rb.AddForce(trajectory);

        }

    }*/

    [Header("Jump")]
    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    private float jumpTimeCounter;
    public float jumpTime;

    public bool isJumping;
    public int extJump;

    public void JumpEew()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (Input.GetKeyDown(KeyCode.Space) && (extJump > 0 || isGrounded == true))
        {
            if(isGrounded == false)
            {
                if (abilities.mainPath != 2 || abilities.subPath != 1 || abilities.subPathLevel != 3 || extJump == 1)
                {
                    ink.inkValue -= djInKCost;
                }
            }
            rb.velocity = Vector2.up * jumpPower;
            extJump--;
            GameObject jumpSound = Instantiate(jumpSFX, transform.position, transform.rotation);
            Destroy(jumpSound, .5f);
            anim.SetTrigger("Leap");
            anim.SetBool("IsFalling", true);
            

        }

        if (isGrounded == true)
        {
            if(abilities.mainPath == 2 && abilities.subPath == 1)
            {
                
                if(abilities.subPathLevel == 3)
                {
                    extJump = 2;
                }
                else
                {
                    extJump = 1;
                }

            }
            else
            {
                extJump = 0;
            }
                    
            anim.SetBool("IsFalling", false);
            

        }

        else
        {
           
           anim.SetBool("IsFalling", true);
            

        }


    }

    public void DoubleJumpVFX()
    {
        GameObject jumpVisuals = Instantiate(doubleJumpVFX, DJpoint.transform.position, transform.rotation);
        Destroy(jumpVisuals, .5f);
    }



    public bool CanJump; 

    void JumpCheck()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDist, ground); // Ground check
        if (hit)
        {
            CanJump = true;
            jumpValue = 0;
        }

        else
        {
            CanJump = false;
            IsJumping = true;

        }

        if(CanJump == true)
        {
            IsJumping = false;
        }

    }

    public bool rollActive;
    public float rollSpeed;
    public void rollControl()
    {
        rollActive = !rollActive;
    }
    public void Roll()
    {
       
        if (facingRight == false)
        {
            rb.velocity = new Vector2(-rollSpeed, 0);
        }

        else
        {
            rb.velocity = new Vector2(rollSpeed, 0);
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
