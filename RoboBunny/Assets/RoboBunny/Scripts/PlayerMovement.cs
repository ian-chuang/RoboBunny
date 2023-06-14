using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// https://www.youtube.com/watch?v=FXpUb-H54Oc
// https://www.youtube.com/watch?v=O6VX6Ro7EtA

public class PlayerController : MonoBehaviour
{
    #region Parameters
    [Header("For Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] private ParticleSystem dust;
    private float XDirectionalInput;
    private bool isMoving;
    private int facingDirection = 1; // 1 is right, -1 is left

    [Header("For Jumping")]
    [SerializeField] float jumpForce = 16f;
    [SerializeField] int extraJumpsValue = 1;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] float jumpCoyoteTime = 0.15f;
    [SerializeField] float jumpBufferTime = 0.10f;
    [SerializeField] float jumpCutMultiplier = 0.5f;
    [SerializeField] float fallGravityMultiplier = 1.2f;
    [SerializeField] float gravityScale = 1.0f;
    [SerializeField] float maxFallVelocity = -40.0f;
    private bool grounded;
    private bool jumpInput;
    private bool jumpInputUp;
    private int extraJumps;
    private float lastGroundedTime;
    private float lastJumpTime;

    [Header("For PogoJumping")]
    [SerializeField] float pogoForce = 18f;
    [SerializeField] private Vector2 pogoCheckSize;
    [SerializeField] private LayerMask pogoAble;
    [SerializeField] private float pogoTime = 0.2f;
    private bool pogoActive; 
    private bool pogoInput;
    

    [Header("For WallSliding")]
    [SerializeField] float wallSlideSpeed;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] Vector2 wallCheckSize;
    private bool isTouchingWall;
    private bool isWallSliding;

    [Header("For WallJumping")]
    [SerializeField] Vector2 wallJumpingPower = new Vector2(8f, 16f);
    private bool isWallJumping;
    [SerializeField] float wallJumpingDuration = 0.3f;

    [Header("For Dashing")]
    [SerializeField] TrailRenderer tr;
    [SerializeField] float dashingPower = 24f;
    [SerializeField] float dashingTime = 0.2f;
    [SerializeField] float dashingCooldown = 1f;
    private bool canDash = true;
    private bool finishDashCooldown = true;
    private bool isDashing;
    private bool dashInput;

    [Header("For Hit")]
    [SerializeField] Vector2 hitKnockbackPower = new Vector2(3f, 3f);
    [SerializeField] float hitDuration = 0.15f;
    [SerializeField] float hitCooldown = 0.5f;
    private bool finishHitCooldown = true;
    private bool isHit;
    private float hitDirection;

    [Header("For Death")]
    [SerializeField] float deathDuration = 5f;
    private bool isDead = false;

    [Header("For Health")]
    [SerializeField] int maxHealth = 3;
    private int currentHealth;
    [SerializeField] GameObject firstHeart;
    [SerializeField] GameObject secondHeart;
    [SerializeField] GameObject thirdHeart;

    [Header("Other")]
    [SerializeField] Animator anim;
    private CameraController cameraController; 
    private Rigidbody2D rb;
    #endregion
    
    
    private void Start()
    {
        // store components
        rb = GetComponent<Rigidbody2D>();
        cameraController = Camera.main.GetComponent<CameraController>();

        // set health
        currentHealth = maxHealth;

        // For displaying UI
        firstHeart.SetActive(true);
        secondHeart.SetActive(true);
        thirdHeart.SetActive(true);
    }

    private void Update()
    {
        // set animation variables
        AnimationControl();
        // collect and store input
        GatherInput(); 
        
        if (isDashing || isHit)
        {
            return;
        }

        // check if player is grounded or touching wall,
        // adjust motion given these properties
        CheckWorld(); 

        if (isWallJumping)
        {
            return;
        }

        // run movement logic
        Movement();
    }

    void GatherInput()
    {
        // store input
        XDirectionalInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");
        jumpInputUp = Input.GetButtonUp("Jump");
        dashInput = Input.GetButtonDown("Fire3");
        pogoInput = Input.GetButtonDown("Fire1");
    }
    void CheckWorld()
    {
        // check if player touching ground
        //https://www.youtube.com/watch?v=FXpUb-H54Oc
        grounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer);
        // check if player is facing and touching wall
        //https://www.youtube.com/watch?v=FXpUb-H54Oc
        isTouchingWall = Physics2D.OverlapBox(wallCheckPoint.position, wallCheckSize, 0, wallLayer);
        
