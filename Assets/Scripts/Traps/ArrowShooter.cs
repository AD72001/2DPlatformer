using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    // Trap status
    [SerializeField] private float arrowDamage;
    [SerializeField] private float attackCooldown;
    private float attackCooldownDuration;
    [SerializeField] private Transform firePosition;
    [SerializeField] private GameObject[] arrows;

    // Audio
    [SerializeField] private AudioClip arrowSound;

    private void Update()
    {
        if (attackCooldownDuration >= attackCooldown)
        {
            Attack();
        }

        attackCooldownDuration += Time.deltaTime;
    }

    private void Attack()
    {
        attackCooldownDuration = 0;
        SoundManager.instance.PlaySound(arrowSound);

        arrows[LoadArrows()].transform.position = firePosition.position;
        arrows[LoadArrows()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int LoadArrows()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }
}
