using UnityEngine;

public class Bat : FlyingEnemy
{
    [SerializeField] private Transform startingPoint;

    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = startingPoint.position;
    }

    private void Update() 
    {
        if (player == null)
        {
            Debug.Log("Player is null.");
            return;
        }
        
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

    private void ComeBack()
    {
        if (transform.position.x < player.transform.position.x)
            transform.rotation = Quaternion.Euler(0,0,0);
        else
            transform.rotation = Quaternion.Euler(0,180,0);
    }
}
