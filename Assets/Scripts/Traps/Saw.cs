using UnityEngine;

public class Saw : MonoBehaviour
{
    // Trap Status
    [SerializeField] private float damage;
    [SerializeField] private float movingDistance;
    [SerializeField] private float movingSpeed;

    // Trap Movement
    private bool movingLeft;
    private float limitLeft;
    private float limitRight;

    // Audio
    [SerializeField] private AudioClip sawSound;

    void Awake()
    {
        limitLeft = transform.position.x - movingDistance;
        limitRight = transform.position.x + movingDistance;
    }

    private void Update()
    {
        // SoundManager.instance.PlaySound(sawSound);

        if (movingLeft)
        {
            if (transform.position.x > limitLeft)
            {
                transform.position = new Vector3(transform.position.x - movingSpeed*Time.deltaTime, 
                    transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < limitRight)
            {
                transform.position = new Vector3(transform.position.x + movingSpeed*Time.deltaTime, 
                    transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = true;
            }
        }

    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<HP>().TakeDamage(damage);
        }
    }
}
