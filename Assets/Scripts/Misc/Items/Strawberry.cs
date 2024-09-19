using UnityEngine;

public class Strawberry : MonoBehaviour
{
    // Kiwi restore lives to player
    [SerializeField] private float HPIncreaseValue;
    [SerializeField] private AudioClip collectSound;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<HP>().AddHPMax(HPIncreaseValue);
            SoundManager.instance.PlaySound(collectSound);
            animator.SetTrigger("collected");
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
