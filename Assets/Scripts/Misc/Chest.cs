using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private AudioClip chestSound;
    [SerializeField] private GameObject item;

    private void Awake() {
        animator = GetComponent<Animator>();
        item.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("IsOpened", true);
            SoundManager.instance.PlaySound(chestSound);
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void IsOpened()
    {
        if (item != null)
        {
            GameObject obtained_item = Instantiate(item, transform.position + Vector3.up*1.2f, Quaternion.identity);
            obtained_item.SetActive(true);
        }
    }
}
