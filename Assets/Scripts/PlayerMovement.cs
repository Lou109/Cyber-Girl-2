using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] BoxCollider2D deathCollider;

    Vector2 death = Vector2.zero;
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;
    CyberGirl2 controls;
    Shooter shooter;
    PlayerHealth playerHealth;

    bool isAlive = true;
    bool moving;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
        controls = new CyberGirl2();
        deathCollider.GetComponentInChildren<BoxCollider2D>();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        playerHealth = GetComponent<PlayerHealth>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    void Update()
    {

        if (!isAlive) { return; }

        Run();
        FlipSprite();
        ClimbLadder();
        Die();
        OnFire();
    }

    void OnFire()
    {
        if (!isAlive) { return; }

        if (shooter != null)
        {
            bool isShooting = Mathf.RoundToInt(controls.Player.Fire.ReadValue<float>()) > 0;
            myAnimator.SetLayerWeight(1, isShooting ? 1 : 0);
            shooter.isFiring = isShooting;
        }

    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();


    }
    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            moving = true;
            this.transform.parent = other.transform;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && !other.gameObject.CompareTag("MovingPlatform"))
        {
            moving = false;
            this.transform.parent = null;
        }
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (value.isPressed)
        {
            //do stuff
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
            this.transform.parent = null;
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    void Die()
    {
        int healthAmount = playerHealth.GetHealth();

        if (healthAmount <= 0 || myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy Bullets")))
        {
            DieResult();
        }
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Shock")))

        {
            DieByShock();
        }
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards", "Enemies")))
        {
            DieResult();
        }
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("CrushingHazards"))
            && myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            DieResult();
        }

        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Prevent Flip")))
        {
            OtherDieResult();
        }

        void DieResult()
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            shooter.isFiring = false;
            myRigidbody.velocity = death;
            deathCollider.enabled = true;
            myFeetCollider.enabled = false;
            myBodyCollider.enabled = false;
            Invoke("DeathDelay", 2.0f);
        }

        void OtherDieResult()
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            shooter.isFiring = false;
            myRigidbody.velocity = death;
            deathCollider.enabled = true;
            myFeetCollider.enabled = false;
            Invoke("DeathDelay", 2.0f);
        }

        void DieByShock()
        {
            isAlive = false;
            myAnimator.SetTrigger("Shock");
            shooter.isFiring = false;
            myRigidbody.velocity = death;
            deathCollider.enabled = true;
            myFeetCollider.enabled = false;
            Invoke("DeathDelay", 2.0f);

        }
    }
        void DeathDelay()

        {
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
