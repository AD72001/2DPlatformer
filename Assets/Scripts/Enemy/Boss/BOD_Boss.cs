using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BOD_Boss : EnemyDamage
{
    private GameObject player;

    // Components
    public int phase;
    private Animator animator;
    [SerializeField] private Collider2D cl;

    // Melee Attack
    [SerializeField] private float range;
    [SerializeField] private float height;
    [SerializeField] private float colliderDistance;
    [SerializeField] private LayerMask playerLayer;
    
    // Casting
    [SerializeField] private float castingCD;
    private float castingTimer = Mathf.Infinity;

    // Spell
    [SerializeField] private GameObject[] spells;

    [SerializeField] private float speed;
    public bool isChasing;
    public bool isAttacking;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        phase = 1;
    }

    private void Update() 
    {
        if (phase == 2) 
            isChasing = false;

        
        castingTimer += Time.deltaTime;
        // Melee
        if (PlayerInAttackRange() && !isAttacking)
        {
            isChasing = false;
            isAttacking = true;
            animator.SetTrigger("meleeAttack");
        }

        // Range
        if (!PlayerInAttackRange() && !isAttacking && castingTimer > castingCD)
        {
            castingTimer = 0;
            isChasing = false;
            isAttacking = true;
            animator.SetTrigger("rangeAttack");
        }

        // Chasing
        if (isChasing && !isAttacking)
        {
            Flip();
            animator.SetBool("isChasing", true);
            Chase();
        }
        else {
            animator.SetBool("isChasing", false);
        }
    }

    // Melee Attack Range
    private bool PlayerInAttackRange()
    {
        RaycastHit2D playerHit = Physics2D.BoxCast(
            cl.bounds.center + transform.right*range*transform.localScale.x*colliderDistance
            , new Vector3(cl.bounds.size.x * range, cl.bounds.size.y, cl.bounds.size.z)
            , 0, transform.localScale, 0, playerLayer);

        return playerHit.collider != null;
    }

    private void DamagePlayer()
    {
        if (PlayerInAttackRange())
        {
            player.GetComponent<HP>().TakeDamage(enemyDamage);
        }
    }

    private void FinishAttack()
    {
        isAttacking = false;
    }

    // Spell
    private void RangeAttack()
    {
        GameObject cur_spell = spells[LoadSpells()];
        
        cur_spell.transform.position = new Vector3(
            player.transform.position.x, 
            player.transform.position.y + 2.0f, 
            player.transform.position.z);
        
        cur_spell.SetActive(true);
    }

    private int LoadSpells()
    {
        for (int i = 0; i < spells.Length; i++)
        {
            if (!spells[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, 
            new Vector3(player.transform.position.x, transform.position.y, transform.position.z), 
            speed*Time.deltaTime);
    }

    private void Flip()
    {
        if (transform.position.x < player.transform.position.x)
            transform.rotation = Quaternion.Euler(0,180,0);
        else
            transform.rotation = Quaternion.Euler(0,0,0);
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            cl.bounds.center + transform.right*range*transform.localScale.x*colliderDistance, 
            new Vector3(cl.bounds.size.x * range, cl.bounds.size.y * height, cl.bounds.size.z));
    }
}
