using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMeleeEnemy : MonoBehaviour
{
    // Enemy Status
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float attackCD;
    [SerializeField] private float range;

    // Component
    [SerializeField] private BoxCollider2D enemyCollider;
    [SerializeField] private float colliderDistance;
    [SerializeField] private LayerMask playerLayer;
    private float CDTimer = Mathf.Infinity;

    [SerializeField] private Animator animator;
    [SerializeField] private HP playerHP;

    // Audio
    [SerializeField] private AudioClip meleeSound;
    private EnemyPatrol enemyPatrol;

    void Awake()
    {
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CDTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (CDTimer >= attackCD)
            {
                // Attack
                CDTimer = 0;
                animator.SetTrigger("meleeAttack");
                SoundManager.instance.PlaySound(meleeSound);
            }
        }
        
        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D playerHit = Physics2D.BoxCast(
            enemyCollider.bounds.center + transform.right*range*transform.localScale.x*colliderDistance
            , new Vector3(enemyCollider.bounds.size.x * range, enemyCollider.bounds.size.y, enemyCollider.bounds.size.z)
            , 0, transform.localScale, 0, playerLayer);

        if (playerHit.collider != null)
        {
            playerHP = playerHit.transform.GetComponent<HP>();
        }

        return playerHit.collider != null;
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHP.TakeDamage(damage);
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(enemyCollider.bounds.center + transform.right*range*transform.localScale.x*colliderDistance, 
            new Vector3(enemyCollider.bounds.size.x * range, enemyCollider.bounds.size.y, enemyCollider.bounds.size.z));
    }
}
