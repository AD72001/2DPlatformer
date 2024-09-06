using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Components
    private Rigidbody2D playerBody;
    private Animator animator; 
    private CapsuleCollider2D playerCollider2D;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask objectLayer;

    // Player Status
    [SerializeField] private float speed; // Player speed
    [SerializeField] private float jumpForce; // Player jump force
    [SerializeField] private HP playerHP;

    // Coyote Jump
    [SerializeField] private float coyoteTime; // Time for Jump after leaving an edge
    private float coyoteTimer;

    // Multi Jump
    [SerializeField] private int extraJumpNumber;
    private int JumpCounter;

    private float horizontalDirection;

    // Audio
    [SerializeField] private AudioClip jumpSound;

    void Awake()
    {
        // References physics' components
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCollider2D = GetComponent<CapsuleCollider2D>();
        playerHP = GetComponent<HP>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalDirection = Input.GetAxis("Horizontal"); // Input Left, Right

        // Flipping sprite
        if (horizontalDirection > 0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontalDirection < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Set animation Idle -> Walking
        animator.SetBool("walking", horizontalDirection != 0);
        animator.SetBool("onGround", OnGround());

        // Jump if Space is pressed once
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetKeyUp(KeyCode.Space) && playerBody.velocity.y > 0)
            playerBody.velocity = new Vector2(playerBody.velocity.x, playerBody.velocity.y / 2);

        playerBody.gravityScale = 5;
        playerBody.velocity = new Vector2(horizontalDirection*speed, playerBody.velocity.y);

        if (OnGround())
            coyoteTimer = coyoteTime;
        else
            coyoteTimer -= Time.deltaTime;

        // dead from gravity?
        if (playerBody.transform.position.y < -5.0f)
        {
            GetComponent<PlayerMovement>().enabled = false;
            playerBody.gravityScale = 0;
            playerHP.PlayerDead();
        } 
    }

    private void Jump()
    {
        if (coyoteTimer < 0 && JumpCounter <= 0) return;

        SoundManager.instance.PlaySound(jumpSound);

        if (OnGround())
        {
            JumpCounter = extraJumpNumber;
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);
        }
        else
        {
            if (coyoteTimer > 0)
                playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);
            else
            {
                if (JumpCounter > 0)
                {
                    playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);
                    JumpCounter--;
                }
            }
        }

        coyoteTimer = 0;
    }

    // BoxCast to check if the player is colliding with the ground or an object.
    private bool OnGround()
    {
        RaycastHit2D rayCastHit2D_ground = Physics2D.BoxCast(playerCollider2D.bounds.center, 
            playerCollider2D.bounds.size, 0, Vector2.down, 0.01f, groundLayer);

        RaycastHit2D rayCastHit2D_object = Physics2D.BoxCast(playerCollider2D.bounds.center, 
            playerCollider2D.bounds.size, 0, Vector2.down, 0.01f, objectLayer);

        return rayCastHit2D_ground.collider != null || rayCastHit2D_object.collider != null;
    }
}
