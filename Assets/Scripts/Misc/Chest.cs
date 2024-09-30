using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D collider;
    [SerializeField] private AudioClip chestSound;
    [SerializeField] private GameObject item;

    private void Awake() {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        item.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("IsOpened", true);
            SoundManager.instance.PlaySound(chestSound);
        }
    }

    private void IsOpened()
    {
        if (item != null)
        {
            GameObject obtained_item = Instantiate(item, collider.bounds.center, Quaternion.identity);
            obtained_item.SetActive(true);
        }
    }
}
