using System;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    // Left Edge and Right Edge
    [SerializeField] private Transform leftLimit;
    [SerializeField] private Transform rightLimit;

    // Components
    [SerializeField] private Transform enemy;
    [SerializeField] private Animator animator;

    // Movement, Direction
    [SerializeField] private int spriteDirection; // Sprites have different init scale x
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool moveLeft;
    
    // Idling Time
    [SerializeField] private float idleTime;
    public float idleTimer;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void Update()
    {
        if (moveLeft)
        {
            if (enemy.position.x >= leftLimit.position.x)
            {
                Moving(-1);
            }
            else {
                ChangeDirection();
            }
        }
        else {
            if (enemy.position.x <= rightLimit.position.x)
            {
                Moving(1);
            }
            else {
                ChangeDirection();
            }
        }
    }

    private void OnDisable() {
        animator.SetBool("moving", false);
    }

    private void ChangeDirection()
    {
        animator.SetBool("moving", false);

        idleTimer += Time.deltaTime;

        if (idleTimer > idleTime)
            moveLeft = !moveLeft;
        
    }

    private void Moving(int _direction)
    {
        idleTimer = 0;
        animator.SetBool("moving", true);   
        // Direction
        enemy.localScale = new Vector3(Math.Abs(initScale.x) * _direction * spriteDirection, initScale.y, initScale.z);

        // Move
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime*speed*_direction, enemy.position.y, enemy.position.z);
    }
}
