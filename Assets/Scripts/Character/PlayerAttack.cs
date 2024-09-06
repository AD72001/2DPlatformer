using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Attack Status
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    private float attackCooldownDuration = Mathf.Infinity;

    // Components
    [SerializeField] private Transform firePosition;
    [SerializeField] private GameObject[] fireBalls;
    [SerializeField] private AudioClip fireBallsSound;
    [SerializeField] private AudioClip meleeSound;

    [SerializeField] private CapsuleCollider2D playerCollider;
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private LayerMask enemyLayer;
    private Animator animator;

    // Attack Range
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float chargeForceX;
    [SerializeField] private float chargeForceY;
    [SerializeField] private float attackDuration;
    private float attackTimer;
    private bool isAttacking; // melee attack only
    
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // During the attack
        if (isAttacking && attackTimer < attackDuration)
        {
            attackTimer += Time.deltaTime;
            // Move
            playerRigidbody.AddForce(new Vector2(
                playerCollider.transform.localScale.x*Time.deltaTime*chargeForceX, 
                Time.deltaTime*chargeForceY));

            CheckInRange();
        }
        else if (attackTimer >= attackDuration)
        {
            isAttacking = false;

            animator.SetBool("meleeAttack", false);

            attackTimer = 0;
        }

        if (Input.GetKey(KeyCode.E) && attackCooldownDuration >= attackCooldown)
        {
            Attack();
        }
        else if (Input.GetKey(KeyCode.Q) && attackCooldownDuration >= attackCooldown)
        {
            MeleeAttack();
        }

        attackCooldownDuration += Time.deltaTime;
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
        SoundManager.instance.PlaySound(fireBallsSound);
        attackCooldownDuration = 0;

        fireBalls[LoadFireballs()].transform.position = firePosition.position;
        fireBalls[LoadFireballs()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private void MeleeAttack()
    {
        animator.SetBool("meleeAttack", true);
        SoundManager.instance.PlaySound(meleeSound);
        isAttacking = true;
        attackCooldownDuration = 0;
    }
    
    private void CheckInRange()
    {
        RaycastHit2D enemyHit = Physics2D.BoxCast(
            playerCollider.bounds.center + transform.right*range*transform.localScale.x*colliderDistance
            , new Vector3(playerCollider.bounds.size.x * range, playerCollider.bounds.size.y, playerCollider.bounds.size.z)
            , 0, transform.localScale, 0, enemyLayer);

        if (enemyHit.collider != null)
        {
            enemyHit.transform.GetComponent<HP>().TakeDamage(damage);
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

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(playerCollider.bounds.center + transform.right*range*transform.localScale.x*colliderDistance, 
            new Vector3(playerCollider.bounds.size.x * range, playerCollider.bounds.size.y, playerCollider.bounds.size.z));
    }
}
