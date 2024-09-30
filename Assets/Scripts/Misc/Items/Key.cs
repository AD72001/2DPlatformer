using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public AudioClip keyObtainedSound;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().keyTotal++;
            SoundManager.instance.PlaySound(keyObtainedSound);
            gameObject.SetActive(false);
        }
    }
}
