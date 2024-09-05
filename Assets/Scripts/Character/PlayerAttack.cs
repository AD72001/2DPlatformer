using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    private float attackCooldownDuration = Mathf.Infinity;
    [SerializeField] private Transform firePosition;
    [SerializeField] private GameObject[] fireBalls;
    [SerializeField] private AudioClip fireBallsSound;
    private PlayerMovement playerMovement;
    private Animator animator;
    
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && attackCooldownDuration >= attackCooldown)
        {
            Attack();
        }

        attackCooldownDuration += Time.deltaTime;
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
        SoundManager.instance.PlaySound(fireBallsSound);
        attackCooldownDuration = 0;

        fireBalls[LoadFireballs()].transform.position = firePosition.position;
        fireBalls[LoadFireballs()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int LoadFireballs()
    {
        for (int i = 0; i < fireBalls.Length; i++)
        {
            if (!fireBalls[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }
}
