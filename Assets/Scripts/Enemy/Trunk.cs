using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : KnightRangeEnemy
{
    //Component
    private GameObject player;

    //Stat
    public bool isAttacking;
    public bool isChasing;
    public float speed;

    private void Awake() {
        isAttacking = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private new void Update() 
    {
        base.Update();
        if (isChasing && !isAttacking)
        {
            Flip();
            Chase();
        }
        else {
            animator.SetBool("moving", false);
        }

        if (PlayerInSight())
        {
            isAttacking = true;
            isChasing = false;
        }
        else {
            isAttacking = false;
        }
    }

    private void FinishAttack()
    {
        isAttacking = false;
    }

    private void Chase()
    {
        animator.SetBool("moving", true);

        transform.position = Vector2.MoveTowards(transform.position, 
            new Vector3(player.transform.position.x, transform.position.y, transform.position.z), 
            speed*Time.deltaTime);
    }

    private void Flip()
    {
        if (transform.position.x < player.transform.position.x)
            transform.rotation = Quaternion.Euler(0,180,0);
        else
            transform.rotation = Quaternion.Euler(0,0,0);
    }
}
