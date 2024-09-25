using System.Collections;
using UnityEngine;

public class HP : MonoBehaviour
{
    // HP
    [SerializeField] private float startingHP;
    public float currentHP { get; private set; }

    // Lives
    [SerializeField] private float startingLives;
    public float currentLives { get; private set; }

    // IFrames
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRenderer;

    // Audio
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip defeatSound;

    [SerializeField] private Animator playerAnimator;
    private bool defeat = false;
    private bool isInvul = false;

    [SerializeField] private Behaviour[] components;

    private void Awake()
    {
        currentHP = startingHP;
        currentLives = startingLives;

        playerAnimator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    
    public void TakeDamage(float _dmg)
    {
        if (!isInvul)
        {
            // Limit current HP to 0 -> maximum
            currentHP = Mathf.Clamp(currentHP - _dmg, 0, startingHP);

            if (currentHP > 0)
            {
                playerAnimator.SetTrigger("hurt");
                SoundManager.instance.PlaySound(hurtSound);
                StartCoroutine(Invulnerable());
            }
            else
            {
                PlayerDead();
            }
        }
    }

    public void PlayerDead()
    {
        if (!defeat)
        {
            defeat = true;
            SoundManager.instance.PlaySound(defeatSound);

            foreach (Behaviour comp in components)
            {
                if (comp != null)
                    comp.enabled = false;
            }

            playerAnimator.SetBool("meleeAttack", false);
            playerAnimator.SetBool("onGround", true);
            playerAnimator.SetTrigger("dead");

            // Deactivate();
        }
    }

    public void AddHP(float _addHP)
    {
        currentHP = Mathf.Clamp(currentHP + _addHP, 0, startingHP);
    }

    public void AddLives(float _addLives)
    {
        currentLives += _addLives;
    }

    public void AddHPMax(float _addHPMax)
    {
        startingHP += _addHPMax;
        currentHP = Mathf.Clamp(currentHP + _addHPMax, 0, startingHP);
    }

    public void Respawn()
    {
        foreach (Behaviour comp in components)
        {
            if (comp != null)
                comp.enabled = true;
        }

        currentLives--;
        AddHP(startingHP);
        playerAnimator.ResetTrigger("dead");
        playerAnimator.Play("Idle");

        StartCoroutine(Invulnerable());

        defeat = false;
    }

    // IFrames function
    private IEnumerator Invulnerable()
    {
        isInvul = true;

        Physics.IgnoreLayerCollision(8, 9, true);

        for (int i = 0; i < numberOfFlashes; i++)   {
            spriteRenderer.color = new Color(1, 0, 0, 0.4f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        Physics.IgnoreLayerCollision(8, 9, false);

        isInvul = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
