
using Unity.VisualScripting;
using UnityEngine;

public class FlyingEnemy : EnemyDamage
{
    private GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private Transform startingPoint;
    public bool isChasing = false;
    public Animator animator;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        transform.position = startingPoint.position;
    }

    void Update()
    {
        if (player == null)
            return;
        
        if (isChasing)
        {
            Flip();
            animator.SetBool("chase", true);
            Chase();
        }
        else
        {
            ComeBack();
            transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed*Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, startingPoint.position) < 0.02f)
        {
            animator.SetBool("chase", false);
        }
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime);
    }

    private void Flip()
    {
        if (transform.position.x < player.transform.position.x)
            transform.rotation = Quaternion.Euler(0,180,0);
        else
            transform.rotation = Quaternion.Euler(0,0,0);
    }

    private void ComeBack()
    {
        if (transform.position.x < player.transform.position.x)
            transform.rotation = Quaternion.Euler(0,0,0);
        else
            transform.rotation = Quaternion.Euler(0,180,0);
    }
}
