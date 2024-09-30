using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private AudioClip doorOpenSound;
    private bool isOpening = false;

    private void Update() {
        if (isOpening)
        {
            OpenDoor();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isOpening && other.gameObject.GetComponent<PlayerMovement>().keyTotal > 0)
            {
                isOpening = true;
                other.gameObject.GetComponent<PlayerMovement>().keyTotal--;
                SoundManager.instance.PlaySound(doorOpenSound);
            }
        }
    }

    private void OpenDoor()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination.position, 10*Time.deltaTime);
    }
}
