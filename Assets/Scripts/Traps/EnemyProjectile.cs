using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    private BoxCollider2D projBoxCollider;
    [SerializeField] private float resetTime;
    private float lifeTime;
    private Animator animator;
    private bool hit;

    void Awake() 
    {
        hit = false;
        lifeTime = 0;
        projBoxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (hit) return;

        float projMovementSpeed = speed * Time.deltaTime;

        transform.Translate(new Vector3(projMovementSpeed, 0, 0)); 

        lifeTime += Time.deltaTime;
        if (lifeTime >= resetTime) 
            gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collider) 
    {
        if (!collider.gameObject.CompareTag("Enemy"))
        {
            hit = true;
            base.OnCollisionEnter2D(collider);
            projBoxCollider.enabled = false;

            if (animator != null)
                animator.SetTrigger("explode");
            else
                gameObject.SetActive(false);
        }
    }
    public void ActivateProjectile()
    {
        hit = false;
        lifeTime = 0;

        gameObject.SetActive(true);
        projBoxCollider.enabled = true;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
