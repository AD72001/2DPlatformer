using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkPointSound;
    private Transform currentCheckpoint;
    private HP playerHP;

    private UIManager uiManager;

    void Awake()
    {
        playerHP = GetComponent<HP>();
        uiManager = FindFirstObjectByType<UIManager>();
    }

    private void CheckRespawn()
    {
        if (currentCheckpoint == null)
        {
            uiManager.GameOver();

            return;
        }

        transform.position = currentCheckpoint.position;
        playerHP.Respawn();

        // Camera.main.GetComponent<CameraController>().MoveToNextArea(currentCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Checkpoint"))
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkPointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("activate");
        }
    }
}
