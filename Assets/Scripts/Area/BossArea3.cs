using UnityEngine;

public class BossArea3 : MonoBehaviour
{
    // Boss Component
    [SerializeField] private BOD_Boss boss;
    [SerializeField] private GameObject[] spells;
    [SerializeField] private Transform[] points;

    private void Awake() {
        boss.gameObject.SetActive(false);
    }

    private void Update() {
        if (boss.phase == 2)
        {
            // Activate lighting at points
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            boss.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player") && boss.phase == 1)
        {
            boss.isChasing = true;
        }
    }
}
