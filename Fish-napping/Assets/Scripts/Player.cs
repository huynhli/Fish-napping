using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D playerRigidBody;
    private float healthPoints;
    public bool isGrounded;
    private bool facingRight;
    // private int spriteIndex;
    // public Sprite[] idleSprites;
    public Animator animator;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthPoints = 100f;
        isGrounded = false;
        facingRight = true;
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

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A)) {
            playerRigidBody.velocity = new Vector2(-5, playerRigidBody.velocity.y);
            if (facingRight) {
                transform.localScale = new Vector3(-2f, 2f, 1f);
                facingRight = false;
            }
        }
        if (Input.GetKey(KeyCode.D)) {
            playerRigidBody.velocity = new Vector2(5, playerRigidBody.velocity.y);
            if (!facingRight) {
                transform.localScale = new Vector3(2f, 2f, 1f);
                facingRight = true;
            }
        }
        if (Input.GetKey(KeyCode.W) && isGrounded) {
            playerRigidBody.AddForce(Vector2.up * 20f, ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
        }
        // if collision, update health points --> sprite change automatic, so just handle logic for death and respawn if occursunity    
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        isGrounded = true;
    }
}
