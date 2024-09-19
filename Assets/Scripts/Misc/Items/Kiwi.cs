using UnityEngine;

public class Kiwi : MonoBehaviour
{
    // Kiwi restore lives to player
    [SerializeField] private float LivesRestoreValue;
    [SerializeField] private AudioClip collectSound;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<HP>().AddLives(LivesRestoreValue);
            SoundManager.instance.PlaySound(collectSound);
            animator.SetTrigger("collected");
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
