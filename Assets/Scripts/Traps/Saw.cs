using UnityEngine;

public class Saw : EnemyDamage 
{
    // Moving Points
    [SerializeField] private Transform[] points;
    [SerializeField] private int startingPoint;
    private int currentPoint;

    // Movement, Direction
    [SerializeField] private float speed;
    
    // Idling Time
    [SerializeField] private float idleTime;
    private float idleTimer;

    private void Awake() {
        transform.position = points[startingPoint].position;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, points[currentPoint].position) < 0.02f)
        {
            idleTimer += Time.deltaTime;

            if (idleTimer > idleTime)
            {
                currentPoint += 1;
                idleTimer = 0;
            }
                
            if (currentPoint >= points.Length)
                currentPoint = 0;
        }

        transform.position = Vector2.MoveTowards(transform.position, points[currentPoint].position, speed*Time.deltaTime);
    }
}
