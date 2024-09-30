using UnityEngine;

public class HP_Potion : MonoBehaviour
{
    // Apples restore HP to player
    [SerializeField] private float HPRestoreValue;
    [SerializeField] private AudioClip collectSound;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<HP>().AddHP(HPRestoreValue);
            SoundManager.instance.PlaySound(collectSound);
            animator.SetTrigger("collected");
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
