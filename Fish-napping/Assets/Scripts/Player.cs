using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D playerRigidBody;
    private int spriteIndex;
    public Sprite[] idleSprites;


    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRigidBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void AnimateSprite() {
        spriteIndex++;
        if (spriteIndex >= idleSprites.Length) {
            spriteIndex = 0;
        }
        spriteRenderer.sprite = idleSprites[spriteIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)) {
            playerRigidBody.velocity = new Vector2(-5, playerRigidBody.velocity.y);
            transform.localScale = new Vector3(-2f, 2f, 1f);
        }
        if (Input.GetKey(KeyCode.D)) {
            playerRigidBody.velocity = new Vector2(5, playerRigidBody.velocity.y);
            transform.localScale = new Vector3(2f, 2f, 1f);

        }
        if (Input.GetKey(KeyCode.W) && playerRigidBody.velocity.y == 0) {
            playerRigidBody.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
        }
        
    }
}
