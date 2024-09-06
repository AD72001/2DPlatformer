using Unity.VisualScripting;
using UnityEngine;

public class Pig : EnemyDamage
{
    // Enemy Status and Component
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float speed;
    [SerializeField] private float detectRange; // detect player range
    [SerializeField] private float hurtRange; // hurt player range
    [SerializeField] private float colliderDistance;
    [SerializeField] private float checkingDelay;
    [SerializeField] private BoxCollider2D enemyCollider;
    //private EnemyPatrol enemyPatrol;

    // Movement
    private float checkingTime;
    private Vector3 destination;
    private bool isAttacking;
    private Vector3[] directions = new Vector3[2];
    private int playerDirection;

    // Attack Timer
    [SerializeField] private float attackDuration;
    private float attackTimer;

    // Animator
    [SerializeField] private Animator animator;

    // Audio
    [SerializeField] private AudioClip rockSound;

    [SerializeField] private HP playerHP;

    void Awake()
    {
        //enemyPatrol = GetComponentInParent<EnemyPatrol>();
        animator = GetComponent<Animator>();
    }
    private void OnEnable() 
    {
        Debug.Log("OnEnable");
        Stop();
    }

    private void Update()
    {
        // During the attack
        if (isAttacking && attackTimer < attackDuration)
        {
            attackTimer += Time.deltaTime;

            // Move
            transform.Translate(destination * Time.deltaTime * speed);

            if (PlayerInRange())
            {
                playerHP.TakeDamage(base.enemyDamage);
                Stop();
            }
        }
        else // Check for player position
        {
            checkingTime += Time.deltaTime;

            if (checkingTime >= checkingDelay)
            {
                CheckForPlayer();
            }

            if (attackTimer >= attackDuration)
            {
                Debug.Log($"Timer: {attackTimer}");
                Stop();
            }
        }

        // if (enemyPatrol != null)
        //     enemyPatrol.enabled = !isAttacking;
    }

    private void CheckForPlayer()
    {
        CheckInAllDirections(); // Get the directions

        for (int i = 0; i < directions.Length; i++)
        {
            RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, directions[i], detectRange, playerLayer);

            // If the player is spotted
            if (hitPlayer.collider != null && !isAttacking)
            {
                isAttacking = true;
                playerDirection = (i == 0) ? -1: 1;

                animator.SetBool("attack", true);
                            
                // Directions
                transform.localScale = new Vector3(
                    Mathf.Abs(transform.localScale.x)*playerDirection, 
                    transform.localScale.y, transform.localScale.z);
                
                destination = directions[i];
                checkingTime = 0;
            }
        }
    }

    private void CheckInAllDirections()
    {
        directions[0] = transform.right * detectRange; // Check Right
        directions[1] = -transform.right * detectRange; // Check Left
    }

    private void Stop()
    {
        // Stop the attack after hit the player or attack duration is exceeded
        destination = transform.position;
        attackTimer = 0;
        isAttacking = false;
        animator.SetBool("attack", false);
    }

    private bool PlayerInRange()
    {
        RaycastHit2D playerHit = Physics2D.BoxCast(
            enemyCollider.bounds.center + transform.right*hurtRange*transform.localScale.x*colliderDistance
            , new Vector3(enemyCollider.bounds.size.x * hurtRange, enemyCollider.bounds.size.y, enemyCollider.bounds.size.z)
            , 0, transform.localScale, 0, playerLayer);

        if (playerHit.collider != null)
        {
            playerHP = playerHit.transform.GetComponent<HP>();
        }

        return playerHit.collider != null;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(enemyCollider.bounds.center + transform.right*hurtRange*transform.localScale.x*colliderDistance, 
            new Vector3(enemyCollider.bounds.size.x * hurtRange, enemyCollider.bounds.size.y, enemyCollider.bounds.size.z));
        Gizmos.DrawWireCube(enemyCollider.bounds.center + transform.right*detectRange*transform.localScale.x*colliderDistance, 
            new Vector3(enemyCollider.bounds.size.x * detectRange, enemyCollider.bounds.size.y, enemyCollider.bounds.size.z));
    }

    private new void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log("collisionEnter2D");
        if (!other.gameObject.CompareTag("Ground"))
            Stop();
    }
}
