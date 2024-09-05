using UnityEngine;

public class HPCollectible : MonoBehaviour
{
    [SerializeField] private float HPRestoreValue;
    [SerializeField] private AudioClip collectSound;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<HP>().AddHP(HPRestoreValue);
            SoundManager.instance.PlaySound(collectSound);
            gameObject.SetActive(false);
        }
    }
}
