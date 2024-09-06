using UnityEngine;

public class EnemyDamage: MonoBehaviour
{
    [SerializeField] protected float enemyDamage;

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<HP>().TakeDamage(enemyDamage);
        }
    }

    protected void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<HP>().TakeDamage(enemyDamage);
        }
    }
}
