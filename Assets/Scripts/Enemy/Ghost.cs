using System.Collections;
using UnityEngine;

public class Ghost : FlyingEnemy
{
    // Enemy Stat
    [SerializeField] private float distance;

    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    private void Update() 
    {
        if (player == null)
            return;

        Flip();

        if (isChasing)
        {
            Chase();
        }
    }

    private void FadeInOut()
    {
        StartCoroutine(FadeInOutIE());
    }

    // Teleport behind the player when hit
    private IEnumerator FadeInOutIE()
    {
        isChasing = false;

        animator.SetTrigger("disappear");
        yield return new WaitForSeconds(0.5f);
        transform.position = player.transform.position + new Vector3(distance, 0, 0)*-1*player.transform.localScale.x;
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("appear");
        yield return new WaitForSeconds(0.5f);

        isChasing = true;
    }
}
