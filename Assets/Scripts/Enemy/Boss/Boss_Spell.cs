using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Spell : MonoBehaviour
{
    // Compont
    private Animator animator;

    //Audio
    [SerializeField] private AudioClip spellSound;

    // Spell Range 
    [SerializeField] private Collider2D cl;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float range;
    [SerializeField] private float damage;

    // Idle Time before attack
    [SerializeField] private float waitTime;
    private float waitTimer;

    // Player Component
    [SerializeField] private LayerMask playerLayer;
    private GameObject player;

    private void Awake() {
        waitTimer = 0.0f;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        waitTimer += Time.deltaTime;

        if (waitTimer > waitTime)
        {
            animator.SetBool("attack", true);
            waitTimer = 0.0f;
        }
    }

    private void DamagePlayer()
    {
        if (PlayerInAttackRange())
        {
            player.GetComponent<HP>().TakeDamage(damage);
        }

        SoundManager.instance.PlaySound(spellSound);

        animator.SetBool("attack", false);
    }

    private bool PlayerInAttackRange() 
    {
        RaycastHit2D playerHit = Physics2D.BoxCast(
            cl.bounds.center + transform.up*range*transform.localScale.x*colliderDistance
            , new Vector3(cl.bounds.size.x, cl.bounds.size.y*range, cl.bounds.size.z)
            , 0, transform.localScale, 0, playerLayer);

        return playerHit.collider != null;
    }
    
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            cl.bounds.center + transform.up*range*transform.localScale.x*colliderDistance, 
            new Vector3(cl.bounds.size.x, cl.bounds.size.y*range, cl.bounds.size.z));
    }

    private void DeActivate()
    {
        gameObject.SetActive(false);
    }
}
