using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLighter : MonoBehaviour
{
    // Trap Status
    [SerializeField] private float fireDamage;
    [SerializeField] private float activationDelay;
    [SerializeField] private float activationTime;
    private Animator fireTrapAnimator;

    // Player Trigger Status
    private bool triggered;
    private bool activated;
    private bool isTakingDamage;

    // Audio
    [SerializeField] private AudioClip fireTrapSound;

    private void Awake()
    {
        fireTrapAnimator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (!triggered)
            {
                isTakingDamage = false;
                StartCoroutine(ActivateFireLighter());
            }
            if (activated && !isTakingDamage)
            {
                collider.GetComponent<HP>().TakeDamage(fireDamage);
                isTakingDamage = true;
            }
        }
    }

    private IEnumerator ActivateFireLighter()
    {
        // Trap is stepped on
        triggered = true;
        fireTrapAnimator.SetTrigger("fireTrapHit");

        // Trap is activated after delay
        yield return new WaitForSeconds(activationDelay);
        activated = true;
        fireTrapAnimator.SetBool("fireTrapActive", true);
        SoundManager.instance.PlaySound(fireTrapSound);

        // Trap activated for a while
        yield return new WaitForSeconds(activationTime);

        // Trap deactivated
        fireTrapAnimator.SetBool("fireTrapActive", false);
        activated = false;
        triggered = false;
    }
}
