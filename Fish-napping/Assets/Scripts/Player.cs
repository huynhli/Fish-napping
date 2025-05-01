using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Player : MonoBehaviour
{
    // private SpriteRenderer spriteRenderer;
    private Rigidbody2D playerRigidBody;
    private float healthPoints;

    public int maxJumps;
    private int jumpsRemaining;


    private float horizontalMovement;
    private bool facingRight;

    public Transform groundCheck;
    public LayerMask groundLayer;

    public Vector2 groundCheckSize = new Vector2(0.5f, 0.5f);


    // private int spriteIndex;
    // public Sprite[] idleSprites;
    public Animator animator;

    private void Awake() {
        // spriteRenderer = GetComponent<SpriteRenderer>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthPoints = 100f;
        facingRight = true;
        horizontalMovement = 0f;
        maxJumps = 2;
    }

    public void Move(InputAction.CallbackContext context) {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    // Start is called before the first frame update
    // void Start()
    // {
    //     InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    // }

    // private void AnimateSprite() {
    //     spriteIndex++;
    //     if (spriteIndex >= idleSprites.Length) {
    //         spriteIndex = 0;
    //     }
    //     spriteRenderer.sprite = idleSprites[spriteIndex];
    // }

    public void Jump(InputAction.CallbackContext context) {
        if (jumpsRemaining > 0) {
            if (context.performed) {
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 30f);
                animator.SetTrigger("Jump");
                jumpsRemaining--;
            } 
            // else if (context.canceled) {
            //     playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 20f);
            //     animator.SetTrigger("Jump");
            //     jumpsRemaining--;
            // }
        }
    }

    // Update is called once per frame
    private void Update() {
        playerRigidBody.velocity = new Vector2(horizontalMovement * 5f, playerRigidBody.velocity.y);
        GroundChecker();
        animator.SetFloat("yVelocity", playerRigidBody.velocity.y);
        animator.SetFloat("xVelocity", playerRigidBody.velocity.magnitude);
    }
    private void FixedUpdate()
    {
        if (horizontalMovement < 0 && facingRight) {
            transform.localScale = new Vector3(-2f, 2f, 1f);
            facingRight = false;
        }
        if (horizontalMovement > 0 && !facingRight) {
            transform.localScale = new Vector3(2f, 2f, 1f);
            facingRight = true;
        }
        // if collision, update health points --> sprite change automatic, so just handle logic for death and respawn if occursunity    
    }

private void GroundChecker() {
    if (Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer)) {
        jumpsRemaining = maxJumps;
    }
}

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }
    // private void OnCollisionEnter2D(Collision2D collision) {
    //     // Only set grounded when touching the ground
    //     if (collision.collider.CompareTag("Ground")) {
    //         isGrounded = true;
    //         animator.SetBool("isJumping", false);  // Stop jump animation on landing
    //     }
    // }
}
