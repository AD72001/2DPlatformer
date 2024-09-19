using UnityEngine;

public class Banana : MonoBehaviour
{
    // Banana restore projectiles to player
    [SerializeField] private float ProjectileRestoreValue;
    [SerializeField] private AudioClip collectSound;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<PlayerAttack>().AddProjectile(ProjectileRestoreValue);
            SoundManager.instance.PlaySound(collectSound);
            animator.SetTrigger("collected");
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
