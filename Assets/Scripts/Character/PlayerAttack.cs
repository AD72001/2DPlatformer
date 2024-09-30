using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Attack Status
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    [SerializeField] public float projectileTotal;
    private float attackCooldownDuration = Mathf.Infinity;

    // Components
    [SerializeField] private Transform firePosition;
    [SerializeField] private GameObject[] fireBalls;
    [SerializeField] private AudioClip fireBallsSound;

    [SerializeField] private CapsuleCollider2D playerCollider;
    [SerializeField] private Rigidbody2D playerRigidbody;

    // Layers
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask objectLayer;
    private Animator animator;

    // Dash Range
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float chargeForceX;
    [SerializeField] private float chargeForceY;
    [SerializeField] private float attackDuration;
    [SerializeField] private AudioClip dashSound;
    private float attackTimer;
    private bool isAttacking; // melee attack only

    private float origravity;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        origravity = playerRigidbody.gravityScale;
    }

    void Update()
    {
        // During the melee attack
        if (isAttacking && attackTimer < attackDuration)
        {
            attackTimer += Time.deltaTime;

            animator.SetBool("walking", false);
            // Move
            playerRigidbody.AddForce(new Vector2(
                playerCollider.transform.localScale.x*Time.deltaTime*chargeForceX, 
                Time.deltaTime*chargeForceY));

            // CheckInRange();
        }
        else if (attackTimer >= attackDuration)
        {
            isAttacking = false;

            playerRigidbody.gravityScale = origravity;

            animator.SetBool("meleeAttack", false);
            Physics2D.IgnoreLayerCollision(8, 9, false);

            attackTimer = 0;
        }

        // Range attack
        if (Input.GetKey(KeyCode.E) 
            && attackCooldownDuration >= attackCooldown
            && projectileTotal > 0)
        {
            RangeAttack();
        } 
        // Melee attack
        else if (Input.GetKey(KeyCode.Q) 
            && attackCooldownDuration >= attackCooldown
            && GetComponent<PlayerMovement>().OnGround())
        {
            MeleeAttack();
        }

        attackCooldownDuration += Time.deltaTime;
    }

    private void RangeAttack()
    {
        animator.SetTrigger("attack");
        SoundManager.instance.PlaySound(fireBallsSound);
        attackCooldownDuration = 0;
        projectileTotal--;

        fireBalls[LoadFireballs()].transform.position = firePosition.position;
        fireBalls[LoadFireballs()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private void MeleeAttack()
    {
        animator.SetBool("meleeAttack", true);
        SoundManager.instance.PlaySound(dashSound);
        isAttacking = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        playerRigidbody.gravityScale = 0;
        attackCooldownDuration = 0;
    }
    
    private void CheckInRange()
    {
        RaycastHit2D enemyHit = Physics2D.BoxCast(
            playerCollider.bounds.center + transform.right*range*transform.localScale.x*colliderDistance
            , new Vector3(playerCollider.bounds.size.x * range, playerCollider.bounds.size.y, playerCollider.bounds.size.z)
            , 0, transform.localScale, 0, enemyLayer);

        RaycastHit2D objectHit = Physics2D.BoxCast(
            playerCollider.bounds.center + transform.right*range*transform.localScale.x*colliderDistance
            , new Vector3(playerCollider.bounds.size.x * range, playerCollider.bounds.size.y, playerCollider.bounds.size.z)
            , 0, transform.localScale, 0, objectLayer);

        if (enemyHit.collider != null)
        {
            enemyHit.transform.GetComponent<HP>().TakeDamage(damage);
        }

        if (objectHit.collider != null)
        {
            objectHit.transform.GetComponent<HP>().TakeDamage(damage);
        }
    }

    private int LoadFireballs()
    {
        for (int i = 0; i < fireBalls.Length; i++)
        {
            if (!fireBalls[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }

    public void AddProjectile(float value)
    {
        projectileTotal += value;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(playerCollider.bounds.center + transform.right*range*transform.localScale.x*colliderDistance, 
            new Vector3(playerCollider.bounds.size.x * range, playerCollider.bounds.size.y, playerCollider.bounds.size.z));
    }
}
