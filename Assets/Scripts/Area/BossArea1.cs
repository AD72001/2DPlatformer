using System.Collections;
using UnityEngine;

public class BossArea1 : MonoBehaviour
{
    // Boss Component
    [SerializeField] private Trunk boss;

    // Player Component
    private GameObject player;

    // Area Component
    private Area area;

    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        area = GetComponent<Area>();
        boss.gameObject.SetActive(false);
    }

    private void Update() {
        if (player.GetComponent<HP>().defeat)
        {
            area.ActivateArea(false);
        }
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
