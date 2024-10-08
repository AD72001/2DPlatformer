using System.Collections;
using UnityEngine;

public class HP : MonoBehaviour
{
    // HP
    [SerializeField] public float startingHP;
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

    private Animator animator;
    
    public bool defeat {get; private set;}
    private bool isInvul = false;

    [SerializeField] private Behaviour[] components;

    private void Awake()
    {
        currentHP = startingHP;
        currentLives = startingLives;

        defeat = false;

        animator = gameObject.GetComponent<Animator>();
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
                animator.SetTrigger("hurt");
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

            animator.SetBool("meleeAttack", false);
            animator.SetBool("onGround", true);
            animator.SetTrigger("dead");

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
        gameObject.GetComponent<PlayerAttack>().projectileTotal += 10;
        animator.ResetTrigger("dead");
        animator.Play("Idle");

        StartCoroutine(Invulnerable());

        defeat = false;
    }

    // IFrames function
    private IEnumerator Invulnerable()
    {
        isInvul = true;

        Physics2D.IgnoreLayerCollision(8, 9, true);

        for (int i = 0; i < numberOfFlashes; i++)   {
            spriteRenderer.color = new Color(1, 0, 0, 0.4f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(8, 9, false);

        isInvul = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
