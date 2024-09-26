using UnityEngine;

public class EnemyDetectControl : MonoBehaviour
{
    [SerializeField] private FlyingEnemy[] enemies;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            foreach (FlyingEnemy enemy in enemies)
            {
                enemy.isChasing = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            foreach (FlyingEnemy enemy in enemies)
                enemy.isChasing = false;
        }
    }
}
