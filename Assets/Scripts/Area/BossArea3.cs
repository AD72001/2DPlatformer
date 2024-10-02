using UnityEngine;

public class BossArea3 : MonoBehaviour
{
    // Boss Component
    [SerializeField] private BOD_Boss boss;

    private void Awake() {
        boss.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            boss.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            boss.isChasing = true;
        }
    }
}
