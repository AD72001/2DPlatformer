using UnityEngine;

public class FlyingEnemy : EnemyDamage
{
    protected GameObject player;
    // Enemy stat
    [SerializeField] protected float speed;
    public bool isChasing = false;
    public bool isActive;
    public Animator animator;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    protected void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, 
            player.GetComponent<Collider2D>().bounds.center, 
            speed*Time.deltaTime);
    }

    protected void Flip()
    {
        if (transform.position.x < player.transform.position.x)
            transform.rotation = Quaternion.Euler(0,180,0);
        else
            transform.rotation = Quaternion.Euler(0,0,0);
    }
}
