using System.Collections;
using UnityEngine;

public class Ghost : FlyingEnemy
{
    // Enemy Stat
    [SerializeField] private float distance;
    [SerializeField] private AudioClip ghostSound;
    [SerializeField] private Transform startingPoint;

    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();

        if (startingPoint == null)
            startingPoint.position = transform.position;
        else
            transform.position = startingPoint.position;
    }

    private void Update() 
    {
        if (player == null)
            return;

        if (player.GetComponent<HP>().defeat)
        {
            isChasing = false;
        }

        if (isChasing)
        {
            Flip();
            Chase();
        }

        if (!isActive && Vector2.Distance(transform.position, startingPoint.position) > 0.02f)
        {
            StartCoroutine(FadeInOutIE(startingPoint.position));
        }
    }

    private void FadeInOut()
    {
        isChasing = false;

        SoundManager.instance.PlaySound(ghostSound);
        StartCoroutine(FadeInOutIE(player.transform.position + new Vector3(distance, 0, 0)*-1*player.transform.localScale.x));
        
        // isChasing = true;
    }

    // Teleport behind the player when hit
    private IEnumerator FadeInOutIE(Vector3 destination)
    {
        animator.SetTrigger("disappear");
        yield return new WaitForSeconds(0.5f);
        transform.position = destination;
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("appear");
        yield return new WaitForSeconds(0.5f);
    }
}
