using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // private SpriteRenderer spriteRenderer;
    [Header("Player")]
    private Rigidbody2D playerRigidBody;
    private float baseHealth;
    public float healthMultiplier;
    public Animator animator;

    [Header("Movement")]
    public int maxJumps;
    private int jumpsRemaining;
    public float moveSpeedMultiplier;
    private float horizontalMovement;
    private bool facingRight;

    [Header("GroundCheck")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.5f);

    [Header("Interactions")]
    private bool atDoor;

    // private int spriteIndex;
    // public Sprite[] idleSprites;

    private void Awake() {
        // spriteRenderer = GetComponent<SpriteRenderer>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        baseHealth = 100f;
        facingRight = true;
        horizontalMovement = 0f;
        maxJumps = 2;
        atDoor = false;
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
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 15f);
                animator.SetTrigger("Jump");
                jumpsRemaining--;
            } 
            else if (context.canceled) {
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 8f);
                animator.SetTrigger("Jump");
                jumpsRemaining--;
            }
        }
    }

    public void Interactions(InputAction.CallbackContext context) {
        if (atDoor) {
            if (context.performed) {
                // change to new scene.
                FindObjectOfType<GameManager>().EnterShop();
            }
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

    
    private void OnTriggerEnter2D(Collider2D collider) {
        // Only set grounded when touching the ground
        if (collider.gameObject.tag == "Door") {
            atDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        // Only set grounded when touching the ground
        if (collider.gameObject.tag == "Door") {
            atDoor = false;
        }
    }
}