        if (grounded)
        {
            // touching ground

            // reset coyote time
            // https://www.youtube.com/watch?v=RFix_Kg2Di0
            lastGroundedTime = jumpCoyoteTime;

            // reset jump counter
            // https://www.youtube.com/watch?v=RFix_Kg2Di0
            extraJumps = extraJumpsValue;

            // enable dashing
            canDash = true;
            
            // disable wall sliding
            isWallSliding = false;
        }
        else if (isTouchingWall)
        {
            // touching wall

            // reset jump counter
            extraJumps = extraJumpsValue;

            // enable dashing
            canDash = true;
            
            // stop the wall jump routine
            StopCoroutine(WallJumpRoutine());
            // disable wall jumping
            isWallJumping = false;
            
            // set wall sliding flag
            isWallSliding = true;

            // allow player to cling to wall and slow descent
            // https://gist.github.com/bendux/b6d7745ad66b3d48ef197a9d261dc8f6
            if (rb.velocity.y < 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
            }
        }
        else 
        {
            // in the air

            // disable wall sliding
            isWallSliding = false;

            // decrement coyote time counter
            lastGroundedTime -= Time.deltaTime;

            // air movement logic
            HandleAirMovement();
        }
    }

    void Movement()
    {
        // What actually moves the player
        // https://gist.github.com/bendux/b6d7745ad66b3d48ef197a9d261dc8f6#file-playermovement-cs-L53
        rb.velocity = new Vector2(XDirectionalInput * moveSpeed, rb.velocity.y);
        
        // Update direction player is facing
        if (XDirectionalInput < 0 && facingDirection == 1)
        {
            Flip();
        }
        else if (XDirectionalInput > 0 && facingDirection == -1)
        {
            Flip();
        }
        
        // Update animation if player is moving
        isMoving = XDirectionalInput != 0;

        // Dashing
        if (dashInput && canDash && finishDashCooldown)
        {
            FindObjectOfType<SoundEffects>().PlayDash();
            StartCoroutine(DashRoutine());
        }
        
        // Jump Buffer logic
        if (jumpInput)
        {
            // reset jump buffer time
            lastJumpTime = jumpBufferTime;
        }
        else
        {
            // decrement jump buffer counter
            lastJumpTime -= Time.deltaTime;
        }

        // try jumping
        TryJump();
    }

    void Flip()
    {
        // flip direction state
        facingDirection *= -1;

        // rotate player
        transform.Rotate(0, 180, 0);

        // play dust visual if grounded
        if (grounded)
        {   
            dust.Play();
        }
    }

    void TryJump()
    {
        if (isWallSliding && jumpInput)
        {
            // play jump sound
            FindObjectOfType<SoundEffects>().PlayJump();

            // Perform wall jump
            // https://gist.github.com/bendux/b6d7745ad66b3d48ef197a9d261dc8f6
            StartCoroutine(WallJumpRoutine());
        }
        else if (lastJumpTime > 0 && lastGroundedTime > 0)
        {
            // Perform regular jump off of ground

            // set upward velocity
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            
            // flush jump buffer
            lastJumpTime = 0;

            // play jump sound
            FindObjectOfType<SoundEffects>().PlayJump();

            // play jump animation
            anim.SetTrigger("Jump");

            // show dust visual
            dust.Play();
        }
        else if (extraJumps > 0 && !grounded && jumpInput)
        {
            // Double jump
            //https://www.youtube.com/watch?v=QGDeafTx5ug

            // set upward velocity
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            // decrement number of extra jumps
            extraJumps--;

            // play jump sound
            FindObjectOfType<SoundEffects>().PlayJump();

            // play double jump animation
            anim.SetTrigger("DoubleJump");

            // show trail visual
            tr.emitting = true;
        }
    }

    private IEnumerator WallJumpRoutine()
    {
        // set player velocity up and away from wall
        rb.velocity = new Vector2(facingDirection * -1 * wallJumpingPower.x, wallJumpingPower.y);
        // disable wall sliding
        isWallSliding = false;
        // play jump sound
        FindObjectOfType<SoundEffects>().PlayJump();
        // play jump animation
        anim.SetTrigger("Jump");
        // set wall jump flag
        isWallJumping = true;
        // flip direction of player
        Flip();
        // wait for wall jump duration
        yield return new WaitForSeconds(wallJumpingDuration);
        // disable wall jump
        isWallJumping = false;
    }

    void HandleAirMovement()
    {
        // pogo
        if (pogoInput)
        {
            // pogo was pressed

            // trigger jump sound
            FindObjectOfType<SoundEffects>().PlayJump();

            // trigger animation
            anim.SetTrigger("DoubleJump");

            // start pogo routine
            StartCoroutine(PogoRoutine());
        }

        if (pogoActive && Physics2D.OverlapBox(groundCheckPoint.position, pogoCheckSize, 0, pogoAble))
        {
            // can pogo

            // reset pogo state
            pogoActive = false;

            // pogo
            PogoJump();
        }

        // jump cut
        // https://gist.github.com/bendux/b6d7745ad66b3d48ef197a9d261dc8f6
        if (jumpInputUp && rb.velocity.y > 0f)
        {
            // player has let go of jump input and player is moving upwards

            // cut upward velocity by factor
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutMultiplier);

            // player isn't grounded
            lastGroundedTime = 0f;
        }

        // fall gravity multiplier
        // https://www.youtube.com/watch?v=2S3g8CgBG1g
        if (rb.velocity.y < 0f)
        {
            // player is falling

            // increase gravity scale
            rb.gravityScale = gravityScale * fallGravityMultiplier;
            // stop showing trail renderer
            tr.emitting = false;
        }
        else
        {
            // player isn't falling

            // reset gravity scale back to normal
            rb.gravityScale = gravityScale;
        }

        // cap velocity so that it won't go below max fall velocity
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, maxFallVelocity)); 
    }

    private IEnumerator PogoRoutine()
    {
        // pogo is active
        pogoActive = true;
        // wait for pogo duration
        yield return new WaitForSeconds(pogoTime);
        // deactivate pogo
        pogoActive = false;
    }

    void PogoJump()
    {
        // show trail
        tr.emitting = true;
        // camera controller effect
        cameraController.FreezeScreen();
        // set upward velocity
        rb.velocity = new Vector2(rb.velocity.x, pogoForce);
        // reset # of extra jumps
        extraJumps = extraJumpsValue;
        // reallow dash 
        canDash = true;
    }

    //https://gist.github.com/bendux/aa8f588b5123d75f07ca8e69388f40d9
    private IEnumerator DashRoutine()
    {
        // disable dashing again
        canDash = false;
        // flag dash cooldown false
        finishDashCooldown = false;
        // is dashing now
        isDashing = true;
        // cache original gravity
        float originalGravity = rb.gravityScale;
        // disable gravity
        rb.gravityScale = 0f;
        // set velocity in dash direction
        rb.velocity = new Vector2(facingDirection * dashingPower, 0f);
        // enable trail
        tr.emitting = true;
        // wait for dashing time
        yield return new WaitForSeconds(dashingTime);
        // disable trail
        tr.emitting = false;
        // go back to original gravity
        rb.gravityScale = originalGravity;
        // not dashing anymore
        isDashing = false;
        // wait for dash cooldown
        yield return new WaitForSeconds(dashingCooldown);
        // flag dash cooldown true
        finishDashCooldown = true;
    }

    void Hit()
    {
        if (!finishHitCooldown || isDead)
        {
            return;
        }
        
        // do camera effects when hit
        cameraController.FreezeScreen();
        cameraController.ShakeScreen();

        // hit sound
        FindObjectOfType<SoundEffects>().PlayHit();

        // decrement health
        currentHealth--;

        // check if either just hit or died
        if (currentHealth <= 0)
        {
            // died

            // remove last heart
            firstHeart.SetActive(false);

            // disable script
            enabled = false;
            // flag is Dead
            isDead = true;
            // start death routine
            StartCoroutine(DeathRoutine());
        }
        else
        {
            // remove life for each hit
            if (currentHealth == 2)
            {
                thirdHeart.SetActive(false);
            }
            else if (currentHealth == 1)
            {
                secondHeart.SetActive(false);
            }
            // start hit routine
            StartCoroutine(HitRoutine());
        }
    }

    private IEnumerator HitRoutine()
    {
        // flag isHit
        isHit = true;
        // set animation state that is hit
        anim.SetBool("Hit", isHit);
        // flag hit cooldown
        finishHitCooldown = false;
        // set knockback velocity
        rb.velocity = new Vector2(hitDirection * hitKnockbackPower.x, hitKnockbackPower.y);
        // wait for hit duration
        yield return new WaitForSeconds(hitDuration);
        // flag is Hit false
        isHit = false;
        // wait for hit cooldown to finish
        yield return new WaitForSeconds(hitCooldown);
        // finished hit cooldown
        finishHitCooldown = true;
    }

    private IEnumerator DeathRoutine()
    {
        // rigid body is now static, not dynamic
        rb.bodyType = RigidbodyType2D.Static;
        // reset all animation state
        anim.SetBool("Moving", false);
        anim.SetBool("WallSlide", false);
        anim.SetFloat("AirSpeedY", 0);
        anim.SetBool("Grounded", true);
        anim.SetBool("Dashing", false);
        anim.SetBool("Hit", false);
        // trigger death sound effect
        FindObjectOfType<SoundEffects>().PlayFalling();
        // trigger death animation
        anim.SetTrigger("Death");
        // wait for death duration
        yield return new WaitForSeconds(deathDuration);
        // reset scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            // collided with Trap

            // set hit direction,
            // dependent on collision transform relative to player transform
            if (transform.position.x < collision.gameObject.transform.position.x)
            {
                hitDirection = -1;
            }
            else
            {
                hitDirection = 1;
            }

            // call Hit()
            Hit();
        }
        else if (collision.gameObject.CompareTag("Death"))
        {
            // died since hit Death/OutOfBounds Tilemap

            // disable script
            enabled = false;
            // flag is Dead
            isDead = true;
            // start death routine
            StartCoroutine(DeathRoutine());
        }
    }

    void AnimationControl()
    {
        // if dead don't do animation
        if (isDead) return;

        // set animation based on state
        anim.SetBool("Moving", isMoving);
        anim.SetBool("WallSlide", isWallSliding);
        anim.SetFloat("AirSpeedY", rb.velocity.y);
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Dashing", isDashing);
        anim.SetBool("Hit", isHit);
    }
    private void OnDrawGizmosSelected()
    {
        // draw gizmos
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(groundCheckPoint.position, pogoCheckSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(groundCheckPoint.position, groundCheckSize);
        Gizmos.color = Color.green;
        Gizmos.DrawCube(wallCheckPoint.position, wallCheckSize);
    }

}
