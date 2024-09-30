using UnityEngine;

public class KnightRangeEnemy : MonoBehaviour
{
    // Enemy Status
    [SerializeField] private float attackCD;
    private float CDTimer = Mathf.Infinity;

    [SerializeField] private float range;

    // Attack Range
    [SerializeField] private BoxCollider2D enemyCollider;
    [SerializeField] private float colliderDistance;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private Animator animator;

    // Projectiles
    [SerializeField] private Transform firePosition;
    [SerializeField] private GameObject[] fireBalls;
    [SerializeField] private AudioClip fireBallsSound;

    private EnemyPatrol enemyPatrol;

    void Awake()
    {
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CDTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (CDTimer >= attackCD)
            {
                // Attack
                CDTimer = 0;
                animator.SetTrigger("rangeAttack");
                SoundManager.instance.PlaySound(fireBallsSound);
            }
        }
        
        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private void RangeAttack()
    {
        CDTimer = 0;
        // Shoot
        fireBalls[LoadFireballs()].transform.position = firePosition.position;
        fireBalls[LoadFireballs()].GetComponent<EnemyProjectile>().ActivateProjectile();
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

    private bool PlayerInSight()
    {
        RaycastHit2D playerHit = Physics2D.BoxCast(
            enemyCollider.bounds.center + transform.right*range*transform.localScale.x*colliderDistance
            , new Vector3(enemyCollider.bounds.size.x * range, enemyCollider.bounds.size.y, enemyCollider.bounds.size.z)
            , 0, transform.localScale, 0, playerLayer);

        return playerHit.collider != null;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            enemyCollider.bounds.center + transform.right*range*transform.localScale.x*colliderDistance, 
            new Vector3(enemyCollider.bounds.size.x * range, enemyCollider.bounds.size.y, enemyCollider.bounds.size.z));
    }
}
