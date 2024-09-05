using UnityEngine;

public class EnemyDamage: MonoBehaviour
{
    [SerializeField] protected float enemyDamage;

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Hit");
            collider.GetComponent<HP>().TakeDamage(enemyDamage);
        }
    }

    protected void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit");
            collider.gameObject.GetComponent<HP>().TakeDamage(enemyDamage);
        }
    }
}
